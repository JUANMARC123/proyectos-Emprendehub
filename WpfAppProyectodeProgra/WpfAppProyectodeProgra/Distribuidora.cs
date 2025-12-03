using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppProyectodeProgra
{
    public class Distribuidora
    {
        public string Nombre { get; set; }
        public string Ciudad { get; set; }
        public string Telefono { get; set; }
        public int AnosExperiencia { get; set; }
        public string Productos { get; set; }
        public string Categoria { get; set; }
        public string Enlace { get; set; }
        public string Logo { get; set; }
        public List<string> ImagenesProductos { get; set; }

        // 🔹 Constructor por defecto
        public Distribuidora()
        {
            Nombre = "Sin nombre";
            Ciudad = "Sin ciudad";
            Telefono = "N/A";
            AnosExperiencia = 0;
            Productos = "No especificado";
            Categoria = "No especificado";
            Enlace = "No disponible";
            Logo = "Sin logo";
            ImagenesProductos = new List<string>();
        }

        // 🔹 Constructor con parámetros
        public Distribuidora(string nombre, string ciudad, string telefono, int anosExperiencia,
                             string productos, string categorias, string enlace, string logo)
        {
            Nombre = nombre;
            Ciudad = ciudad;
            Telefono = telefono;
            AnosExperiencia = anosExperiencia;
            Productos = productos;
            Categoria = categorias;
          Enlace = enlace;
            Logo = logo;
            ImagenesProductos =  new List<string>(); 
        }
    }

}
