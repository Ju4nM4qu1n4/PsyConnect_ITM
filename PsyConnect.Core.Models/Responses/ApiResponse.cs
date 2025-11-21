using System.Collections.Generic;

namespace PsyConnect.Core.Models.Responses
{
    public class ApiResponse<T>
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
        public T Datos { get; set; }
        public List<string> Errores { get; set; } = new();
    }
}