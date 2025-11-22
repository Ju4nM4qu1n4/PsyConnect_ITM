namespace PsyConnect.Core.Models.Requests
{
    public class AsignarRecomendacionRequest
    {
        public int EstudianteId { get; set; }
        public int PsicologoId { get; set; }
        public int? ResultadoId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string TipoRecurso { get; set; }
        public string URL { get; set; }
    }
}