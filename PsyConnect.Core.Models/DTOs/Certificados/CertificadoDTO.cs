using System;

namespace PsyConnect.Core.Models.DTOs.Certificados
{
    public class CertificadoDTO
    {
        public int CertificadoID { get; set; }
        public int EstudianteID { get; set; }
        public string TipoCertificado { get; set; }
        public DateTime FechaGeneracion { get; set; }
        public string RutaArchivo { get; set; }
        public bool Descargado { get; set; }
        public DateTime? FechaDescarga { get; set; }
    }
}