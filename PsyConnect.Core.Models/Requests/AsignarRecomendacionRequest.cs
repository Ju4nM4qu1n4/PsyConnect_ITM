namespace PsyConnect.Core.Models.Requests
{
    public class AsignarRecomendacionRequest
    {
        public int EstudianteId { get; set; }
        public int PsicologoId { get; set; }
        public int? ResultadoId { get; set; }
        public string Título { get; set; }
        public string Descripción { get; set; }
        public string TipoRecurso { get; set; }
        public string URL { get; set; }
    }
}