using PsyConnect.Core.Entities.Tests;
using PsyConnect.Core.Entities.Respuestas;
using PsyConnect.Core.Models.DTOs.Tests;
using PsyConnect.Core.Models.Requests;
using PsyConnect.Data.Repositories.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsyConnect.Business.Services.Tests
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IRespuestaTestRepository _respuestaTestRepository;
        private readonly IEstudianteRepository _estudianteRepository;
        private readonly IMapper _mapper;

        public TestService(
            ITestRepository testRepository,
            IRespuestaTestRepository respuestaTestRepository,
            IEstudianteRepository estudianteRepository,
            IMapper mapper)
        {
            _testRepository = testRepository;
            _respuestaTestRepository = respuestaTestRepository;
            _estudianteRepository = estudianteRepository;
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

        public async Task IniciarRespuestaTestAsync(int estudianteId, int testId)
        {
            
            var estudiante = await _estudianteRepository.GetByIdAsync(estudianteId);
            if (estudiante == null)
                throw new Exception("Estudiante no encontrado");

            
            var test = await _testRepository.GetByIdAsync(testId);
            if (test == null)
                throw new Exception("Test no encontrado");

           
            if (!test.Activo)
                throw new Exception("Este test no está disponible");

          
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

        public async Task CompletarTestAsync(int respuestaId)
        {
            var respuesta = await _respuestaTestRepository.GetRespuestaConDetallesAsync(respuestaId);
            if (respuesta == null)
                throw new Exception("Respuesta no encontrada");

           
            var test = await _testRepository.GetByIdAsync(respuesta.TestID);
            if (respuesta.DetallesRespuesta.Count != test.CantidadPreguntas)
                throw new Exception("Debes responder todas las preguntas");

            int puntajeTotal = respuesta.DetallesRespuesta.Sum(d => d.ValorRespuesta ?? 0);

            respuesta.PuntajeTotal = puntajeTotal;
            respuesta.EstadoID = 2; 
            respuesta.FechaFinalización = DateTime.Now;

            _respuestaTestRepository.Update(respuesta);
            await _respuestaTestRepository.SaveChangesAsync();
        }
    }
}