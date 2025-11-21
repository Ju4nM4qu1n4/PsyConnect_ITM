namespace PsyConnect.Core.Models.Requests
{
    public class RegistrarPsicologoRequest
    {
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Teléfono { get; set; }
        public string Cédula { get; set; }
        public string Especialidad { get; set; }
        public string Licencia { get; set; }
    }
}