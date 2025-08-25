using Biblioteca.Models;
using Biblioteca.Models.ModelosDTO;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Biblioteca.Services
{
    public class SessionU
    {
        
        public static void IniciarSesion(UsuarioDTO usuario)
        {
            HttpContext.Current.Session["UsuarioID"] = usuario.ID;
            HttpContext.Current.Session["Nombre"] = usuario.Nombre;
            HttpContext.Current.Session["Rol"] = usuario.Rol;
        }

        public static void CerrarSesion()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }

        public static string ObtenerRol()
        {
            return HttpContext.Current.Session["Rol"]?.ToString();
        }
    }
}