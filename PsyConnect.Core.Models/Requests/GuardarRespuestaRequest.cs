using System.Collections.Generic;

namespace PsyConnect.Core.Models.Requests
{
    public class GuardarRespuestaRequest
    {
        public int RespuestaId { get; set; }
        public List<RespuestaDetalleRequest> Respuestas { get; set; } = new();
    }

    public class RespuestaDetalleRequest
    {
        public int PreguntaId { get; set; }
        public int OpcionSeleccionada { get; set; }
    }
}