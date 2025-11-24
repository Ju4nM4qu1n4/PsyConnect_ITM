using PsyConnect.Core.Entities.Tests;
using PsyConnect.Data;
using PsyConnect.Core.Entities.Resultados;
using PsyConnect.Core.Entities.Respuestas;
using PsyConnect.Core.Models.DTOs.Tests;
using PsyConnect.Core.Models.Requests;
using PsyConnect.Data.Repositories.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PsyConnect.Core.Models.Responses;
using PsyConnect.Data.Repositories.Implementations;
using PsyConnect.Data.Repositories;

namespace PsyConnect.Business.Services.Tests
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IRespuestaTestRepository _respuestaTestRepository;
        private readonly IEstudianteRepository _estudianteRepository;
        private readonly IOpcionRespuestaRepository _opcionRespuestaRepository;
        private readonly IResultadoRepository _resultadoRepository;
        private readonly IMapper _mapper;

        public TestService(
            ITestRepository testRepository,
            IRespuestaTestRepository respuestaTestRepository,
            IEstudianteRepository estudianteRepository,
            IMapper mapper,
            IOpcionRespuestaRepository opcionRespuestaRepository,
            IResultadoRepository resultadoRepository)
        {
            _testRepository = testRepository;
            _respuestaTestRepository = respuestaTestRepository;
            _estudianteRepository = estudianteRepository;
            _opcionRespuestaRepository = opcionRespuestaRepository;
            _resultadoRepository = resultadoRepository;
            _mapper = mapper;
        }

        public async Task<TestDTO> ObtenerTestAsync(int testId)
        {
            var test = await _testRepository.GetTestConPreguntasAsync(testId);
            if (test == null)
                throw new Exception("Test no encontrado");

            return _mapper.Map<TestDTO>(test);
        }

        public async Task<IEnumerable<TestDTO>> ObtenerTestsActivosAsync()
        {
            var tests = await _testRepository.GetTestsActivosAsync();
            return _mapper.Map<IEnumerable<TestDTO>>(tests);
        }

        public async Task<IEnumerable<TestDTO>> ObtenerTestsPorTipoAsync(int tipoTestId)
        {
            var tests = await _testRepository.GetTestsPorTipoAsync(tipoTestId);
            return _mapper.Map<IEnumerable<TestDTO>>(tests);
        }
        public async Task<RespuestaTest> IniciarTestAsync(int estudianteId, int testId)
        {
            // Validar estudiante usando el repositorio
            var estudiante = await _estudianteRepository.GetByIdAsync(estudianteId);
            if (estudiante == null)
            {
                throw new KeyNotFoundException("Estudiante no encontrado");
            }

            // Validar test usando el repositorio
            var test = await _testRepository.GetByIdAsync(testId);
            if (test == null)
            {
                throw new KeyNotFoundException("Test no encontrado");
            }

            // Validar que el test esté activo
            if (!test.Activo)
            {
                throw new InvalidOperationException("Este test no está disponible");
            }

            // Crear la respuesta del test
            var respuestaTest = new RespuestaTest
            {
                EstudianteID = estudianteId,
                TestID = testId,
                FechaInicio = DateTime.Now,
                EstadoID = 1, // En Progreso
                PuntajeTotal = 0
            };

            // Guardar usando el repositorio
            await _respuestaTestRepository.AddAsync(respuestaTest);
            await _respuestaTestRepository.SaveChangesAsync();

            return respuestaTest;
        }

        public async Task IniciarRespuestaTestAsync(int estudianteId, int testId)
        {

            var estudiante = await _estudianteRepository.GetByIdAsync(estudianteId);
            if (estudiante == null)
                throw new Exception("Estudiante no encontrado");


            var test = await _testRepository.GetByIdAsync(testId);
            if (test == null)
                throw new Exception("Test no encontrado");


            if (!test.Activo)
                throw new Exception("Este test no esta disponible");


            var respuesta = new RespuestaTest
            {
                EstudianteID = estudianteId,
                TestID = testId,
                EstadoID = 1,
                FechaInicio = DateTime.Now
            };

            await _respuestaTestRepository.AddAsync(respuesta);
            await _respuestaTestRepository.SaveChangesAsync();
        }

        public async Task GuardarRespuestaAsync(GuardarRespuestaRequest request)
        {
            var respuesta = await _respuestaTestRepository.GetByIdAsync(request.RespuestaId);
            if (respuesta == null)
                throw new Exception("Respuesta no encontrada");

            if (respuesta.EstadoID != 1)
                throw new Exception("No puedes editar esta respuesta");


            _respuestaTestRepository.Update(respuesta);
            await _respuestaTestRepository.SaveChangesAsync();
        }

        public async Task<ResultadoTestResponse> EnviarRespuestasAsync(EnviarRespuestasTestRequest request)
        {
            // Validar que el test existe
            var test = await _testRepository.GetByIdAsync(request.TestID);
            if (test == null)
                throw new Exception("Test no encontrado");

            // Crear y persistir la respuesta principal
            var respuestaTest = new RespuestaTest
            {
                EstudianteID = request.EstudianteID,
                TestID = request.TestID,
                EstadoID = 2, // Completado
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now,
                PuntajeTotal = 0
            };

            await _respuestaTestRepository.AddAsync(respuestaTest);
            await _respuestaTestRepository.SaveChangesAsync();

            // Recuperar la entidad respuesta con colección de detalles
            var respuestaConDetalles = await _respuestaTestRepository.GetRespuestaConDetallesAsync(respuestaTest.RespuestaID);
            if (respuestaConDetalles == null)
                throw new Exception("No se pudo recuperar la respuesta creada");

            int puntajeTotal = 0;

            foreach (var r in request.Respuestas)
            {
                var opcion = await _opcionRespuestaRepository.GetByIdAsync(r.OpcionID);
                if (opcion == null)
                    continue;

                var detalle = new DetalleRespuestaTest
                {
                    RespuestaID = respuestaConDetalles.RespuestaID,
                    PreguntaID = r.PreguntaID,
                    OpcionSeleccionada = r.OpcionID,
                    ValorRespuesta = opcion.Valor
                };

                // Añadir detalle a la colección del agregado
                if (respuestaConDetalles.DetallesRespuesta == null)
                    respuestaConDetalles.DetallesRespuesta = new List<DetalleRespuestaTest>();

                respuestaConDetalles.DetallesRespuesta.Add(detalle);
                puntajeTotal += opcion.Valor;
            }

            // Actualizar puntaje y estado
            respuestaConDetalles.PuntajeTotal = puntajeTotal;
            respuestaConDetalles.EstadoID = 2;
            respuestaConDetalles.FechaFinalizacion = DateTime.Now;

            _respuestaTestRepository.Update(respuestaConDetalles);
            await _respuestaTestRepository.SaveChangesAsync();

            // Calcular interpretación
            var interpretacion = CalcularInterpretacion(puntajeTotal);

            var resultado = new ResultadoInterpretacion
            {
                RespuestaID = respuestaConDetalles.RespuestaID,
                Interpretacion = interpretacion.Interpretacion,
                Recomendacion = interpretacion.Recomendacion,
                Nivel = interpretacion.Nivel,
                FechaEvaluacion = DateTime.Now
            };

            await _resultadoRepository.AddAsync(resultado);
            await _resultadoRepository.SaveChangesAsync();

            return new ResultadoTestResponse
            {
                RespuestaID = respuestaConDetalles.RespuestaID,
                PuntajeTotal = puntajeTotal,
                Nivel = interpretacion.Nivel,
                Interpretacion = interpretacion.Interpretacion,
                Recomendacion = interpretacion.Recomendacion,
                FechaRealizacion = respuestaConDetalles.FechaFinalizacion ?? DateTime.Now
            };
        }

        public async Task<List<HistorialTestResponse>> ObtenerHistorialTestsAsync(int estudianteId)
        {
            var respuestas = await _respuestaTestRepository.GetRespuestasPorEstudianteAsync(estudianteId);

            return respuestas.Select(r => new HistorialTestResponse
            {
                RespuestaID = r.RespuestaID,
                NombreTest = r.Test?.NombreTest ?? "Test",
                FechaRealizacion = r.FechaFinalizacion ?? r.FechaInicio,
                PuntajeTotal = r.PuntajeTotal ?? 0,
                Estado = r.EstadoRespuestaTest?.Nombre ?? "Desconocido",
                Nivel = r.ResultadoInterpretacion?.Nivel
            }).ToList();
        }

        public async Task<ResultadoTestResponse> ObtenerResultadoAsync(int respuestaId)
        {
            // Obtener resultado (interpretación)
            var resultado = await _resultadoRepository.GetResultadoPorRespuestaAsync(respuestaId);

            // Obtener la respuesta para datos base
            var respuesta = await _respuestaTestRepository.GetByIdAsync(respuestaId);
            if (respuesta == null)
                throw new Exception("Resultado no encontrado");

            return new ResultadoTestResponse
            {
                RespuestaID = respuesta.RespuestaID,
                PuntajeTotal = respuesta.PuntajeTotal ?? 0,
                Nivel = resultado?.Nivel ?? "Sin evaluar",
                Interpretacion = resultado?.Interpretacion ?? "Pendiente de evaluación",
                Recomendacion = resultado?.Recomendacion ?? "Consulte con un profesional",
                FechaRealizacion = respuesta.FechaFinalizacion ?? respuesta.FechaInicio
            };
        }

        // Método privado para calcular interpretación según puntaje
        private (string Nivel, string Interpretacion, string Recomendacion) CalcularInterpretacion(int puntaje)
        {
            // Escala basada en 10 preguntas con valores 0-3 (máximo 30 puntos)
            if (puntaje <= 10)
            {
                return (
                    "Bajo",
                    "Tus resultados indican un nivel bajo de estrés o ansiedad. Te encuentras en un estado emocional saludable.",
                    "Continúa con tus hábitos saludables. Considera mantener rutinas de autocuidado como ejercicio regular y tiempo de descanso."
                );
            }
            else if (puntaje <= 20)
            {
                return (
                    "Moderado",
                    "Tus resultados sugieren un nivel moderado de estrés o ansiedad. Es importante prestar atención a tu bienestar emocional.",
                    "Te recomendamos agendar una cita con un psicólogo para explorar técnicas de manejo de estrés. Practica ejercicios de relajación y mantén una comunicación abierta con personas de confianza."
                );
            }
            else
            {
                return (
                    "Alto",
                    "Tus resultados indican un nivel alto de estrés o ansiedad. Es importante que busques apoyo profesional.",
                    "Te recomendamos encarecidamente agendar una cita con un psicólogo lo antes posible. El apoyo profesional puede ayudarte a desarrollar estrategias efectivas de afrontamiento."
                );
            }
        }

        public Task CompletarTestAsync(int respuestaId)
        {
            throw new NotImplementedException();
        }
    }
}