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
        public UsuarioController(IConfiguration conf)
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

        [HttpPut]
        [Route("ActualizarPerfil")]
        public IActionResult ActualizarPerfil(Usuario model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.Execute("ActualizarPerfil", new { model.Consecutivo, model.Identificacion, model.Nombre, model.Correo, model.ConsecutivoRol });

                if (result > 0)
                {
                    respuesta.Codigo = 0;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "Su información de perfil no se ha actualizado correctamente";
                }

                return Ok(respuesta);
            }
        }

        [HttpPut]
        [Route("ActualizarEstado")]
        public IActionResult ActualizarEstado(Usuario model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.Execute("ActualizarEstado", new { model.Consecutivo });

                if (result > 0)
                {
                    respuesta.Codigo = 0;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "El estado del usuario no se ha actualizado correctamente";
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

        [HttpGet]
        [Route("ConsultarUsuario")]
        public IActionResult ConsultarUsuario(long Consecutivo)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.QueryFirstOrDefault<Usuario>("ConsultarUsuario", new { Consecutivo });

                if (result != null)
                {
                    respuesta.Codigo = 0;
                    respuesta.Contenido = result;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "No se encontró la información del usuario";
                }

                return Ok(respuesta);
            }
        }

        [HttpGet]
        [Route("ConsultarRoles")]
        public IActionResult ConsultarRoles()
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.Query<Rol>("ConsultarRoles", new { });

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
