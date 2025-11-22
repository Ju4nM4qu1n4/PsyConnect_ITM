namespace PsyConnect.Core.Models.Requests
{
    public class CambiarContrasenaRequest
    {
        public int UsuarioId { get; set; }
        public string ContrasenaActual { get; set; }
        public string NuevaContrasena { get; set; }
    }
}