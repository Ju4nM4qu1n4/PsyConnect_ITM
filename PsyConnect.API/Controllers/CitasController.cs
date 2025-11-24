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
        [HttpGet("mis-citas")]
        public async Task<IActionResult> ObtenerMisCitas()
        {
            try
            {
                // Obtener el UsuarioID desde el claim del JWT
                var usuarioIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(usuarioIdClaim))
                {
                    return Unauthorized(new ErrorResponse
                    {
                        Mensaje = "No se pudo identificar al usuario",
                        Detalle = "Token inválido o usuario no autenticado"
                    });
                }

                var usuarioId = int.Parse(usuarioIdClaim);

                // Aquí necesitas obtener el EstudianteID desde el UsuarioID
                // Opción 1: Si agregaste EstudianteId al token (recomendado - ver abajo)
                var estudianteIdClaim = User.FindFirst("EstudianteId")?.Value;

                if (string.IsNullOrEmpty(estudianteIdClaim))
                {
                    return Unauthorized(new ErrorResponse
                    {
                        Mensaje = "Usuario no es un estudiante",
                        Detalle = "Solo los estudiantes pueden acceder a esta funcionalidad"
                    });
                }

                var estudianteId = int.Parse(estudianteIdClaim);
                var citas = await _citaService.ObtenerCitasPorEstudianteAsync(estudianteId);

                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Citas obtenidas correctamente",
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