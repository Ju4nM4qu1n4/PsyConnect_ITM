using System;
using PsyConnect.Core.Entities.Usuarios;
using PsyConnect.Core.Entities.Respuestas;
using PsyConnect.Core.Entities.Citas;

namespace PsyConnect.Core.Entities.Certificados
{
    public class Certificado
    {
        public int CertificadoID { get; set; }
        public int EstudianteID { get; set; }
        public int? RespuestaTestID { get; set; }
        public int? CitaID { get; set; }
        public string TipoCertificado { get; set; } 
        public DateTime FechaGeneración { get; set; }
        public string RutaArchivo { get; set; }
        public bool Descargado { get; set; }
        public DateTime? FechaDescarga { get; set; }
        public virtual Estudiante Estudiante { get; set; }
        public virtual RespuestaTest RespuestaTest { get; set; }
        public virtual Cita Cita { get; set; }
    }
}