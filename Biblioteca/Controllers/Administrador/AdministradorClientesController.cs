using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Biblioteca.Models;

namespace Biblioteca.Controllers.Administrador
{
    public class AdministradorClientesController : Controller
    {
        private Context db = new Context();
        public ActionResult Index(string busqueda)
        {
            var clientes = db.Clientes.ToList();
            if (!string.IsNullOrEmpty(busqueda))
            {
                clientes = (List<Models.Cliente>)clientes.Where(b => b.Nombre.Contains(busqueda)).ToList();
            }
            ViewBag.Bibliotecas = db.Bibliotecas.ToList();
            ViewBag.Roles = db.RolClientes.ToList();
            return View(clientes);
        }


        public async Task<ActionResult> Create(Models.Cliente clientes)
        {
            if (ModelState.IsValid)
            {
                db.Clientes.Add(clientes);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Administrador");
            }

            return View(clientes);
        }


        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Cliente cliente = await db.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Models.Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.Entry(cliente).Property(x => x.AdministradorID).IsModified = false;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AdministradorID = new SelectList(db.Administradores, "ID", "Nombre", biblioteca.AdministradorID);
            return View(biblioteca);
        }
    }
}