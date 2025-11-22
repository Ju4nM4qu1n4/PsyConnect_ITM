using Microsoft.EntityFrameworkCore;
using PsyConnect.Core.Entities.Citas;
using PsyConnect.Data.Context;
using PsyConnect.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsyConnect.Data.Repositories.Implementations
{
    public class CitaRepository : Repository<Cita>, ICitaRepository
    {
        public CitaRepository(PsyConnectContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Cita>> GetCitasPorEstudianteAsync(int estudianteId)
        {
            return await _dbSet
                .Where(c => c.EstudianteID == estudianteId)
                .Include(c => c.Psicologo)
                .Include(c => c.ModalidadCita)
                .Include(c => c.EstadoCita)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cita>> GetCitasPorPsicologoAsync(int psicologoId)
        {
            return await _dbSet
                .Where(c => c.PsicologoID == psicologoId)
                .Include(c => c.Estudiante)
                .Include(c => c.ModalidadCita)
                .Include(c => c.EstadoCita)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cita>> GetCitasProximasAsync(DateTime fecha)
        {
            return await _dbSet
                .Where(c => c.FechaHora > fecha && c.EstadoID != 5) // 5 = Cancelada
                .Include(c => c.Estudiante)
                .Include(c => c.Psicologo)
                .OrderBy(c => c.FechaHora)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cita>> GetCitasCompletadasPorEstudianteAsync(int estudianteId)
        {
            return await _dbSet
                .Where(c => c.EstudianteID == estudianteId && c.EstadoID == 4) // 4 = Completada
                .Include(c => c.Psicologo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cita>> GetCitasDisponiblesPorPsicologoAsync(int psicologoId, DateTime fecha)
        {
            return await _dbSet
                .Where(c => c.PsicologoID == psicologoId && c.FechaHora.Date == fecha.Date && c.EstadoID == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cita>> GetCitasPorModalidadAsync(int modalidadId)
        {
            return await _dbSet
                .Where(c => c.ModalidadID == modalidadId)
                .Include(c => c.Estudiante)
                .Include(c => c.Psicologo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cita>> GetCitasPorEstadoAsync(int estadoId)
        {
            return await _dbSet
                .Where(c => c.EstadoID == estadoId)
                .Include(c => c.Estudiante)
                .Include(c => c.Psicologo)
                .ToListAsync();
        }
    }
}