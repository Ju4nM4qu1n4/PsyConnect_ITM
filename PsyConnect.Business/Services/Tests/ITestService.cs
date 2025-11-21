using PsyConnect.Core.Models.DTOs.Tests;
using PsyConnect.Core.Models.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsyConnect.Business.Services.Tests
{
    public interface ITestService
    {
        Task<TestDTO> ObtenerTestAsync(int testId);
        Task<IEnumerable<TestDTO>> ObtenerTestsActivosAsync();
        Task<IEnumerable<TestDTO>> ObtenerTestsPorTipoAsync(int tipoTestId);
        Task IniciarRespuestaTestAsync(int estudianteId, int testId);
        Task GuardarRespuestaAsync(GuardarRespuestaRequest request);
        Task CompletarTestAsync(int respuestaId);
    }
}
