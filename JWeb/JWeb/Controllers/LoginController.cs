using Microsoft.AspNetCore.Mvc;

namespace JWeb.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult InicioSesion()
        {
            return View();
        }
    }
}
