using PsyConnect.Core.Models.DTOs.Certificados;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsyConnect.Business.Services.Certificados
{
    public interface ICertificadoService
    {
        Task<CertificadoDTO> GenerarCertificadoTestAsync(int respuestaId);
        Task<CertificadoDTO> GenerarCertificadoCitaAsync(int citaId);
        Task<IEnumerable<CertificadoDTO>> ObtenerCertificadosEstudianteAsync(int estudianteId);
        Task MarcarComoDescargadoAsync(int certificadoId);
    }
}