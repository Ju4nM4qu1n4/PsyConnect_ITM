using PsyConnect.Core.Entities.Tests;
using PsyConnect.Core.Models;

namespace PsyConnect.Data.Repositories
{
    // Repositorio de OpcionesRespuesta
    public interface IOpcionRespuestaRepository
    {
        Task<OpcionRespuesta?> GetByIdAsync(int opcionId);
        Task<List<OpcionRespuesta>> GetByPreguntaIdAsync(int preguntaId);
    }

    // Repositorio de DetallesRespuestaTest (opcional, por si lo necesitas)
    public interface IDetalleRespuestaTestRepository
    {
        Task<List<DetalleRespuestaTest>> GetByRespuestaIdAsync(int respuestaId);
        Task AddAsync(DetalleRespuestaTest detalle);
        Task AddRangeAsync(List<DetalleRespuestaTest> detalles);
        Task SaveChangesAsync();
    }
}