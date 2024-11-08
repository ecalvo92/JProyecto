using Dapper;
using JApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _conf;
        public UsuarioController(IConfiguration conf, IHostEnvironment env)
        {
            _conf = conf;
        }

        [HttpPut]
        [Route("CambiarAcceso")]
        public IActionResult CambiarAcceso(Usuario model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();

                var UsaClaveTemp = false;
                var Vigencia = DateTime.Now;
                var result = context.Execute("ActualizarContrasenna", new { model.Consecutivo, model.Contrasenna, UsaClaveTemp, Vigencia });

                if (result > 0)
                {
                    respuesta.Codigo = 0;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "Su información de acceso no se ha actualizado correctamente";
                }

                return Ok(respuesta);
            }
        }

        [HttpGet]
        [Route("ConsultarUsuarios")]
        public IActionResult ConsultarUsuarios()
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.Query<Usuario>("ConsultarUsuarios", new { });

                if (result.Any())
                {
                    respuesta.Codigo = 0;
                    respuesta.Contenido = result;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "No hay usuarios registrados en este momento";
                }

                return Ok(respuesta);
            }
        }
        

    }
}
