using System;
using System.Collections.Generic;
using PsyConnect.Core.Entities.Usuarios;

namespace PsyConnect.Core.Entities.Citas
{
    public class Cita
    {
        public int CitaID { get; set; }
        public int EstudianteID { get; set; }
        public int PsicologoID { get; set; }
        public int ModalidadID { get; set; }
        public int EstadoID { get; set; }
        public DateTime FechaHora { get; set; }
        public int Duracion { get; set; }
        public string Ubicacion { get; set; }
        public string? EnlaceTeams { get; set; }
        public string NotasEstudiante { get; set; }
        public string ObservacionesPsicologo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public virtual Estudiante Estudiante { get; set; }
        public virtual Psicologo Psicologo { get; set; }
        public virtual ModalidadCita ModalidadCita { get; set; }
        public virtual EstadoCita EstadoCita { get; set; }
        public virtual ICollection<Certificados.Certificado> Certificados { get; set; } = new List<Certificados.Certificado>();
    }
}