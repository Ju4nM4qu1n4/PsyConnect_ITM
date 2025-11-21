using PsyConnect.Core.Models.DTOs.Recomendaciones;
using PsyConnect.Core.Models.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsyConnect.Business.Services.Recomendaciones
{
    public interface IRecomendacionService
    {
        Task<RecomendacionDTO> AsignarRecomendacionAsync(AsignarRecomendacionRequest request);
        Task<IEnumerable<RecomendacionDTO>> ObtenerRecomendacionesEstudianteAsync(int estudianteId);
        Task<IEnumerable<RecomendacionDTO>> ObtenerRecomendacionesVigentesAsync();
        Task EliminarRecomendacionAsync(int recomendacionId);
    }
}