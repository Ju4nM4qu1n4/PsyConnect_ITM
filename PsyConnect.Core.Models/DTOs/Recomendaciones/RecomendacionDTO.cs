using System;

namespace PsyConnect.Core.Models.DTOs.Recomendaciones
{
    public class RecomendacionDTO
    {
        public int RecomendacionID { get; set; }
        public int EstudianteID { get; set; }
        public int PsicologoID { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string TipoRecurso { get; set; }
        public string URL { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public bool Vigente { get; set; }
    }
}