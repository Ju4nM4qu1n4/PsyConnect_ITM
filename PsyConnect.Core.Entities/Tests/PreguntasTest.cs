using System.Collections.Generic;

namespace PsyConnect.Core.Entities.Tests
{
    public class PreguntaTest
    {
        public int PreguntaID { get; set; }
        public int TestID { get; set; }
        public int Numero { get; set; }
        public string Texto { get; set; }
        public string Tipo { get; set; } 
        public int? Puntaje { get; set; }
        public virtual Test Test { get; set; }
        public virtual ICollection<OpcionRespuesta> OpcionesRespuesta { get; set; } = new List<OpcionRespuesta>();
        public virtual ICollection<Respuestas.DetalleRespuestaTest> DetallesRespuesta { get; set; } = new List<Respuestas.DetalleRespuestaTest>();
    }
}