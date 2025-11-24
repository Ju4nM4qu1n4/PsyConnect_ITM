using PsyConnect.Core.Entities.Tests;
using PsyConnect.Core.Entities.Usuarios;

namespace PsyConnect.Core.Models
{
    public class Test
    {
        public int TestID { get; set; }
        public int TipoTestID { get; set; }
        public int ModalidadTestID { get; set; }
        public string NombreTest { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int PsicologoID { get; set; }
        public int CantidadPreguntas { get; set; }
        public int TiempoEstimado { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Navegación
        public virtual TipoTest? TipoTest { get; set; }
        public virtual ModalidadTest? ModalidadTest { get; set; }
        public virtual Psicologo? Psicologo { get; set; }
        public virtual ICollection<PreguntaTest> Preguntas { get; set; } = new List<PreguntaTest>();
    }

    public class TipoTest
    {
        public int TipoTestID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; }
    }

    public class ModalidadTest
    {
        public int ModalidadTestID { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}