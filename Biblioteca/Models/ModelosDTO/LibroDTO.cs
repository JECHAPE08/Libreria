using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Biblioteca.Models.ModelosDTO
{
    public class RespuestaBusqueda
    {
        [JsonProperty("numFound")]
        public int LibrosEncontrados { get; set; }

        [JsonProperty("start")]
        public int Inicio { get; set; }

        [JsonProperty("numFoundExact")]
        public bool EncontradosExactos { get; set; }

        [JsonProperty("num_found")]
        public int LibrosEncontradosAlt { get; set; }

        [JsonProperty("documentation_url")]
        public string UrlDocumentacion { get; set; }

        [JsonProperty("q")]
        public string Consulta { get; set; }

        [JsonProperty("offset")]
        public object Desplazamiento { get; set; }

        [JsonProperty("docs")]
        public List<Libro> Documentos { get; set; }
    }

    public class LibroInformacionDTO
    {
        [JsonProperty("author_key")]
        public List<string> ClavesAutor { get; set; }

        [JsonProperty("author_name")]
        public List<string> NombresAutor { get; set; }

        [JsonProperty("ebook_access")]
        public string AccesoEbook { get; set; }

        [JsonProperty("edition_count")]
        public int ConteoEdiciones { get; set; }

        [JsonProperty("first_publish_year")]
        public int AnioPrimeraPublicacion { get; set; }

        [JsonProperty("has_fulltext")]
        public bool TieneTextoCompleto { get; set; }

        [JsonProperty("key")]
        public string Clave { get; set; }

        [JsonProperty("language")]
        public List<string> Idioma { get; set; }

        [JsonProperty("public_scan_b")]
        public bool EscaneoPublico { get; set; }

        [JsonProperty("title")]
        public string Titulo { get; set; }
    }

    public class LibroDTO
    {
        public string Clave { get; set; }
        public string Titulo { get; set; }
        public string Imagen { get; set; }
        public string Tema { get; set; }
        public bool Estatus {  get; set; }

        public static implicit operator string(LibroDTO v)
        {
            throw new NotImplementedException();
        }
    }
}