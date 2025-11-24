namespace PsyConnect.Core.Models.Responses
{
    // Response del test con preguntas
    public class TestResponse
    {
        public int TestID { get; set; }
        public string NombreTest { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int CantidadPreguntas { get; set; }
        public int TiempoEstimado { get; set; }
        public List<PreguntaResponse> Preguntas { get; set; } = new();
    }

    public class PreguntaResponse
    {
        public int PreguntaID { get; set; }
        public int Numero { get; set; }
        public string Texto { get; set; } = string.Empty;
        public List<OpcionResponse> Opciones { get; set; } = new();
    }

    public class OpcionResponse
    {
        public int OpcionID { get; set; }
        public string Texto { get; set; } = string.Empty;
    }

    // Response del resultado
    public class ResultadoTestResponse
    {
        public int RespuestaID { get; set; }
        public int PuntajeTotal { get; set; }
        public string Nivel { get; set; } = string.Empty;
        public string Interpretacion { get; set; } = string.Empty;
        public string Recomendacion { get; set; } = string.Empty;
        public DateTime FechaRealizacion { get; set; }
    }

    // Response de historial
    public class HistorialTestResponse
    {
        public int RespuestaID { get; set; }
        public string NombreTest { get; set; } = string.Empty;
        public DateTime FechaRealizacion { get; set; }
        public int? PuntajeTotal { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string? Nivel { get; set; }
    }
}