using PsyConnect.Core.Models.DTOs.Citas;
using PsyConnect.Core.Models.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsyConnect.Business.Services.Citas
{
    public interface ICitaService
    {
        Task<CitaDTO> AgendarCitaAsync(AgendarCitaRequest request);
        Task CancelarCitaAsync(int citaId);
        Task<CitaDTO> ObtenerCitaAsync(int citaId);
        Task<IEnumerable<CitaDTO>> ObtenerCitasPorEstudianteAsync(int estudianteId);
        Task<IEnumerable<CitaDTO>> ObtenerCitasProximasAsync();
        Task ActualizarEstadoCitaAsync(int citaId, int nuevoEstado);
        Task<IEnumerable<CitaDTO>> ObtenerCitasPorPsicologoAsync(int psicologoId);
    }
}