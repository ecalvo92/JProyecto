namespace JApi.Models
{
    public class Producto
    {
        public long Consecutivo { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Inventario { get; set; }
        public string Imagen { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}
