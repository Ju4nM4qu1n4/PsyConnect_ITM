using System.Collections.Generic;

namespace PsyConnect.Core.Entities.Citas
{
    public class EstadoCita
    {
        public int EstadoID { get; set; }
        public string Nombre { get; set; } 
        public virtual ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }
}