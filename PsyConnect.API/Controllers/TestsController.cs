using PsyConnect.Business.Services.Tests;
using PsyConnect.Core.Models.Requests;
using PsyConnect.Core.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PsyConnect.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestsController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestsController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet("{testId}")]
        public async Task<IActionResult> ObtenerTest(int testId)
        {
            try
            {
                var test = await _testService.ObtenerTestAsync(testId);
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Test obtenido",
                    Datos = test
                });
            }
            catch (System.Exception ex)
            {
                return NotFound(new ErrorResponse
                {
                    Mensaje = "Test no encontrado",
                    Detalle = ex.Message
                });
            }
        }

        [HttpGet("activos")]
        public async Task<IActionResult> ObtenerTestsActivos()
        {
            try
            {
                var tests = await _testService.ObtenerTestsActivosAsync();
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Tests activos obtenidos",
                    Datos = tests
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Mensaje = "Error al obtener tests",
                    Detalle = ex.Message
                });
            }
        }

        [HttpGet("tipo/{tipoTestId}")]
        public async Task<IActionResult> ObtenerTestsPorTipo(int tipoTestId)
        {
            try
            {
                var tests = await _testService.ObtenerTestsPorTipoAsync(tipoTestId);
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Tests obtenidos",
                    Datos = tests
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Mensaje = "Error al obtener tests",
                    Detalle = ex.Message
                });
            }
        }

        [HttpPost("iniciar/{estudianteId}/{testId}")]
        public async Task<IActionResult> IniciarTest(int estudianteId, int testId)
        {
            try
            {
                await _testService.IniciarRespuestaTestAsync(estudianteId, testId);
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Test iniciado correctamente"
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Mensaje = "Error al iniciar test",
                    Detalle = ex.Message
                });
            }
        }

        [HttpPost("guardar-respuesta")]
        public async Task<IActionResult> GuardarRespuesta([FromBody] GuardarRespuestaRequest request)
        {
            try
            {
                await _testService.GuardarRespuestaAsync(request);
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Respuestas guardadas correctamente"
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Mensaje = "Error al guardar respuestas",
                    Detalle = ex.Message
                });
            }
        }

        [HttpPost("completar/{respuestaId}")]
        public async Task<IActionResult> CompletarTest(int respuestaId)
        {
            try
            {
                await _testService.CompletarTestAsync(respuestaId);
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Test completado correctamente"
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Mensaje = "Error al completar test",
                    Detalle = ex.Message
                });
            }
        }
    }
}