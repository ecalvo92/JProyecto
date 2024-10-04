using JApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        [HttpPost]
        [Route("CrearCuenta")]
        public IActionResult CrearCuenta(Usuario model)
        {
            //Debería ir a la base de datos a insertar una cuenta de usuario

            return Ok(model);
        }

    }
}
