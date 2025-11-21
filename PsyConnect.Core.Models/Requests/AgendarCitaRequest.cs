using System;

namespace PsyConnect.Core.Models.Requests
{
    public class AgendarCitaRequest
    {
        public int EstudianteId { get; set; }
        public int PsicologoId { get; set; }
        public int ModalidadId { get; set; }
        public DateTime FechaHora { get; set; }
        public string Ubicacion { get; set; }
        public string Notas { get; set; }
    }
}