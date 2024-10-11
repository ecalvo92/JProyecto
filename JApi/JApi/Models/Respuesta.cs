namespace JApi.Models
{
    public class Respuesta
    {
        public int Codigo { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public object? Contenido { get; set; }
    }
}
