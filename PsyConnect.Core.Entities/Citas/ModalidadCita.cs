using System.Collections.Generic;

namespace PsyConnect.Core.Entities.Citas
{
    public class ModalidadCita
    {
        public int ModalidadID { get; set; }
        public string Nombre { get; set; } 
        public string Descripcion { get; set; }
        public virtual ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }
}