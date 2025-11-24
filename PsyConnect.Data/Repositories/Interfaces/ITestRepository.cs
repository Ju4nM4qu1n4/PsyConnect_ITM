using PsyConnect.Core.Entities.Tests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsyConnect.Data.Repositories.Interfaces
{
    public interface ITestRepository : IRepository<Test>
    {
        Task<IEnumerable<Test>> GetTestsActivosAsync();
        Task<IEnumerable<Test>> GetTestsPorTipoAsync(int tipoTestId);
        Task<IEnumerable<Test>> GetTestsPorPsicologoAsync(int psicologoId);
        Task<IEnumerable<Test>> GetTestsPorModalidadAsync(int modalidadTestId);
        Task<Test?> GetByIdAsync(int testId);
        Task<Test?> GetTestConPreguntasAsync(int testId);
        
       
    }
}