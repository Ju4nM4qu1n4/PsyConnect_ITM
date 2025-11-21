using PsyConnect.Core.Entities.Recomendaciones;
using PsyConnect.Core.Models.DTOs.Recomendaciones;
using PsyConnect.Core.Models.Requests;
using PsyConnect.Data.Repositories.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsyConnect.Business.Services.Recomendaciones
{
    public class RecomendacionService : IRecomendacionService
    {
        private readonly IRecomendacionRepository _recomendacionRepository;
        private readonly IEstudianteRepository _estudianteRepository;
        private readonly IMapper _mapper;

        public RecomendacionService(
            IRecomendacionRepository recomendacionRepository,
            IEstudianteRepository estudianteRepository,
            IMapper mapper)
        {
            _recomendacionRepository = recomendacionRepository;
            _estudianteRepository = estudianteRepository;
            _mapper = mapper;
        }

        public async Task<RecomendacionDTO> AsignarRecomendacionAsync(AsignarRecomendacionRequest request)
        {
           
            var estudiante = await _estudianteRepository.GetByIdAsync(request.EstudianteId);
            if (estudiante == null)
                throw new Exception("Estudiante no encontrado");

            
            var recomendacion = new RecomendacionPersonalizada
            {
                EstudianteID = request.EstudianteId,
                PsicólogoID = request.PsicologoId,
                ResultadoID = request.ResultadoId,
                Título = request.Título,
                Descripción = request.Descripción,
                TipoRecurso = request.TipoRecurso,
                URL = request.URL,
                FechaAsignación = DateTime.Now,
                Vigente = true
            };

            await _recomendacionRepository.AddAsync(recomendacion);
            await _recomendacionRepository.SaveChangesAsync();

            return _mapper.Map<RecomendacionDTO>(recomendacion);
        }

        public async Task<IEnumerable<RecomendacionDTO>> ObtenerRecomendacionesEstudianteAsync(int estudianteId)
        {
            var recomendaciones = await _recomendacionRepository.GetRecomendacionesPorEstudianteAsync(estudianteId);
            return _mapper.Map<IEnumerable<RecomendacionDTO>>(recomendaciones);
        }

        public async Task<IEnumerable<RecomendacionDTO>> ObtenerRecomendacionesVigentesAsync()
        {
            var recomendaciones = await _recomendacionRepository.GetRecomendacionesVigentesAsync();
            return _mapper.Map<IEnumerable<RecomendacionDTO>>(recomendaciones);
        }

        public async Task EliminarRecomendacionAsync(int recomendacionId)
        {
            var recomendacion = await _recomendacionRepository.GetByIdAsync(recomendacionId);
            if (recomendacion == null)
                throw new Exception("Recomendación no encontrada");

            recomendacion.Vigente = false;
            _recomendacionRepository.Update(recomendacion);
            await _recomendacionRepository.SaveChangesAsync();
        }
    }
}