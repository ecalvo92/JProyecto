namespace JApi.Models
{
    public class Usuario
    {
        public long Consecutivo { get; set; }
        public string Identificacion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Contrasenna { get; set; } = string.Empty;
    }
}
