using PsyConnect.Business.Services.Resultados;
using PsyConnect.Core.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PsyConnect.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultadosController : ControllerBase
    {
        private readonly IResultadoService _resultadoService;

        public ResultadosController(IResultadoService resultadoService)
        {
            _resultadoService = resultadoService;
        }

        [HttpPost("evaluar/{respuestaId}")]
        public async Task<IActionResult> EvaluarTest(int respuestaId)
        {
            try
            {
                var resultado = await _resultadoService.EvaluarTestAsync(respuestaId);
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Test evaluado correctamente",
                    Datos = resultado
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Mensaje = "Error al evaluar test",
                    Detalle = ex.Message
                });
            }
        }

        [HttpGet("{resultadoId}")]
        public async Task<IActionResult> ObtenerResultado(int resultadoId)
        {
            try
            {
                var resultado = await _resultadoService.ObtenerResultadoAsync(resultadoId);
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Resultado obtenido",
                    Datos = resultado
                });
            }
            catch (System.Exception ex)
            {
                return NotFound(new ErrorResponse
                {
                    Mensaje = "Resultado no encontrado",
                    Detalle = ex.Message
                });
            }
        }

        [HttpGet("estudiante/{estudianteId}")]
        public async Task<IActionResult> ObtenerResultadosPorEstudiante(int estudianteId)
        {
            try
            {
                var resultados = await _resultadoService.ObtenerResultadosPorEstudianteAsync(estudianteId);
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Resultados obtenidos",
                    Datos = resultados
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Mensaje = "Error al obtener resultados",
                    Detalle = ex.Message
                });
            }
        }
    }
}