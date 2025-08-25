using Biblioteca.Models.ModelosDTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Biblioteca.Services
{
    public class OpenLibraryApi
    {
        private static readonly HttpClient client = new HttpClient();
        private static string _urlBase = "https://openlibrary.org/";

        public static async Task<LibroDTO> ConsultarPorIsbnAsync(string isbn)
        {
            try
            {
                var response = await client.GetAsync($"{_urlBase}isbn/{isbn}.json");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("No se encontró el libro.");
                    return null;
                }

                string json = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(json);

                var firstResult = data["docs"]?.First;

                if (firstResult == null)
                {
                    Console.WriteLine("Libro no encontrado en resultados.");
                    return null;
                }
                string clave = firstResult["key"]?.ToString();
                string titulo = firstResult["title"]?.ToString();
                string tema = firstResult["subject"]?.First?.ToString(); // Solo la primera categoría
                string imagenUrl = $"https://covers.openlibrary.org/b/isbn/{isbn}-M.jpg";

                return new LibroDTO
                {
                    Clave = clave,
                    Titulo = titulo,
                    Tema = tema ?? "Sin tema",
                    Imagen = imagenUrl
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public static async Task<string> ColsultarTitulo(string titulo)
        {
            try
            {
                var response = await client.GetAsync($"{_urlBase}search.json?title={titulo}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                return $"Error: Libro no encontrado (Status: {response.StatusCode})";
            }
            catch (Exception ex)
            { 
                return $"Error_ {ex.Message}";
            }
        }
    }
}