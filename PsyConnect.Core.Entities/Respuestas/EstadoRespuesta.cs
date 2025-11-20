using System.Collections.Generic;

namespace PsyConnect.Core.Entities.Respuestas
{
    public class EstadoRespuestaTest
    {
        public int EstadoID { get; set; }
        public string Nombre { get; set; }
        public virtual ICollection<RespuestaTest> RespuestasTest { get; set; } = new List<RespuestaTest>();
    }
}