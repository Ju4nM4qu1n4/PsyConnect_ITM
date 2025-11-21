using PsyConnect.Business.Services.Usuarios;
using PsyConnect.Core.Models.Requests;
using PsyConnect.Core.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PsyConnect.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("registro-estudiante")]
        public async Task<IActionResult> RegistrarEstudiante([FromBody] RegistrarEstudianteRequest request)
        {
            try
            {
                var usuario = await _usuarioService.RegistrarEstudianteAsync(request);
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Estudiante registrado correctamente",
                    Datos = usuario
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Mensaje = "Error al registrar estudiante",
                    Detalle = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [HttpPost("registro-psicologo")]
        public async Task<IActionResult> RegistrarPsicologo([FromBody] RegistrarPsicologoRequest request)
        {
            try
            {
                var usuario = await _usuarioService.RegistrarPsicologoAsync(request);
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Psicólogo registrado correctamente",
                    Datos = usuario
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Mensaje = "Error al registrar psicólogo",
                    Detalle = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var resultado = await _usuarioService.AutenticarAsync(request);
                return Ok(new SuccessResponse<AuthResponse>
                {
                    Mensaje = "Autenticación exitosa",
                    Datos = resultado
                });
            }
            catch (System.Exception ex)
            {
                return Unauthorized(new ErrorResponse
                {
                    Mensaje = "Error de autenticación",
                    Detalle = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [HttpGet("perfil/{usuarioId}")]
        public async Task<IActionResult> ObtenerPerfil(int usuarioId)
        {
            try
            {
                var perfil = await _usuarioService.ObtenerPerfilAsync(usuarioId);
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Perfil obtenido",
                    Datos = perfil
                });
            }
            catch (System.Exception ex)
            {
                return NotFound(new ErrorResponse
                {
                    Mensaje = "Usuario no encontrado",
                    Detalle = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [HttpPost("cambiar-contraseña")]
        public async Task<IActionResult> CambiarContraseña([FromBody] CambiarContraseñaRequest request)
        {
            try
            {
                await _usuarioService.CambiarContraseñaAsync(request.UsuarioId, request.ContrasenaActual, request.NuevaContrasena);
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Contraseña actualizada correctamente"
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Mensaje = "Error al cambiar contraseña",
                    Detalle = ex.InnerException?.Message ?? ex.Message
                });
            }
        }
    }
}