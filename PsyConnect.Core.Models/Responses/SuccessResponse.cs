namespace PsyConnect.Core.Models.Responses
{
    public class SuccessResponse<T>
    {
        public bool Exitoso { get; set; } = true;
        public string Mensaje { get; set; }
        public T Datos { get; set; }
    }
}