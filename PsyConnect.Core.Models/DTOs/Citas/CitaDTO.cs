using System;

namespace PsyConnect.Core.Models.DTOs.Citas
{
    public class CitaDTO
    {
        public int CitaID { get; set; }
        public int EstudianteID { get; set; }
        public int PsicólogoID { get; set; }
        public DateTime FechaHora { get; set; }
        public int Duración { get; set; }
        public string ModalidadNombre { get; set; }
        public string EstadoNombre { get; set; }
        public string Ubicación { get; set; }
        public string EnlaceTeams { get; set; }
        public string NotasEstudiante { get; set; }
    }
}