namespace PsyConnect.Core.Models.Responses
{
    public class AuthResponse
    {
        public int UsuarioID { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoUsuario { get; set; }
        public string Token { get; set; }
    }
}