using Dapper;
using JApi.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace JApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly IConfiguration _conf;
        public ErrorController(IConfiguration conf)
        {
            _conf = conf;
        }

        [Route("RegistrarErrorBD")]
        public IActionResult RegistrarErrorBD()
        {
            var exc = HttpContext.Features.Get<IExceptionHandlerFeature>();
            
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var Consecutivo = ObtenerUsuarioToken();
                var Mensaje = exc!.Error.Message;
                var Origen = exc.Path;

                context.Execute("RegistrarError", new { Consecutivo, Mensaje, Origen });
            }

            var respuesta = new Respuesta();
            respuesta.Codigo = -1;
            respuesta.Mensaje = "Se presentó un problema en el sistema";
            return Ok(respuesta);
        }

        private long ObtenerUsuarioToken()
        {
            if (User.Claims.Count() > 0)
            {
                var Consecutivo = User.Claims.Select(Claim => new { Claim.Type, Claim.Value })
                    .FirstOrDefault(x => x.Type == "IdUsuario")!.Value;

                return long.Parse(Consecutivo);
            }

            return 0;
        }

    }
}
