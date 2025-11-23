using PsyConnect.Business.Services.Citas;
using PsyConnect.Core.Models.Requests;
using PsyConnect.Core.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PsyConnect.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly ICitaService _citaService;

        public CitasController(ICitaService citaService)
        {
            _citaService = citaService;
        }

        [HttpPost("agendar")]
        public async Task<IActionResult> AgendarCita([FromBody] AgendarCitaRequest request)
        {
            try
            {
                var cita = await _citaService.AgendarCitaAsync(request);
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Cita agendada correctamente",
                    Datos = cita
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Mensaje = "Error al agendar cita",
                    Detalle = ex.Message
                });
            }
        }

        [HttpGet("{citaId}")]
        public async Task<IActionResult> ObtenerCita(int citaId)
        {
            try
            {
                var cita = await _citaService.ObtenerCitaAsync(citaId);
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Cita obtenida",
                    Datos = cita
                });
            }
            catch (System.Exception ex)
            {
                return NotFound(new ErrorResponse
                {
                    Mensaje = "Cita no encontrada",
                    Detalle = ex.Message
                });
            }
        }



        // ← NUEVO ENDPOINT: Obtener citas del estudiante actual (usa JWT)
        [HttpGet("estudiante")]
        public async Task<IActionResult> ObtenerMisCitas()
        {
            try
            {
                // Obtener el EstudianteID desde el claim del JWT
                var estudianteIdClaim = User.FindFirst("EstudianteId")?.Value;

                if (string.IsNullOrEmpty(estudianteIdClaim))
                {
                    return Unauthorized(new ErrorResponse
                    {
                        Mensaje = "No se pudo identificar al estudiante",
                        Detalle = "Token inválido o estudiante no autenticado"
                    });
                }

                var estudianteId = int.Parse(estudianteIdClaim);
                var citas = await _citaService.ObtenerCitasPorEstudianteAsync(estudianteId);

                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Citas obtenidas",
                    Datos = citas
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Mensaje = "Error al obtener citas",
                    Detalle = ex.Message
                });
            }
        }

        [HttpGet("estudiante/{estudianteId}")]
        public async Task<IActionResult> ObtenerCitasPorEstudiante(int estudianteId)
        {
            try
            {
                var citas = await _citaService.ObtenerCitasPorEstudianteAsync(estudianteId);
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Citas obtenidas",
                    Datos = citas
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Mensaje = "Error al obtener citas",
                    Detalle = ex.Message
                });
            }
        }

        [HttpGet("psicologo/{psicologoId}")]
        public async Task<IActionResult> ObtenerCitasPorPsicologo(int psicologoId)
        {
            try
            {
                var citas = await _citaService.ObtenerCitasPorPsicologoAsync(psicologoId);
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Citas obtenidas",
                    Datos = citas
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Mensaje = "Error al obtener citas",
                    Detalle = ex.Message
                });
            }
        }

        [HttpGet("proximas")]
        public async Task<IActionResult> ObtenerCitasProximas()
        {
            try
            {
                var citas = await _citaService.ObtenerCitasProximasAsync();
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Citas proximas obtenidas",
                    Datos = citas
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Mensaje = "Error al obtener citas",
                    Detalle = ex.Message
                });
            }
        }

        [HttpDelete("{citaId}")]
        public async Task<IActionResult> CancelarCita(int citaId)
        {
            try
            {
                await _citaService.CancelarCitaAsync(citaId);
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Cita cancelada correctamente"
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Mensaje = "Error al cancelar cita",
                    Detalle = ex.Message
                });
            }
        }

        [HttpPut("{citaId}/estado/{nuevoEstado}")]
        public async Task<IActionResult> ActualizarEstadoCita(int citaId, int nuevoEstado)
        {
            try
            {
                await _citaService.ActualizarEstadoCitaAsync(citaId, nuevoEstado);
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Estado de cita actualizado"
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Mensaje = "Error al actualizar estado",
                    Detalle = ex.Message
                });
            }
        }
    }
}