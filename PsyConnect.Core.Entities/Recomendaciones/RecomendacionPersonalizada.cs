using System;
using PsyConnect.Core.Entities.Usuarios;
using PsyConnect.Core.Entities.Resultados;

namespace PsyConnect.Core.Entities.Recomendaciones
{
    public class RecomendacionPersonalizada
    {
        public int RecomendacionID { get; set; }
        public int EstudianteID { get; set; }
        public int PsicólogoID { get; set; }
        public int? ResultadoID { get; set; }
        public string Título { get; set; }
        public string Descripción { get; set; }
        public string TipoRecurso { get; set; }
        public string URL { get; set; }
        public DateTime FechaAsignación { get; set; }
        public bool Vigente { get; set; }
        public virtual Estudiante Estudiante { get; set; }
        public virtual Psicólogo Psicólogo { get; set; }
        public virtual ResultadoInterpretacion ResultadoInterpretacion { get; set; }
    }
}