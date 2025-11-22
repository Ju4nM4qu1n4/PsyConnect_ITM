using PsyConnect.Core.Entities.Usuarios;
using System.Threading.Tasks;

namespace PsyConnect.Data.Repositories.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> GetUsuarioPorEmailAsync(string email);
        Task<Usuario> GetUsuarioPorEmailYContrasenaAsync(string email, string contrasena);
    }
}