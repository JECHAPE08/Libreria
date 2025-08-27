using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteca.Models;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace Biblioteca.Controllers.Administrador
{
    
    public class AdministradorBibliotecasController : Controller
    {
        private Context db = new Context();

        public ActionResult Index(string busqueda)
        {
            var bibliotecas = db.Bibliotecas.Where(u => u.Estatus == true).ToList();
            if (!string.IsNullOrEmpty(busqueda))
            {
                bibliotecas = (List<Models.Biblioteca>)bibliotecas.Where(b => b.Nombre.Contains(busqueda)).ToList();
            }
            var usuariosPorBiblioteca = db.Usuarios
                                  .GroupBy(u => u.BibliotecaID)
                                  .ToDictionary(g => g.Key, g => g.Count());

            ViewBag.UsuariosPorBiblioteca = usuariosPorBiblioteca;
            return View(bibliotecas);
        }


        public ActionResult Details(int id, string busqueda = "")
        {
            var biblioteca = db.Bibliotecas.Find(id);

            if (biblioteca == null)
            {
                return HttpNotFound();
            }

            // Consulta base: usuarios que pertenecen a esta biblioteca
            var usuariosQuery = db.Usuarios.Where(u => u.BibliotecaID == id);

            // Si hay búsqueda, filtramos
            if (!string.IsNullOrEmpty(busqueda))
            {
                usuariosQuery = usuariosQuery.Where(u => u.Nombre.Contains(busqueda) || u.Correo.Contains(busqueda));
            }

            var usuarios = usuariosQuery.ToList();

            // Administradores de esta biblioteca
            var administradores = db.Clientes
                .Where(u => u.BibliotecaID == id)
                .ToList();

            // Pasamos datos a la vista
            ViewBag.usuarios = usuarios;
            ViewBag.CantidadUsuarios = usuarios.Count;
            ViewBag.Administradores = administradores;
            ViewBag.Busqueda = busqueda; // para rellenar el input
            ViewBag.Bibliotecas = db.Bibliotecas.ToList();
            ViewBag.Roles = db.RolUsuarios.ToList();

            return View(biblioteca);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Models.Biblioteca biblioteca)
        {
            biblioteca.AdministradorID = 1;
            if (ModelState.IsValid)
            {
                db.Bibliotecas.Add(biblioteca);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Administrador");
            }

            ViewBag.AdministradorID = new SelectList(db.Administradores, "ID", "Nombre", biblioteca.AdministradorID);
            return View(biblioteca);
        }


        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Biblioteca biblioteca = await db.Bibliotecas.FindAsync(id);
            if (biblioteca == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdministradorID = new SelectList(db.Administradores, "ID", "Nombre", biblioteca.AdministradorID);
            return View(biblioteca);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Models.Biblioteca biblioteca)
        {
            if (ModelState.IsValid)
            {
                db.Entry(biblioteca).State = EntityState.Modified;
                db.Entry(biblioteca).Property(x => x.AdministradorID).IsModified = false;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AdministradorID = new SelectList(db.Administradores, "ID", "Nombre", biblioteca.AdministradorID);
            return View(biblioteca);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            Models.Biblioteca biblioteca = await db.Bibliotecas.FindAsync(id);
            if (biblioteca == null)
            {
                return HttpNotFound();
            }
            biblioteca.Estatus = false;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
