using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace PsyConnect.Core.Entities.Usuarios
{
    public class Psicologo
    {
        public int PsicologoID { get; set; }
        public int UsuarioID { get; set; }
        public string Cedula { get; set; }
        public string Especialidad { get; set; }
        public string Licencia { get; set; }
        public TimeSpan HoraInicioJornada { get; set; }
        public TimeSpan HoraFinJornada { get; set; }
        public string SedeAsignada { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Citas.Cita> Citas { get; set; } = new List<Citas.Cita>();
        public virtual ICollection<Tests.Test> Tests { get; set; } = new List<Tests.Test>();
        public virtual ICollection<Resultados.ResultadoInterpretacion> ResultadosInterpretacion { get; set; } = new List<Resultados.ResultadoInterpretacion>();
        public virtual ICollection<Recomendaciones.RecomendacionPersonalizada> RecomendacionesPersonalizadas { get; set; } = new List<Recomendaciones.RecomendacionPersonalizada>();
        public virtual ICollection<Reportes.Reporte> Reportes { get; set; } = new List<Reportes.Reporte>();
    }
}