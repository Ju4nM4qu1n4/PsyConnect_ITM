using PsyConnect.Core.Entities.Citas;
using PsyConnect.Core.Models.DTOs.Citas;
using PsyConnect.Core.Models.Requests;
using PsyConnect.Data.Repositories.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsyConnect.Business.Services.Citas
{
    public class CitaService : ICitaService
    {
        private readonly ICitaRepository _citaRepository;
        private readonly IEstudianteRepository _estudianteRepository;
        private readonly IMapper _mapper;

        public CitaService(
            ICitaRepository citaRepository,
            IEstudianteRepository estudianteRepository,
            IMapper mapper)
        {
            _citaRepository = citaRepository;
            _estudianteRepository = estudianteRepository;
            _mapper = mapper;
        }

        public async Task<CitaDTO> AgendarCitaAsync(AgendarCitaRequest request)
        {
            
            var estudiante = await _estudianteRepository.GetByIdAsync(request.EstudianteId);
            if (estudiante == null)
                throw new Exception("Estudiante no encontrado");

           
            if (request.FechaHora < DateTime.Now.AddHours(24))
                throw new Exception("Debes agendar con al menos 24 horas de anticipacion");

            
            var citasExistentes = await _citaRepository.GetCitasPorEstudianteAsync(request.EstudianteId);
            if (citasExistentes.Any(c => c.FechaHora.Date == request.FechaHora.Date && c.EstadoID != 5))
                throw new Exception("Ya tienes una cita agendada para ese dia");

          
            var cita = new Cita
            {
                EstudianteID = request.EstudianteId,
                PsicologoID = request.PsicologoId,
                ModalidadID = request.ModalidadId,
                EstadoID = 1,
                FechaHora = request.FechaHora,
                Duracion = 60,
                Ubicacion = request.Ubicacion,
                NotasEstudiante = request.Notas,
                FechaRegistro = DateTime.Now,
                EnlaceTeams = "",
                ObservacionesPsicologo = "",
                
            };

            await _citaRepository.AddAsync(cita);
            await _citaRepository.SaveChangesAsync();

            return _mapper.Map<CitaDTO>(cita);
        }

        public async Task CancelarCitaAsync(int citaId)
        {
            var cita = await _citaRepository.GetByIdAsync(citaId);
            if (cita == null)
                throw new Exception("Cita no encontrada");

          
            if (cita.EstadoID == 4)
                throw new Exception("No puedes cancelar una cita completada");

            cita.EstadoID = 5; 
            _citaRepository.Update(cita);
            await _citaRepository.SaveChangesAsync();
        }

        public async Task<CitaDTO> ObtenerCitaAsync(int citaId)
        {
            var cita = await _citaRepository.GetByIdAsync(citaId);
            if (cita == null)
                throw new Exception("Cita no encontrada");

            return _mapper.Map<CitaDTO>(cita);
        }

        public async Task<IEnumerable<CitaDTO>> ObtenerCitasPorEstudianteAsync(int estudianteId)
        {
            var citas = await _citaRepository.GetCitasPorEstudianteAsync(estudianteId);
            return _mapper.Map<IEnumerable<CitaDTO>>(citas);
        }

        public async Task<IEnumerable<CitaDTO>> ObtenerCitasProximasAsync()
        {
            var citas = await _citaRepository.GetCitasProximasAsync(DateTime.Now);
            return _mapper.Map<IEnumerable<CitaDTO>>(citas);
        }

        public async Task ActualizarEstadoCitaAsync(int citaId, int nuevoEstado)
        {
            var cita = await _citaRepository.GetByIdAsync(citaId);
            if (cita == null)
                throw new Exception("Cita no encontrada");

            cita.EstadoID = nuevoEstado;
            _citaRepository.Update(cita);
            await _citaRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CitaDTO>> ObtenerCitasPorPsicologoAsync(int psicologoId)
        {
            var citas = await _citaRepository.GetCitasPorPsicologoAsync(psicologoId);
            return _mapper.Map<IEnumerable<CitaDTO>>(citas);
        }
    }
}