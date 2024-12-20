﻿namespace JApi.Models
{
    public class Usuario
    {
        public long Consecutivo { get; set; }
        public string Identificacion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Contrasenna { get; set; } = string.Empty;
        public bool UsaClaveTemp { get; set; }
        public DateTime Vigencia { get; set; }
        public short ConsecutivoRol { get;set; }
        public string NombreRol { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}
