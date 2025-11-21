using PsyConnect.Core.Models.DTOs.Usuarios;
using PsyConnect.Core.Models.Requests;
using PsyConnect.Core.Models.Responses;
using System.Threading.Tasks;

namespace PsyConnect.Business.Services.Usuarios
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> RegistrarEstudianteAsync(RegistrarEstudianteRequest request);
        Task<UsuarioDTO> RegistrarPsicologoAsync(RegistrarPsicologoRequest request);
        Task<AuthResponse> AutenticarAsync(LoginRequest request);
        Task<UsuarioDTO> ObtenerPerfilAsync(int usuarioId);
        Task CambiarContraseñaAsync(int usuarioId, string contraseñaActual, string nuevaContraseña);
    }
}