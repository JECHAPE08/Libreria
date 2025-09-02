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

            // Obtener datos de bibliotecas
            ViewBag.TotalBibliotecas = db.Bibliotecas.Where(u => u.Estatus == true).Count();
            ViewBag.Bibliotecas = db.Bibliotecas.Where(u => u.Estatus == true).ToList();

            // Obtener datos de clientes
            ViewBag.TotalClientes = db.Clientes.Where(u => u.Estatus == true).Count();

            // Obtener datos de usuarios (diferente a clientes)
            ViewBag.TotalUsuarios = db.Usuarios.Where(u => u.Estatus == true).Count();

            // Obtener roles de clientes
            ViewBag.Roles = db.RolClientes.ToList();

            // Obtener bibliotecas inactivas
            ViewBag.BibliotecasInactivas = db.Bibliotecas.Where(u => u.Estatus == false).Count();

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