using Microsoft.AspNetCore.Mvc;

namespace JWeb.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult MostrarError()
        {
            return View("Error");
        }
    }
}
