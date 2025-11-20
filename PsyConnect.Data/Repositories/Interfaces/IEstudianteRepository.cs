using PsyConnect.Core.Entities.Usuarios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsyConnect.Data.Repositories.Interfaces
{
    public interface IEstudianteRepository : IRepository<Estudiante>
    {
        Task<Estudiante> GetEstudiantePorMatriculaAsync(string matricula);
        Task<Estudiante> GetEstudiantePorUsuarioAsync(int usuarioId);
        Task<IEnumerable<Estudiante>> GetEstudiantesPorCarreraAsync(string carrera);
        Task<IEnumerable<Estudiante>> GetEstudiantesPorSemestreAsync(int semestre);
        Task<IEnumerable<Estudiante>> GetEstudiantesConResultadosAsync();
    }
}