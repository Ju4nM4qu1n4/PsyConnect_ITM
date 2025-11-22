using System;
using System.Collections.Generic;
using PsyConnect.Core.Entities.Usuarios;
using PsyConnect.Core.Entities.Tests;

namespace PsyConnect.Core.Entities.Respuestas
{
    public class RespuestaTest
    {
        public int RespuestaID { get; set; }
        public int EstudianteID { get; set; }
        public int TestID { get; set; }
        public int EstadoID { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public int? PuntajeTotal { get; set; }
        public virtual Estudiante Estudiante { get; set; }
        public virtual Test Test { get; set; }
        public virtual EstadoRespuestaTest EstadoRespuestaTest { get; set; }
        public virtual ICollection<DetalleRespuestaTest> DetallesRespuesta { get; set; } = new List<DetalleRespuestaTest>();
        public virtual Resultados.ResultadoInterpretacion ResultadoInterpretacion { get; set; }
        public virtual ICollection<Certificados.Certificado> Certificados { get; set; } = new List<Certificados.Certificado>();
    }
}