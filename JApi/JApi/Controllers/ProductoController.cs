using Dapper;
using JApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IConfiguration _conf;
        public ProductoController(IConfiguration conf)
        {
            _conf = conf;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ConsultarProductos")]
        public IActionResult ConsultarProductos()
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.Query<Producto>("ConsultarProductos", new { });

                if (result.Any())
                {
                    respuesta.Codigo = 0;
                    respuesta.Contenido = result;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "No hay productos registrados en este momento";
                }

                return Ok(respuesta);
            }
        }


        [Authorize]
        [HttpGet]
        [Route("ConsultarProducto")]
        public IActionResult ConsultarProducto(long Consecutivo)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.QueryFirstOrDefault<Producto>("ConsultarProducto", new { Consecutivo });

                if (result != null)
                {
                    respuesta.Codigo = 0;
                    respuesta.Contenido = result;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "No se encontró la información del producto";
                }

                return Ok(respuesta);
            }
        }


        [Authorize]
        [HttpPut]
        [Route("ActualizarEstado")]
        public IActionResult ActualizarEstado(Producto model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.Execute("ActualizarEstadoProducto", new { model.Consecutivo });

                if (result > 0)
                {
                    respuesta.Codigo = 0;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "El estado del producto no se ha actualizado correctamente";
                }

                return Ok(respuesta);
            }
        }


        [Authorize]
        [HttpPost]
        [Route("RegistrarProducto")]
        public IActionResult RegistrarProducto(Producto model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.QueryFirstOrDefault<Producto>("RegistrarProducto", new { model.Nombre, model.Precio, model.Inventario, model.Imagen });

                if (result != null)
                {
                    respuesta.Codigo = 0;
                    respuesta.Mensaje = result.Consecutivo.ToString();
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "La información del producto no se ha registrado correctamente";
                }

                return Ok(respuesta);
            }
        }


        [Authorize]
        [HttpPut]
        [Route("ActualizarProducto")]
        public IActionResult ActualizarProducto(Producto model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.Execute("ActualizarProducto", new { model.Consecutivo, model.Nombre, model.Precio, model.Inventario });

                if (result > 0)
                {
                    respuesta.Codigo = 0;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "La información del producto no se ha actualizado correctamente";
                }

                return Ok(respuesta);
            }
        }


    }
}
