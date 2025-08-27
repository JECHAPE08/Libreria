using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Biblioteca.Models
{
    [Table("Libros")]
    public class Libro
    {
        public int ID { get; set; }
        public int KEY { get; set; }
        public string Materia { get; set; }
        public int NumeroEjemplar { get; set; }
        public string Clasificacion { get; set; }
        public bool Estatus { get; set; } = true;
        public int CategoriaID { get; set; }
        [ForeignKey("CategoriaID")]
        public virtual Categoria Categoria { get; set; }

    }
}