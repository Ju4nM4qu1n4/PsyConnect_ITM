namespace PsyConnect.Core.Models.Requests
{
    public class RegistrarPsicologoRequest
    {
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Cedula { get; set; }
        public string Especialidad { get; set; }
        public string Licencia { get; set; }
    }
}