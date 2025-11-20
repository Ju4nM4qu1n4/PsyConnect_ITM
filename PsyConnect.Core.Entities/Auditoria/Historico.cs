using System;
using PsyConnect.Core.Entities.Usuarios;

namespace PsyConnect.Core.Entities.Auditoría
{
    public class Historico
    {
        public int HistoricoID { get; set; }
        public int? EstudianteID { get; set; }
        public string TipoActividad { get; set; }
        public string Descripción { get; set; }
        public DateTime FechaActividad { get; set; }
        public virtual Estudiante Estudiante { get; set; }
    }
}