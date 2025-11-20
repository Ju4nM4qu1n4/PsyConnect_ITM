using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace PsyConnect.Core.Entities.Tests
{
    public class ModalidadTest
    {
        public int ModalidadTestID { get; set; }
        public string Nombre { get; set; } 
        public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
    }
}