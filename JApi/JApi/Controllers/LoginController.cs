using Dapper;
using JApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _conf;
        private readonly IHostEnvironment _env;
        public LoginController(IConfiguration conf, IHostEnvironment env)
        {
            _conf = conf;
            _env = env;
        }

        [HttpPost]
        [Route("CrearCuenta")]
        public IActionResult CrearCuenta(Usuario model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.Execute("CrearCuenta", new { model.Identificacion, model.Nombre, model.Correo, model.Contrasenna });

                if (result > 0)
                {
                    respuesta.Codigo = 0;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "Su información no se ha registrado correctamente";
                }

                return Ok(respuesta);
            }
        }

        [HttpPost]
        [Route("IniciarSesion")]
        public IActionResult IniciarSesion(Usuario model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.QueryFirstOrDefault<Usuario>("IniciarSesion", new { model.Correo, model.Contrasenna });

                if (result != null)
                {
                    if (result.UsaClaveTemp && result.Vigencia < DateTime.Now)
                    {
                        respuesta.Codigo = -1;
                        respuesta.Mensaje = "Su información de acceso temporal ha expirado";
                    }
                    else
                    {
                        result.Token = GenerarToken(result);

                        respuesta.Codigo = 0;
                        respuesta.Contenido = result;
                    }
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "Su información no se encontró en nuestro sistema";
                }

                return Ok(respuesta);
            }
        }

        [HttpPost]
        [Route("RecuperarAcceso")]
        public IActionResult RecuperarAcceso(Usuario model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();

                //var parameters = new DynamicParameters();
                //parameters.Add("CorreoElectronico", model.Correo);
                //var result = context.QueryFirstOrDefault<Usuario>("ValidarUsuario", parameters);

                var result = context.QueryFirstOrDefault<Usuario>("ValidarUsuario", new { model.Correo });

                if (result != null)
                {
                    var Codigo = GenerarCodigo();
                    var Contrasenna = Encrypt(Codigo);
                    var UsaClaveTemp = true;
                    var Vigencia = DateTime.Now.AddMinutes(10);
                    context.Execute("ActualizarContrasenna", new { result.Consecutivo, Contrasenna, UsaClaveTemp, Vigencia });

                    var ruta = Path.Combine(_env.ContentRootPath, "RecuperarAcceso.html");
                    var html = System.IO.File.ReadAllText(ruta);

                    html = html.Replace("@@Nombre", result.Nombre);
                    html = html.Replace("@@Contrasenna", Codigo);
                    html = html.Replace("@@Vencimiento", Vigencia.ToString("dd/MM/yyyy hh:mm tt"));

                    EnviarCorreo(result.Correo, "Recuperar Accesos Sistema", html);

                    respuesta.Codigo = 0;
                    respuesta.Contenido = result;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "Su información no se encontró en nuestro sistema";
                }

                return Ok(respuesta);
            }
        }

        private string GenerarCodigo()
        {
            int length = 8;
            const string valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ012456789";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        private string Encrypt(string texto)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_conf.GetSection("Variables:Llave").Value!);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(texto);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        private string Decrypt(string texto)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(texto);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_conf.GetSection("Variables:Llave").Value!);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        private void EnviarCorreo(string destino, string asunto, string contenido)
        {
            string cuenta = _conf.GetSection("Variables:CorreoEmail").Value!;
            string contrasenna = _conf.GetSection("Variables:ClaveEmail").Value!;

            MailMessage message = new MailMessage();
            message.From = new MailAddress(cuenta);
            message.To.Add(new MailAddress(destino));
            message.Subject = asunto;
            message.Body = contenido;
            message.Priority = MailPriority.Normal;
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.office365.com", 587);
            client.Credentials = new System.Net.NetworkCredential(cuenta, contrasenna);
            client.EnableSsl = true;

            //Esto es para que no se intente enviar el correo si no hay una contraseña
            if (!string.IsNullOrEmpty(contrasenna))
            { 
                client.Send(message);
            }
        }

        private string GenerarToken(Usuario model)
        {
            string SecretKey = _conf.GetSection("Variables:Llave").Value!;
            
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("IdUsuario", model.Consecutivo.ToString()));
            claims.Add(new Claim("IdRol", model.ConsecutivoRol.ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(20),
                signingCredentials: cred);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
