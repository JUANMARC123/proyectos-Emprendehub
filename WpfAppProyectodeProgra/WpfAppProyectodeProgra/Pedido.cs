using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppProyectodeProgra
{
    public class Pedido
    {
        public Distribuidora Distribuidora { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;

        public string Nombre => Distribuidora?.Nombre;
        public string Ciudad => Distribuidora?.Ciudad;
        public string Productos => Distribuidora?.Productos;
        public string Categoria => Distribuidora?.Categoria;

        // Constructor por defecto
        public Pedido() { }

        // Constructor con parámetro
        public Pedido(Distribuidora distribuidora)
        {
            Distribuidora = distribuidora;
            Fecha = DateTime.Now;
        }
    }


}
