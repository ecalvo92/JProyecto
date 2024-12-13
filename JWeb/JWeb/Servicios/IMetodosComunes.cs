using JWeb.Models;

namespace JWeb.Servicios
{
    public interface IMetodosComunes
    {
        string Encrypt(string texto);

        List<Carrito> ConsultarCarritoServicio();
    }
}
