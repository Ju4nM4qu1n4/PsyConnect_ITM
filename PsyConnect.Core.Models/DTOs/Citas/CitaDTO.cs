using System;

namespace PsyConnect.Core.Models.DTOs.Citas
{
    public class CitaDTO
    {
        public int CitaID { get; set; }
        public int EstudianteID { get; set; }
        public int PsicologoID { get; set; }
        public DateTime FechaHora { get; set; }
        public int Duracion { get; set; }
        public string ModalidadNombre { get; set; }
        public string EstadoNombre { get; set; }
        public string Ubicacion { get; set; }
        public string? EnlaceTeams { get; set; }
        public string NotasEstudiante { get; set; }
    }
}