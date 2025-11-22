namespace PsyConnect.Core.Models.Requests
{
    public class RegistrarEstudianteRequest
    {
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Matricula { get; set; }
        public string Carrera { get; set; }
        public int Semestre { get; set; }
        public string Genero { get; set; }
        public string Direccion { get; set; }
    }
}