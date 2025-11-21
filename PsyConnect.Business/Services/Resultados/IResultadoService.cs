using PsyConnect.Core.Models.DTOs.Resultados;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsyConnect.Business.Services.Resultados
{
    public interface IResultadoService
    {
        Task<ResultadoDTO> EvaluarTestAsync(int respuestaId);
        Task<ResultadoDTO> ObtenerResultadoAsync(int resultadoId);
        Task<IEnumerable<ResultadoDTO>> ObtenerResultadosPorEstudianteAsync(int estudianteId);
    }
}