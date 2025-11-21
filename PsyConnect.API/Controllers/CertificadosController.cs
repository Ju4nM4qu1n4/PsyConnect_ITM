using PsyConnect.Business.Services.Certificados;
using PsyConnect.Core.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PsyConnect.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CertificadosController : ControllerBase
    {
        private readonly ICertificadoService _certificadoService;

        public CertificadosController(ICertificadoService certificadoService)
        {
            _certificadoService = certificadoService;
        }

        [HttpPost("generar-test/{respuestaId}")]
        public async Task<IActionResult> GenerarCertificadoTest(int respuestaId)
        {
            try
            {
                var certificado = await _certificadoService.GenerarCertificadoTestAsync(respuestaId);
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Certificado de test generado correctamente",
                    Datos = certificado
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Mensaje = "Error al generar certificado",
                    Detalle = ex.Message
                });
            }
        }

        [HttpPost("generar-cita/{citaId}")]
        public async Task<IActionResult> GenerarCertificadoCita(int citaId)
        {
            try
            {
                var certificado = await _certificadoService.GenerarCertificadoCitaAsync(citaId);
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Certificado de cita generado correctamente",
                    Datos = certificado
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Mensaje = "Error al generar certificado",
                    Detalle = ex.Message
                });
            }
        }

        [HttpGet("estudiante/{estudianteId}")]
        public async Task<IActionResult> ObtenerCertificadosEstudiante(int estudianteId)
        {
            try
            {
                var certificados = await _certificadoService.ObtenerCertificadosEstudianteAsync(estudianteId);
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Certificados obtenidos",
                    Datos = certificados
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Mensaje = "Error al obtener certificados",
                    Detalle = ex.Message
                });
            }
        }

        [HttpPost("{certificadoId}/descargar")]
        public async Task<IActionResult> MarcarComoDescargado(int certificadoId)
        {
            try
            {
                await _certificadoService.MarcarComoDescargadoAsync(certificadoId);
                return Ok(new SuccessResponse<object>
                {
                    Mensaje = "Certificado marcado como descargado"
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Mensaje = "Error al marcar certificado",
                    Detalle = ex.Message
                });
            }
        }
    }
}