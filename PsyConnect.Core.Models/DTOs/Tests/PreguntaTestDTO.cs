using System.Collections.Generic;

namespace PsyConnect.Core.Models.DTOs.Tests
{
    public class PreguntaTestDTO
    {
        public int PreguntaID { get; set; }
        public int Numero { get; set; }
        public string Texto { get; set; }
        public string Tipo { get; set; }
        public List<OpcionRespuestaDTO> Opciones { get; set; } = new();
    }
}