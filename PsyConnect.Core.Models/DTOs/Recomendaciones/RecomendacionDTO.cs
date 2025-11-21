using System;

namespace PsyConnect.Core.Models.DTOs.Recomendaciones
{
    public class RecomendacionDTO
    {
        public int RecomendacionID { get; set; }
        public int EstudianteID { get; set; }
        public int PsicólogoID { get; set; }
        public string Título { get; set; }
        public string Descripción { get; set; }
        public string TipoRecurso { get; set; }
        public string URL { get; set; }
        public DateTime FechaAsignación { get; set; }
        public bool Vigente { get; set; }
    }
}