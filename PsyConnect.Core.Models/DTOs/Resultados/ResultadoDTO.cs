using System;

namespace PsyConnect.Core.Models.DTOs.Resultados
{
    public class ResultadoDTO
    {
        public int ResultadoID { get; set; }
        public int RespuestaID { get; set; }
        public string Nivel { get; set; }
        public string Interpretacion { get; set; }
        public string Recomendacion { get; set; }
        public DateTime FechaEvaluacion { get; set; }
    }
}