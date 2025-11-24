using PsyConnect.Core.Entities.Tests;
using PsyConnect.Core.Entities.Usuarios;

namespace PsyConnect.Core.Models
{
    public class RespuestaTest
    {
        public int RespuestaID { get; set; }
        public int EstudianteID { get; set; }
        public int TestID { get; set; }
        public int EstadoID { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public int? PuntajeTotal { get; set; }

        // Navegación
        public virtual Estudiante? Estudiante { get; set; }
        public virtual Test? Test { get; set; }
        public virtual EstadoRespuestaTest? Estado { get; set; }
        public virtual ICollection<DetalleRespuestaTest> Detalles { get; set; } = new List<DetalleRespuestaTest>();
        public virtual ResultadoInterpretacion? Resultado { get; set; }
    }

    public class DetalleRespuestaTest
    {
        public int DetalleID { get; set; }
        public int RespuestaID { get; set; }
        public int PreguntaID { get; set; }
        public int? OpcionSeleccionada { get; set; }
        public int? ValorRespuesta { get; set; }

        // Navegación
        public virtual RespuestaTest? RespuestaTest { get; set; }
        public virtual PreguntaTest? Pregunta { get; set; }
        public virtual OpcionRespuesta? Opcion { get; set; }
    }

    public class EstadoRespuestaTest
    {
        public int EstadoID { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }

    public class ResultadoInterpretacion
    {
        public int ResultadoID { get; set; }
        public int RespuestaID { get; set; }
        public string Interpretacion { get; set; } = string.Empty;
        public string Recomendacion { get; set; } = string.Empty;
        public string Nivel { get; set; } = string.Empty;
        public DateTime? FechaEvaluacion { get; set; }
        public int? PsicologoID { get; set; }

        // Navegación
        public virtual RespuestaTest? RespuestaTest { get; set; }
        public virtual Psicologo? Psicologo { get; set; }
    }
}