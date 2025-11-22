namespace PsyConnect.Core.Models.DTOs.Usuarios
{
    public class UsuarioDTO
    {
        public int UsuarioID { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoUsuario { get; set; }
        public string Telefono { get; set; }
    }
}