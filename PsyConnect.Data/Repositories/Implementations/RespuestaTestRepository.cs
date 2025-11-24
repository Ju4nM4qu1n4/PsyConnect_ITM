using Microsoft.EntityFrameworkCore;
using PsyConnect.Core.Entities.Respuestas;
using PsyConnect.Data.Context;
using PsyConnect.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsyConnect.Data.Repositories.Implementations
{
    public class RespuestaTestRepository : Repository<RespuestaTest>, IRespuestaTestRepository
    {
        public RespuestaTestRepository(PsyConnectContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RespuestaTest>> GetRespuestasPorEstudianteAsync(int estudianteId)
        {
            return await _dbSet
                .Where(r => r.EstudianteID == estudianteId)
                .Include(r => r.Test)
                .Include(r => r.EstadoRespuestaTest)
                .ToListAsync();
        }

        public async Task<IEnumerable<RespuestaTest>> GetRespuestasPorTestAsync(int testId)
        {
            return await _dbSet
                .Where(r => r.TestID == testId)
                .Include(r => r.Estudiante)
                .ToListAsync();
        }

        public async Task<IEnumerable<RespuestaTest>> GetRespuestasCompletadasAsync()
        {
            return await _dbSet
                .Where(r => r.EstadoID == 4) // Evaluado
                .Include(r => r.Estudiante)
                .Include(r => r.Test)
                .ToListAsync();
        }

        public async Task<IEnumerable<RespuestaTest>> GetRespuestasEnProgresoAsync()
        {
            return await _dbSet
                .Where(r => r.EstadoID == 1) // En Progreso
                .Include(r => r.Estudiante)
                .Include(r => r.Test)
                .ToListAsync();
        }

        public async Task<RespuestaTest?> GetRespuestaConDetallesAsync(int respuestaId)
        {
            return await _dbSet
                .Include(r => r.DetallesRespuesta)
                .Include(r => r.Test)
                .Include(r => r.EstadoRespuestaTest)
                .FirstOrDefaultAsync(r => r.RespuestaID == respuestaId);
        }


        public async Task<int> CountRespuestasPorTestAsync(int testId)
        {
            return await _dbSet.CountAsync(r => r.TestID == testId);
        }

        Task<List<RespuestaTest>> IRespuestaTestRepository.GetRespuestasPorEstudianteAsync(int estudianteId)
        {
            throw new NotImplementedException();
        }
    }
}