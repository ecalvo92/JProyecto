namespace JWeb.Models
{
    public class Carrito
    {
        public long ConsecutivoUsuario { get; set; }
        public long ConsecutivoProducto { get; set; }
        public int Unidades { get; set; }

        public long Consecutivo { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
    }
}
