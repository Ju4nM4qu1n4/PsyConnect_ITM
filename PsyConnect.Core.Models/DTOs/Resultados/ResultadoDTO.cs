using System;

namespace PsyConnect.Core.Models.DTOs.Resultados
{
    public class ResultadoDTO
    {
        public int ResultadoID { get; set; }
        public int RespuestaID { get; set; }
        public string Nivel { get; set; }
        public string Interpretación { get; set; }
        public string Recomendación { get; set; }
        public DateTime FechaEvaluación { get; set; }
    }
}