using Microsoft.EntityFrameworkCore;
using PsyConnect.Core.Entities.Usuarios;
using PsyConnect.Data.Context;
using PsyConnect.Data.Repositories.Interfaces;
using System.Threading.Tasks;

namespace PsyConnect.Data.Repositories.Implementations
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(PsyConnectContext context) : base(context)
        {
        }

        public async Task<Usuario> GetUsuarioPorEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario> GetUsuarioPorEmailYContraseñaAsync(string email, string contraseña)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email && u.Contraseña == contraseña);
        }
    }
}