using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace PsyConnect.Core.Entities.Tests
{
    public class TipoTest
    {
        public int TipoTestID { get; set; }
        public string Nombre { get; set; }
        public string Descripción { get; set; }
        public bool Activo { get; set; }
        public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
    }
}