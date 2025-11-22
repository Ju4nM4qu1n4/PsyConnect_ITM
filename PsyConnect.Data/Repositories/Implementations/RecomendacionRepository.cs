using Microsoft.EntityFrameworkCore;
using PsyConnect.Core.Entities.Recomendaciones;
using PsyConnect.Data.Context;
using PsyConnect.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsyConnect.Data.Repositories.Implementations
{
    public class RecomendacionRepository : Repository<RecomendacionPersonalizada>, IRecomendacionRepository
    {
        public RecomendacionRepository(PsyConnectContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RecomendacionPersonalizada>> GetRecomendacionesPorEstudianteAsync(int estudianteId)
        {
            return await _dbSet
                .Where(r => r.EstudianteID == estudianteId && r.Vigente)
                .Include(r => r.Psicologo)
                .ToListAsync();
        }

        public async Task<IEnumerable<RecomendacionPersonalizada>> GetRecomendacionesVigentesAsync()
        {
            return await _dbSet
                .Where(r => r.Vigente)
                .Include(r => r.Estudiante)
                .Include(r => r.Psicologo)
                .ToListAsync();
        }

        public async Task<IEnumerable<RecomendacionPersonalizada>> GetRecomendacionesPorPsicologoAsync(int psicologoId)
        {
            return await _dbSet
                .Where(r => r.PsicologoID == psicologoId)
                .Include(r => r.Estudiante)
                .ToListAsync();
        }

        public async Task<IEnumerable<RecomendacionPersonalizada>> GetRecomendacionesPorResultadoAsync(int resultadoId)
        {
            return await _dbSet
                .Where(r => r.ResultadoID == resultadoId && r.Vigente)
                .ToListAsync();
        }
    }
}