using System;

namespace PsyConnect.Core.Entities.Usuarios
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoUsuario { get; set; } 
        public string Telefono { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Estado { get; set; }
        public DateTime? UltimoAcceso { get; set; }
    }
}