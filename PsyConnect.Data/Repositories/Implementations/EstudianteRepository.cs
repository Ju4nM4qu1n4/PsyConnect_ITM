using Microsoft.EntityFrameworkCore;
using PsyConnect.Core.Entities.Usuarios;
using PsyConnect.Data.Context;
using PsyConnect.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsyConnect.Data.Repositories.Implementations
{
    public class EstudianteRepository : Repository<Estudiante>, IEstudianteRepository
    {
        public EstudianteRepository(PsyConnectContext context) : base(context)
        {
        }

        public async Task<Estudiante> GetEstudiantePorMatriculaAsync(string matricula)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Matricula == matricula);
        }

        public async Task<Estudiante> GetEstudiantePorUsuarioAsync(int usuarioId)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.UsuarioID == usuarioId);
        }

        public async Task<IEnumerable<Estudiante>> GetEstudiantesPorCarreraAsync(string carrera)
        {
            return await _dbSet.Where(e => e.Carrera == carrera).ToListAsync();
        }

        public async Task<IEnumerable<Estudiante>> GetEstudiantesPorSemestreAsync(int semestre)
        {
            return await _dbSet.Where(e => e.Semestre == semestre).ToListAsync();
        }

        public async Task<IEnumerable<Estudiante>> GetEstudiantesConResultadosAsync()
        {
            return await _dbSet
                .Include(e => e.RespuestasTest)
                .Where(e => e.RespuestasTest.Any())
                .ToListAsync();
        }
    }
}