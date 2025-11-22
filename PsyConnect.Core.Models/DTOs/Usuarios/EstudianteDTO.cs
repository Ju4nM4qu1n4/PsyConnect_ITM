namespace PsyConnect.Core.Models.DTOs.Usuarios
{
    public class EstudianteDTO
    {
        public int EstudianteID { get; set; }
        public int UsuarioID { get; set; }
        public string Matricula { get; set; }
        public string Carrera { get; set; }
        public int Semestre { get; set; }
        public string Genero { get; set; }
        public UsuarioDTO Usuario { get; set; }
    }
}
