using Microsoft.EntityFrameworkCore;
using PsyConnect.Core.Entities.Resultados;
using PsyConnect.Data.Context;
using PsyConnect.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsyConnect.Data.Repositories.Implementations
{
    public class ResultadoRepository : Repository<ResultadoInterpretacion>, IResultadoRepository
    {
        public ResultadoRepository(PsyConnectContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ResultadoInterpretacion>> GetResultadosPorEstudianteAsync(int estudianteId)
        {
            return await _dbSet
                .Where(r => r.RespuestaTest.EstudianteID == estudianteId)
                .Include(r => r.RespuestaTest)
                .ToListAsync();
        }

        public async Task<IEnumerable<ResultadoInterpretacion>> GetResultadosPorNivelAsync(string nivel)
        {
            return await _dbSet
                .Where(r => r.Nivel == nivel)
                .Include(r => r.RespuestaTest)
                .ToListAsync();
        }

        public async Task<IEnumerable<ResultadoInterpretacion>> GetResultadosEvaluadosPorPsicologoAsync(int psicologoId)
        {
            return await _dbSet
                .Where(r => r.PsicólogoID == psicologoId)
                .Include(r => r.RespuestaTest)
                .ToListAsync();
        }

        public async Task<ResultadoInterpretacion> GetResultadoPorRespuestaAsync(int respuestaId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(r => r.RespuestaID == respuestaId);
        }
    }
}