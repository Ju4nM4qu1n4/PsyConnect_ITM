using Microsoft.EntityFrameworkCore;
using PsyConnect.Core.Entities.Certificados;
using PsyConnect.Data.Context;
using PsyConnect.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsyConnect.Data.Repositories.Implementations
{
    public class CertificadoRepository : Repository<Certificado>, ICertificadoRepository
    {
        public CertificadoRepository(PsyConnectContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Certificado>> GetCertificadosPorEstudianteAsync(int estudianteId)
        {
            return await _dbSet
                .Where(c => c.EstudianteID == estudianteId)
                .OrderByDescending(c => c.FechaGeneración)
                .ToListAsync();
        }

        public async Task<IEnumerable<Certificado>> GetCertificadosDescargadosAsync()
        {
            return await _dbSet
                .Where(c => c.Descargado)
                .Include(c => c.Estudiante)
                .OrderByDescending(c => c.FechaDescarga)
                .ToListAsync();
        }

        public async Task<IEnumerable<Certificado>> GetCertificadosPorTipoAsync(string tipo)
        {
            return await _dbSet
                .Where(c => c.TipoCertificado == tipo)
                .Include(c => c.Estudiante)
                .ToListAsync();
        }
    }
}