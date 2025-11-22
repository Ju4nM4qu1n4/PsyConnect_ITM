using System;
using PsyConnect.Core.Entities.Usuarios;

namespace PsyConnect.Core.Entities.Reportes
{
    public class Reporte
    {
        public int ReporteID { get; set; }
        public int? PsicologoID { get; set; }
        public int? AdministradorID { get; set; }
        public string TipoReporte { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? CantidadCitas { get; set; }
        public int? CantidadTests { get; set; }
        public string TestsMasUtilizados { get; set; }
        public string NivelesRiesgo { get; set; }
        public DateTime FechaGeneracion { get; set; }
        public string RutaArchivo { get; set; }
        public virtual Psicologo Psicologo { get; set; }
        public virtual Usuario Administrador { get; set; }
    }
}