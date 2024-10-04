using JWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace JWeb.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult CrearCuenta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearCuenta(Usuario model)
        {
            return View();
        }


        public IActionResult InicioSesion()
        {
            return View();
        }

        public IActionResult RecuperarAcceso()
        {
            return View();
        }

        
    }
}
