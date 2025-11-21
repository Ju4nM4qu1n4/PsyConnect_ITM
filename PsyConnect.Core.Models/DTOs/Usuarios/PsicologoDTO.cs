namespace PsyConnect.Core.Models.DTOs.Usuarios
{
    public class PsicologoDTO
    {
        public int PsicólogoID { get; set; }
        public int UsuarioID { get; set; }
        public string Especialidad { get; set; }
        public string Licencia { get; set; }
        public string SedeAsignada { get; set; }
        public UsuarioDTO Usuario { get; set; }
    }
}