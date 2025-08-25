using Biblioteca.Models;
using Biblioteca.Models.ModelosDTO;
using Biblioteca.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Biblioteca.Controllers
{
    public class ClienteController : Controller
    {
        public async Task<LibroDTO> Busqueda()
        {
            LibroDTO libro = await OpenLibraryApi.ConsultarPorIsbnAsync("OL59716892M");
            return libro;
        } 

        public ActionResult Index()
        {
            return View(Busqueda());
        }


    }
}