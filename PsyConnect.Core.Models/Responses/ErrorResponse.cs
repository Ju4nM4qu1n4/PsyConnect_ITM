using System;
using System.Collections.Generic;

namespace PsyConnect.Core.Models.Responses
{
    public class ErrorResponse
    {
        public string Mensaje { get; set; }
        public string Detalle { get; set; }
        public DateTime Timestamp { get; set; }
        public List<string> Errores { get; set; } = new();
    }
}