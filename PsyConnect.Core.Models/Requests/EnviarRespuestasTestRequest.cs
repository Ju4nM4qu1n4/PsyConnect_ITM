namespace PsyConnect.Core.Models.Requests
{
    // Request para enviar respuestas del test
    public class EnviarRespuestasTestRequest
    {
        public int TestID { get; set; }
        public int EstudianteID { get; set; }
        public List<RespuestaPregunta> Respuestas { get; set; } = new();
    }

    public class RespuestaPregunta
    {
        public int PreguntaID { get; set; }
        public int OpcionID { get; set; }
    }
}