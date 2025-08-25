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
            var bibliotecas = db.Bibliotecas.ToList();
            if (!string.IsNullOrEmpty(busqueda))
            {
                bibliotecas = (List<Models.Biblioteca>)bibliotecas.Where(b => b.Nombre.Contains(busqueda)).ToList();
            }
            return View(bibliotecas);
        }


        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Biblioteca biblioteca = await db.Bibliotecas.FindAsync(id);

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
            db.Bibliotecas.Remove(biblioteca);
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
