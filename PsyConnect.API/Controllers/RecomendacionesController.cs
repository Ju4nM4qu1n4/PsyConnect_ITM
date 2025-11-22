using PsyConnect.Business.Services.Recomendaciones;
using PsyConnect.Core.Models.Requests;
using PsyConnect.Core.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PsyConnect.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecomendacionesController : ControllerBase
    {
        private readonly IRecomendacionService _recomendacionService;

        public RecomendacionesController(IRecomendacionService recomendacionService)
        {
            _recomendacionService = recomendacionService;
        }

        [HttpPost("asignar")]
        public async Task<IActionResult> AsignarRecomendacion([FromBody] AsignarRecomendacionRequest request)
        {
            try
            {
                var recomendacion = await _recomendacionService.AsignarRecomendacionAsync(request);
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Recomendacion asignada correctamente",
                    Datos = recomendacion
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Mensaje = "Error al asignar recomendacion",
                    Detalle = ex.Message
                });
            }
        }

        [HttpGet("estudiante/{estudianteId}")]
        public async Task<IActionResult> ObtenerRecomendacionesEstudiante(int estudianteId)
        {
            try
            {
                var recomendaciones = await _recomendacionService.ObtenerRecomendacionesEstudianteAsync(estudianteId);
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Recomendaciones obtenidas",
                    Datos = recomendaciones
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Mensaje = "Error al obtener recomendaciones",
                    Detalle = ex.Message
                });
            }
        }

        [HttpGet("vigentes")]
        public async Task<IActionResult> ObtenerRecomendacionesVigentes()
        {
            try
            {
                var recomendaciones = await _recomendacionService.ObtenerRecomendacionesVigentesAsync();
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Recomendaciones vigentes obtenidas",
                    Datos = recomendaciones
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Mensaje = "Error al obtener recomendaciones",
                    Detalle = ex.Message
                });
            }
        }

        [HttpDelete("{recomendacionId}")]
        public async Task<IActionResult> EliminarRecomendacion(int recomendacionId)
        {
            try
            {
                await _recomendacionService.EliminarRecomendacionAsync(recomendacionId);
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Recomendacion eliminada correctamente"
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Mensaje = "Error al eliminar recomendacion",
                    Detalle = ex.Message
                });
            }
        }
    }
}