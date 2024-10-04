using JWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace JWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _http;
        public LoginController(IHttpClientFactory http)
        {
            _http = http;
        }

        [HttpGet]
        public IActionResult CrearCuenta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearCuenta(Usuario model)
        {
            using (var client = _http.CreateClient())
            {
                string url = "https://localhost:7077/api/Login/CrearCuenta";
                JsonContent datos = JsonContent.Create(model);

                var response = client.PostAsync(url, datos).Result;

                if (response.IsSuccessStatusCode)
                { 
                    var result = response.Content.ReadFromJsonAsync<Usuario>().Result;
                }

            }

            return View();
        }



        [HttpGet]
        public IActionResult InicioSesion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InicioSesion(Usuario model)
        {
            return View();
        }



        [HttpGet]
        public IActionResult RecuperarAcceso()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecuperarAcceso(Usuario model)
        {
            return View();
        }


    }
}
