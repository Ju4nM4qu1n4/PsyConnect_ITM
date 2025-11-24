using PsyConnect.Core.Entities.Resultados;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsyConnect.Data.Repositories.Interfaces
{
    public interface IResultadoRepository : IRepository<ResultadoInterpretacion>
    {
        Task<IEnumerable<ResultadoInterpretacion>> GetResultadosPorEstudianteAsync(int estudianteId);
        Task<IEnumerable<ResultadoInterpretacion>> GetResultadosPorNivelAsync(string nivel);
        Task<ResultadoInterpretacion?> GetByIdAsync(int resultadoId);
        Task<ResultadoInterpretacion?> GetResultadoPorRespuestaAsync(int respuestaId);
        Task AddAsync(ResultadoInterpretacion resultado);
        Task SaveChangesAsync();
        Task<IEnumerable<ResultadoInterpretacion>> GetResultadosEvaluadosPorPsicologoAsync(int psicologoId);
        
    }
}