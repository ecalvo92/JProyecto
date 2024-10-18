using JWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Inicio()
        {
            return View();
        }
    }
}
