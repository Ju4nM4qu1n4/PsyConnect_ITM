using PsyConnect.Core.Entities.Respuestas;
using PsyConnect.Core.Models.DTOs.Tests;
using PsyConnect.Core.Models.Requests;
using PsyConnect.Core.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsyConnect.Business.Services.Tests
{
    public interface ITestService
    {
        Task<TestDTO> ObtenerTestAsync(int testId);
        Task<IEnumerable<TestDTO>> ObtenerTestsActivosAsync();
        Task<IEnumerable<TestDTO>> ObtenerTestsPorTipoAsync(int tipoTestId);
        Task<RespuestaTest> IniciarTestAsync(int estudianteId, int testId);
        Task<ResultadoTestResponse> EnviarRespuestasAsync(EnviarRespuestasTestRequest request);
        Task<List<HistorialTestResponse>> ObtenerHistorialTestsAsync(int estudianteId);
        Task<ResultadoTestResponse> ObtenerResultadoAsync(int respuestaId);
        Task GuardarRespuestaAsync(GuardarRespuestaRequest request);
        Task CompletarTestAsync(int respuestaId);
    }
}