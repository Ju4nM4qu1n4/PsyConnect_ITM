using PsyConnect.Core.Entities.Citas;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsyConnect.Data.Repositories.Interfaces
{
    public interface ICitaRepository : IRepository<Cita>
    {
        Task<IEnumerable<Cita>> GetCitasPorEstudianteAsync(int estudianteId);
        Task<IEnumerable<Cita>> GetCitasPorPsicologoAsync(int psicologoId);
        Task<IEnumerable<Cita>> GetCitasProximasAsync(DateTime fecha);
        Task<IEnumerable<Cita>> GetCitasCompletadasPorEstudianteAsync(int estudianteId);
        Task<IEnumerable<Cita>> GetCitasDisponiblesPorPsicologoAsync(int psicologoId, DateTime fecha);
        Task<IEnumerable<Cita>> GetCitasPorModalidadAsync(int modalidadId);
        Task<IEnumerable<Cita>> GetCitasPorEstadoAsync(int estadoId);
    }
}