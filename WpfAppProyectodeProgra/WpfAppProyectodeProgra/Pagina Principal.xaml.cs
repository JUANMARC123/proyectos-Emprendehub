using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Collections.ObjectModel;

namespace WpfAppProyectodeProgra
{
    public partial class Pagina_Principal : Window
    {
        private readonly string rutaArchivo = "c:\\distribuidoras\\distribuidoras.txt";

        public ObservableCollection<Distribuidora> Distribuidoras { get; set; }
            = new ObservableCollection<Distribuidora>();
        private List<Distribuidora> todasDistribuidoras = new List<Distribuidora>();
        private ObservableCollection<Distribuidora> listaDistribuidoras = new ObservableCollection<Distribuidora>();

        // Ventana de Pedidos realizada (una sola instancia)
        private PedidosRealizados ventanaPedidos;

        public Pagina_Principal()
        {
            InitializeComponent();
            DataContext = this;

            todasDistribuidoras = CargarDistribuidoras();
            DistribuidorasGrid.ItemsSource = todasDistribuidoras;

            // Inicializar ventana de pedidos con colección vacía
            ventanaPedidos = new PedidosRealizados();

            CargarCategorias();
        }

        private List<Distribuidora> CargarDistribuidoras()
        {
            var lista = new List<Distribuidora>();

            if (!File.Exists(rutaArchivo))
                return lista;

            foreach (var linea in File.ReadAllLines(rutaArchivo))
            {
                var partes = linea.Split('|');
                if (partes.Length < 9) continue; // categoría incluida

                lista.Add(new Distribuidora
                {
                    Nombre = partes[0],
                    Ciudad = partes[1],
                    Telefono = partes[2],
                    AnosExperiencia = int.Parse(partes[3]),
                    Productos = partes[4],
                    Categoria = partes[5],
                    Enlace = partes[6],
                    Logo = partes[7],
                    ImagenesProductos = partes[8].Split(',').ToList()
                });
            }

            return lista;
        }

        private void CargarCategorias()
        {
            var categoriasUnicas = todasDistribuidoras
                .Select(d => d.Categoria)
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            CbCategoria.Items.Clear();
            CbCategoria.Items.Add("Todas");
            CbCategoria.SelectedIndex = 0;

            foreach (var cat in categoriasUnicas)
                CbCategoria.Items.Add(cat);
        }

        private void FiltrarDistribuidoras()
        {
            string filtro = SearchBox.Text.ToLower();
            string categoria = CbCategoria.SelectedItem?.ToString() ?? "Todas";

            var filtradas = todasDistribuidoras.Where(d =>
                (d.Nombre.ToLower().Contains(filtro) ||
                 d.Ciudad.ToLower().Contains(filtro) ||
                 d.Productos.ToLower().Contains(filtro)) &&
                (categoria == "Todas" || d.Categoria == categoria)
            ).ToList();

            DistribuidorasGrid.ItemsSource = filtradas;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FiltrarDistribuidoras();
        }

        private void CbCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FiltrarDistribuidoras();
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            AgregarProvedores ventana = new AgregarProvedores();
            bool? resultado = ventana.ShowDialog();

            if (resultado == true)
            {
                string linea = $"{ventana.NuevoRegistro.Nombre}|{ventana.NuevoRegistro.Ciudad}|{ventana.NuevoRegistro.Telefono}|{ventana.NuevoRegistro.AnosExperiencia}|{ventana.NuevoRegistro.Productos}|{ventana.NuevoRegistro.Categoria}|{ventana.NuevoRegistro.Enlace}|{ventana.NuevoRegistro.Logo}|{string.Join(",", ventana.NuevoRegistro.ImagenesProductos)}";

                File.AppendAllText(rutaArchivo, linea + Environment.NewLine);

                todasDistribuidoras.Add(ventana.NuevoRegistro);
                DistribuidorasGrid.ItemsSource = todasDistribuidoras;

                CargarCategorias();
            }
        }

        private void DistribuidorasGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (DistribuidorasGrid.SelectedItem == null) return;

        Distribuidora seleccionada = (Distribuidora)DistribuidorasGrid.SelectedItem;

        // Abrir ventana detalles pasando referencia de pedidos
        VentanaDetalles detalles = new VentanaDetalles(seleccionada);
        detalles.ShowDialog();

        DistribuidorasGrid.SelectedItem = null;
    }

        private void BtnPedidos_Click(object sender, RoutedEventArgs e)
        {
            if (ventanaPedidos == null)
                ventanaPedidos = new PedidosRealizados();

            ventanaPedidos.Show();
            ventanaPedidos.Focus();
        }

        private void BtnGestionarDistribuidoras_Click(object sender, RoutedEventArgs e)
        {
            GestionDistribuidoras ventana = new GestionDistribuidoras(listaDistribuidoras);
            ventana.ShowDialog();
        }

    }
}
