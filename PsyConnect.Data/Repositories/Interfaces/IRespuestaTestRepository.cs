using PsyConnect.Core.Entities.Respuestas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsyConnect.Data.Repositories.Interfaces
{
    public interface IRespuestaTestRepository : IRepository<RespuestaTest>
    {
        Task<IEnumerable<RespuestaTest>> GetRespuestasPorEstudianteAsync(int estudianteId);
        Task<IEnumerable<RespuestaTest>> GetRespuestasPorTestAsync(int testId);
        Task<IEnumerable<RespuestaTest>> GetRespuestasCompletadasAsync();
        Task<IEnumerable<RespuestaTest>> GetRespuestasEnProgresoAsync();
        Task<RespuestaTest> GetRespuestaConDetallesAsync(int respuestaId);
        Task<int> CountRespuestasPorTestAsync(int testId);
    }
}