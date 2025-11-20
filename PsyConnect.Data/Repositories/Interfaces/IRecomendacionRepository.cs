using PsyConnect.Core.Entities.Recomendaciones;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsyConnect.Data.Repositories.Interfaces
{
    public interface IRecomendacionRepository : IRepository<RecomendacionPersonalizada>
    {
        Task<IEnumerable<RecomendacionPersonalizada>> GetRecomendacionesPorEstudianteAsync(int estudianteId);
        Task<IEnumerable<RecomendacionPersonalizada>> GetRecomendacionesVigentesAsync();
        Task<IEnumerable<RecomendacionPersonalizada>> GetRecomendacionesPorPsicologoAsync(int psicologoId);
        Task<IEnumerable<RecomendacionPersonalizada>> GetRecomendacionesPorResultadoAsync(int resultadoId);
    }
}