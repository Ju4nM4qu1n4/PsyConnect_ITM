namespace PsyConnect.Core.Models.Requests
{
    public class RegistrarEstudianteRequest
    {
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Teléfono { get; set; }
        public string Matrícula { get; set; }
        public string Carrera { get; set; }
        public int Semestre { get; set; }
        public string Género { get; set; }
        public string Dirección { get; set; }
    }
}