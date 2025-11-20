using PsyConnect.Core.Entities.Certificados;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsyConnect.Data.Repositories.Interfaces
{
    public interface ICertificadoRepository : IRepository<Certificado>
    {
        Task<IEnumerable<Certificado>> GetCertificadosPorEstudianteAsync(int estudianteId);
        Task<IEnumerable<Certificado>> GetCertificadosDescargadosAsync();
        Task<IEnumerable<Certificado>> GetCertificadosPorTipoAsync(string tipo);
    }
}