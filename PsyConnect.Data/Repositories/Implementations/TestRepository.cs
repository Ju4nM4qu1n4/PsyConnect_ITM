using Microsoft.EntityFrameworkCore;
using PsyConnect.Core.Entities.Tests;
using PsyConnect.Data.Context;
using PsyConnect.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsyConnect.Data.Repositories.Implementations
{
    public class TestRepository : Repository<Test>, ITestRepository
    {
        public TestRepository(PsyConnectContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Test>> GetTestsActivosAsync()
        {
            return await _dbSet
                .Where(t => t.Activo)
                .Include(t => t.TipoTest)
                .Include(t => t.ModalidadTest)
                .ToListAsync();
        }

        public async Task<IEnumerable<Test>> GetTestsPorTipoAsync(int tipoTestId)
        {
            return await _dbSet
                .Where(t => t.TipoTestID == tipoTestId && t.Activo)
                .Include(t => t.ModalidadTest)
                .Include(t => t.Psicologo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Test>> GetTestsPorPsicologoAsync(int psicologoId)
        {
            return await _dbSet
                .Where(t => t.PsicologoID == psicologoId)
                .Include(t => t.TipoTest)
                .Include(t => t.ModalidadTest)
                .ToListAsync();
        }

        public async Task<IEnumerable<Test>> GetTestsPorModalidadAsync(int modalidadTestId)
        {
            return await _dbSet
                .Where(t => t.ModalidadTestID == modalidadTestId && t.Activo)
                .Include(t => t.TipoTest)
                .ToListAsync();
        }

        public async Task<Test?> GetTestConPreguntasAsync(int testId)
        {
            return await _dbSet
                .Include(t => t.PreguntasTest)
                    .ThenInclude(p => p.OpcionesRespuesta)
                .Include(t => t.TipoTest)
                .Include(t => t.ModalidadTest)
                .FirstOrDefaultAsync(t => t.TestID == testId && t.Activo);
        }
    }
}