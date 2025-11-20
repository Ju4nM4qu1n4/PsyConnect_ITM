using PsyConnect.Core.Entities.Tests;

namespace PsyConnect.Core.Entities.Respuestas
{
    public class DetalleRespuestaTest
    {
        public int DetalleID { get; set; }
        public int RespuestaID { get; set; }
        public int PreguntaID { get; set; }
        public int? OpcionSeleccionada { get; set; }
        public int? ValorRespuesta { get; set; }
        public virtual RespuestaTest RespuestaTest { get; set; }
        public virtual PreguntaTest PreguntaTest { get; set; }
        public virtual OpcionRespuesta OpcionRespuesta { get; set; }
    }
}