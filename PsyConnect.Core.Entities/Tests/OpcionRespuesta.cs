using System.Collections.Generic;

namespace PsyConnect.Core.Entities.Tests
{
    public class OpcionRespuesta
    {
        public int OpcionID { get; set; }
        public int PreguntaID { get; set; }
        public string Texto { get; set; }
        public int Valor { get; set; }
        public virtual PreguntaTest PreguntaTest { get; set; }
        public virtual ICollection<Respuestas.DetalleRespuestaTest> DetallesRespuesta { get; set; } = new List<Respuestas.DetalleRespuestaTest>();
    }
}