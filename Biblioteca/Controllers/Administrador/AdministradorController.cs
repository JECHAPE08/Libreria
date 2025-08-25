using Biblioteca.Models;
using Biblioteca.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Biblioteca.Controllers
{
    [Autenticacion("Administrador")]
    public class AdministradorController : Controller
    {
        private Context db = new Context();
        [HttpGet]
        public ActionResult Index()
        {
            Validar();
            ViewBag.TotalBibliotecas = db.Bibliotecas.Count();
            ViewBag.TotalClientes = db.Clientes.Count(); 
            ViewBag.TotalLibros = db.Libros.Count();
            ViewBag.Roles = db.RolClientes.ToList();
            ViewBag.Bibliotecas = db.Bibliotecas.ToList();
            return View();
        }

        private ActionResult Validar()
        {
            if (Session["UsuarioID"] == null || Session["Rol"].ToString() != "Admin")
                return RedirectToAction("Index", "Login");
            return null;
        }
    }
}