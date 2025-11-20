using System;
using System.Collections.Generic;
using PsyConnect.Core.Entities.Usuarios;

namespace PsyConnect.Core.Entities.Tests
{
    public class Test
    {
        public int TestID { get; set; }
        public int TipoTestID { get; set; }
        public int ModalidadTestID { get; set; }
        public string NombreTest { get; set; }
        public string Descripción { get; set; }
        public int PsicólogoID { get; set; }
        public int CantidadPreguntas { get; set; }
        public int TiempoEstimado { get; set; } 
        public bool Activo { get; set; }
        public DateTime FechaCreación { get; set; }
        public virtual TipoTest TipoTest { get; set; }
        public virtual ModalidadTest ModalidadTest { get; set; }
        public virtual Psicólogo Psicólogo { get; set; }
        public virtual ICollection<PreguntaTest> PreguntasTest { get; set; } = new List<PreguntaTest>();
        public virtual ICollection<Respuestas.RespuestaTest> RespuestasTest { get; set; } = new List<Respuestas.RespuestaTest>();
    }
}