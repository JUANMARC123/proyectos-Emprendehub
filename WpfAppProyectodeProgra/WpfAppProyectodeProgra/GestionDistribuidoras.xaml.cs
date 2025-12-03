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
using System.Collections.ObjectModel;

namespace WpfAppProyectodeProgra
{
    public partial class GestionDistribuidoras : Window
    {
        public ObservableCollection<Distribuidora> Distribuidoras { get; set; }
        private ObservableCollection<Distribuidora> ListaOriginal;
        private PedidosRealizados ventanaPedidos;


        public GestionDistribuidoras(ObservableCollection<Distribuidora> lista)
        {
            InitializeComponent();

            Distribuidoras = lista;
            ListaOriginal = new ObservableCollection<Distribuidora>(lista);

            dgDistribuidoras.ItemsSource = Distribuidoras;
        }

        // -----------------------------
        // FILTRO DE BUSQUEDA
        // -----------------------------
        private void txtBuscar_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            Filtrar();
        }

        // -----------------------------
        // FILTRO CATEGORÍA
        // -----------------------------
        private void cmbFiltrarCategoria_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Filtrar();
        }

        // -----------------------------
        // FUNCIÓN CENTRAL DE FILTRADO
        // -----------------------------
        private void Filtrar()
        {
            string texto = txtBuscar.Text.ToLower();
            string categoriaSeleccionada = (cmbFiltrarCategoria.SelectedItem as ComboBoxItem)?.Content.ToString();

            var resultado = ListaOriginal.Where(d =>
                (d.Nombre.ToLower().Contains(texto) ||
                 d.Ciudad.ToLower().Contains(texto) ||
                 d.Productos.ToLower().Contains(texto) ||
                 d.Categoria.ToLower().Contains(texto))
                &&
                (categoriaSeleccionada == "Todas" || d.Categoria == categoriaSeleccionada)
            );

            Distribuidoras = new ObservableCollection<Distribuidora>(resultado);
            dgDistribuidoras.ItemsSource = Distribuidoras;
        }

        // -----------------------------
        // BOTÓN: VER DETALLES
        // -----------------------------
        private void BtnDetalles_Click(object sender, RoutedEventArgs e)
        {
            if (dgDistribuidoras.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una distribuidora.");
                return;
            }

            Distribuidora seleccionada = (Distribuidora)dgDistribuidoras.SelectedItem;

            VentanaDetalles ventana = new VentanaDetalles(seleccionada);
            ventana.ShowDialog();
        }

        
     
        // -----------------------------
        // BOTÓN: ELIMINAR
        // -----------------------------
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dgDistribuidoras.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una distribuidora para eliminar.");
                return;
            }

            Distribuidora eliminar = (Distribuidora)dgDistribuidoras.SelectedItem;

            if (MessageBox.Show("¿Eliminar distribuidora?", "Confirmar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ListaOriginal.Remove(eliminar);
                Distribuidoras.Remove(eliminar);
            }
        }
    }
}