using System;
using System.Collections.Generic;
using PsyConnect.Core.Entities.Usuarios;
using PsyConnect.Core.Entities.Respuestas;

namespace PsyConnect.Core.Entities.Resultados
{
    public class ResultadoInterpretacion
    {
        public int ResultadoID { get; set; }
        public int RespuestaID { get; set; }
        public string Interpretacion { get; set; }
        public string Recomendacion { get; set; }
        public string Nivel { get; set; }
        public DateTime? FechaEvaluacion { get; set; }
        public int? PsicologoID { get; set; }
        public virtual RespuestaTest RespuestaTest { get; set; }
        public virtual Psicologo Psicologo { get; set; }
        public virtual ICollection<Recomendaciones.RecomendacionPersonalizada> RecomendacionesPersonalizadas { get; set; } = new List<Recomendaciones.RecomendacionPersonalizada>();
    }
}