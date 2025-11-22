using System;

namespace PsyConnect.Core.Models.DTOs.Tests
{
    public class RespuestaTestDTO
    {
        public int RespuestaID { get; set; }
        public int EstudianteID { get; set; }
        public int TestID { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public int? PuntajeTotal { get; set; }
        public string EstadoNombre { get; set; }
    }
}