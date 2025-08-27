using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Biblioteca.Models;
using Biblioteca.Models.ModelosDTO;
using Biblioteca.Services;

namespace Biblioteca.Controllers
{
    public class LoginController : Controller
    {
        private Context db = new Context();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(LoginDTO login)
        {
            try
            {
                var adminT = BuscarAdmin(login);
                var clienteT = BuscarCliente(login);
                var usuarioT = BuscarUsuario(login);

                await Task.WhenAll(adminT, clienteT, usuarioT);

                var administrador = adminT.Result;
                var cliente = clienteT.Result;
                var usuario = usuarioT.Result;

                if (administrador != null)
                {
                    SessionU.IniciarSesion(administrador);
                    return RedirectToAction("Index", "Administrador");
                }
                if (cliente != null)
                {
                    SessionU.IniciarSesion(cliente);
                    return RedirectToAction("Index", "Cliente");
                }
                if (usuario != null)
                {
                    SessionU.IniciarSesion(usuario);
                    return RedirectToAction("Index", "Usuario");
                }

                ViewBag.ErrorMessage = "Correo o Contraseña incorrectos";
                return View(usuario);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(login);
            }
         }



        public ActionResult Logout() 
        {
            SessionU.CerrarSesion();
            return RedirectToAction("Index");
        }

        public async Task<UsuarioDTO> BuscarCliente(LoginDTO usuario )

        {
            try
            {
                var cliente = await db.Clientes
                    .Include(a => a.RolClientes)
                    .Where(c => c.Correo == usuario.Correo && c.Contrasena == usuario.Contrasena)
                    .Select(c => new UsuarioDTO
                    {
                        ID = c.ID,
                        Nombre = c.Nombre,
                        Rol = c.RolClientes.Nombre ?? "Sin rol"
                    })
                    .FirstOrDefaultAsync();
                return cliente;
            }
            catch
            {

                Debug.Print("--------------------Ocurrio un error al validar con Cliente-------------------------------");

                Debug.Print("Ocurrio un error al validar con Cliente");

                return null;
            }

        }


        public async Task<UsuarioDTO> BuscarUsuario(LoginDTO usuario)
        {
            try
            {
                var cliente = await db.Usuarios
                    .Include(a => a.RolUsuario)
                    .Where(c => c.Correo == usuario.Correo && c.Contrasena == usuario.Contrasena)
                    .Select(c => new UsuarioDTO
                    {
                        ID = c.ID,
                        Nombre = c.Nombre,
                        Rol = c.RolUsuario.Nombre ?? "Sin rol"
                    })
                    .FirstOrDefaultAsync();
                return cliente;
            }
            catch
            {
                Debug.Print("-----------------Ocurrio un error al validar con Usuario------------------------");
                return null;
            }
        }
        public async Task<UsuarioDTO> BuscarAdmin(LoginDTO usuario)
        {
            try
            {
                var cliente = await db.Administradores
                    .Where(c => c.Correo == usuario.Correo && c.Contrasena == usuario.Contrasena)
                    .Select(c => new UsuarioDTO
                    {
                        ID = c.ID,
                        Nombre = c.Nombre,
                        Rol = "Administrador"
                    })
                    .FirstOrDefaultAsync();
                return cliente;
            }
            catch
            {
                Debug.Print("-----------------Ocurrio un error al validar con Administrador------------------------");
                return null;
            }
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