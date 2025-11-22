using PsyConnect.Core.Entities.Resultados;
using PsyConnect.Core.Models.DTOs.Resultados;
using PsyConnect.Data.Repositories.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsyConnect.Business.Services.Resultados
{
    public class ResultadoService : IResultadoService
    {
        private readonly IResultadoRepository _resultadoRepository;
        private readonly IRespuestaTestRepository _respuestaTestRepository;
        private readonly IMapper _mapper;

        public ResultadoService(
            IResultadoRepository resultadoRepository,
            IRespuestaTestRepository respuestaTestRepository,
            IMapper mapper)
        {
            _resultadoRepository = resultadoRepository;
            _respuestaTestRepository = respuestaTestRepository;
            _mapper = mapper;
        }

        public async Task<ResultadoDTO> EvaluarTestAsync(int respuestaId)
        {
            var respuesta = await _respuestaTestRepository.GetByIdAsync(respuestaId);
            if (respuesta == null)
                throw new Exception("Respuesta no encontrada");

            if (respuesta.EstadoID != 2) 
                throw new Exception("El test no ha sido completado");

            string nivel = DeterminarNivel(respuesta.PuntajeTotal ?? 0);
            string interpretacion = GenerarInterpretacion(respuesta.PuntajeTotal ?? 0, nivel);
            string recomendacion = GenerarRecomendacion(respuesta.PuntajeTotal ?? 0, nivel);

           
            var resultado = new ResultadoInterpretacion
            {
                RespuestaID = respuestaId,
                Nivel = nivel,
                Interpretacion = interpretacion,
                Recomendacion = recomendacion,
                FechaEvaluacion = DateTime.Now
            };

            await _resultadoRepository.AddAsync(resultado);
            await _resultadoRepository.SaveChangesAsync();

            return _mapper.Map<ResultadoDTO>(resultado);
        }

        public async Task<ResultadoDTO> ObtenerResultadoAsync(int resultadoId)
        {
            var resultado = await _resultadoRepository.GetByIdAsync(resultadoId);
            if (resultado == null)
                throw new Exception("Resultado no encontrado");

            return _mapper.Map<ResultadoDTO>(resultado);
        }

        public async Task<IEnumerable<ResultadoDTO>> ObtenerResultadosPorEstudianteAsync(int estudianteId)
        {
            var resultados = await _resultadoRepository.GetResultadosPorEstudianteAsync(estudianteId);
            return _mapper.Map<IEnumerable<ResultadoDTO>>(resultados);
        }

        private string DeterminarNivel(int puntaje)
        {
            if (puntaje >= 80) return "Critico";
            if (puntaje >= 60) return "Alto";
            if (puntaje >= 40) return "Medio";
            return "Bajo";
        }

        private string GenerarInterpretacion(int puntaje, string nivel)
        {
            return nivel switch
            {
                "Critico" => "Presenta sintomas muy pronunciados que requieren atencion inmediata.",
                "Alto" => "Presenta sintomas significativos que se recomienda abordar.",
                "Medio" => "Presenta algunos sintomas que pueden manejarse con seguimiento.",
                _ => "Presenta sintomas minimos."
            };
        }

        private string GenerarRecomendacion(int puntaje, string nivel)
        {
            return nivel switch
            {
                "Critico" => "Se recomienda sesion urgente con psicologo. Considera buscar ayuda profesional.",
                "Alto" => "Se recomienda seguimiento con psicologo y tecnicas de autocuidado.",
                "Medio" => "Se sugiere mantener seguimiento y practicar tecnicas de bienestar.",
                _ => "Manten habitos saludables y bienestar general."
            };
        }
    }
}