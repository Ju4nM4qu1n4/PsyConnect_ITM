using PsyConnect.Core.Entities.Respuestas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsyConnect.Data.Repositories.Interfaces
{
    public interface IRespuestaTestRepository : IRepository<RespuestaTest>
    {
        Task<RespuestaTest?> GetByIdAsync(int respuestaId);
        Task<RespuestaTest?> GetRespuestaConDetallesAsync(int respuestaId);
        Task<List<RespuestaTest>> GetRespuestasPorEstudianteAsync(int estudianteId);
        Task AddAsync(RespuestaTest respuestaTest);
        void Update(RespuestaTest respuestaTest);
        Task SaveChangesAsync();
        Task<IEnumerable<RespuestaTest>> GetRespuestasPorTestAsync(int testId);
        Task<IEnumerable<RespuestaTest>> GetRespuestasCompletadasAsync();
        Task<IEnumerable<RespuestaTest>> GetRespuestasEnProgresoAsync();
        Task<int> CountRespuestasPorTestAsync(int testId);
    }
}