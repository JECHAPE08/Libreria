using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteca.Models.ModelosDTO
{
    public class UsuarioDTO
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
    }
}