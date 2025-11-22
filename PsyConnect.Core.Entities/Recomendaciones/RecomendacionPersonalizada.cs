using System;
using PsyConnect.Core.Entities.Usuarios;
using PsyConnect.Core.Entities.Resultados;

namespace PsyConnect.Core.Entities.Recomendaciones
{
    public class RecomendacionPersonalizada
    {
        public int RecomendacionID { get; set; }
        public int EstudianteID { get; set; }
        public int PsicologoID { get; set; }
        public int? ResultadoID { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string TipoRecurso { get; set; }
        public string URL { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public bool Vigente { get; set; }
        public virtual Estudiante Estudiante { get; set; }
        public virtual Psicologo Psicologo { get; set; }
        public virtual ResultadoInterpretacion ResultadoInterpretacion { get; set; }
    }
}