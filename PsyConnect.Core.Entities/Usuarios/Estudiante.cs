using System;
using System.Collections.Generic;

namespace PsyConnect.Core.Entities.Usuarios
{
    public class Estudiante
    {
        public int EstudianteID { get; set; }
        public int UsuarioID { get; set; }
        public string Matrícula { get; set; }
        public string Carrera { get; set; }
        public int Semestre { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Género { get; set; }
        public string Dirección { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Citas.Cita> Citas { get; set; } = new List<Citas.Cita>();
        public virtual ICollection<Respuestas.RespuestaTest> RespuestasTest { get; set; } = new List<Respuestas.RespuestaTest>();
        public virtual ICollection<Certificados.Certificado> Certificados { get; set; } = new List<Certificados.Certificado>();
        public virtual ICollection<Recomendaciones.RecomendacionPersonalizada> RecomendacionesPersonalizadas { get; set; } = new List<Recomendaciones.RecomendacionPersonalizada>();
        public virtual ICollection<Auditoría.Historico> Historicos { get; set; } = new List<Auditoría.Historico>();
    }
}
