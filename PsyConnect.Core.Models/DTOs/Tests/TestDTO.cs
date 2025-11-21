using System.Collections.Generic;

namespace PsyConnect.Core.Models.DTOs.Tests
{
    public class TestDTO
    {
        public int TestID { get; set; }
        public string NombreTest { get; set; }
        public string Descripción { get; set; }
        public string TipoTestNombre { get; set; }
        public string ModalidadNombre { get; set; }
        public int CantidadPreguntas { get; set; }
        public int TiempoEstimado { get; set; }
        public List<PreguntaTestDTO> Preguntas { get; set; } = new();
    }
}