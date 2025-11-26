using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfAppProyectodeProgra
{
    public partial class AgregarProvedores : Window
    {
        public Distribuidora NuevoRegistro { get; private set; }

        public AgregarProvedores()
        {
            InitializeComponent();
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.");
                return;
            }

            if (!int.TryParse(TxtAnios.Text, out int experiencia))
            {
                MessageBox.Show("Años de experiencia debe ser número.");
                return;
            }

            // Obtener la categoría seleccionada
            string categoria = "";
            if (CbCategoria.SelectedItem != null)
            {
                if (CbCategoria.SelectedItem is ComboBoxItem)
                    categoria = ((ComboBoxItem)CbCategoria.SelectedItem).Content.ToString();
                else
                    categoria = CbCategoria.SelectedItem.ToString();
            }

            // Separar imágenes por línea
            List<string> imagenes = new List<string>();
            if (!string.IsNullOrWhiteSpace(TxtImagenes.Text))
            {
                imagenes = TxtImagenes.Text.Split(new[] { '\n', '\r' },
                             StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            // Crear nuevo registro
            NuevoRegistro = new Distribuidora
            {
                Nombre = TxtNombre.Text,
                Ciudad = TxtCiudad.Text,
                Telefono = TxtTelefono.Text,
                AnosExperiencia = experiencia,
                Productos = TxtProductos.Text,
                Categoria = categoria, // <-- aquí se guarda la categoría
                Enlace = TxtEnlace.Text,
                Logo = TxtLogo.Text,
                ImagenesProductos = imagenes
            };

            DialogResult = true;
            Close();
        }
    }
}

