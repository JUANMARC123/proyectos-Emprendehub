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
using System.IO;

namespace WpfAppProyectodeProgra
{
    public partial class PedidosRealizados : Window
    {
        private readonly string rutaPedidos = "c:\\registros\\pedidos.txt";
        public ObservableCollection<Pedido> Pedidos { get; set; } = new ObservableCollection<Pedido>();

        public PedidosRealizados()
        {
            InitializeComponent();
            PedidosGrid.ItemsSource = Pedidos;
            CargarPedidosDesdeArchivo();
        }

        private void CargarPedidosDesdeArchivo()
        {
            if (!File.Exists(rutaPedidos)) return;

            foreach (var linea in File.ReadAllLines(rutaPedidos))
            {
                var partes = linea.Split('|');
                if (partes.Length < 5) continue;

                Pedidos.Add(new Pedido
                {
                    Distribuidora = new Distribuidora
                    {
                        Nombre = partes[0],
                        Ciudad = partes[1],
                        Productos = partes[2],
                        Categoria = partes[3]
                    },
                    Fecha = DateTime.Parse(partes[4])
                });
            }
        }

        public void AgregarPedido(Distribuidora distribuidora)
        {
            var pedido = new Pedido { Distribuidora = distribuidora };
            Pedidos.Add(pedido);
            GuardarPedidoArchivo(pedido);
        }

        private void GuardarPedidoArchivo(Pedido pedido)
        {
            string linea = $"{pedido.Distribuidora.Nombre}|{pedido.Distribuidora.Ciudad}|{pedido.Distribuidora.Productos}|{pedido.Distribuidora.Categoria}|{pedido.Fecha}";
            File.AppendAllText(rutaPedidos, linea + Environment.NewLine, Encoding.UTF8);
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = SearchBox.Text.ToLower();
            PedidosGrid.ItemsSource = new ObservableCollection<Pedido>(
                Pedidos.Where(p =>
                    p.Nombre.ToLower().Contains(filtro) ||
                    p.Ciudad.ToLower().Contains(filtro) ||
                    p.Productos.ToLower().Contains(filtro) ||
                    (!string.IsNullOrEmpty(p.Categoria) && p.Categoria.ToLower().Contains(filtro))
                )
            );
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (PedidosGrid.SelectedItem is Pedido pedido)
            {
                if (MessageBox.Show($"Eliminar pedido de '{pedido.Nombre}'?", "Confirmar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Pedidos.Remove(pedido);
                    GuardarTodosPedidos();
                }
            }
        }

        private void BtnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            if (PedidosGrid.SelectedItem is Pedido pedido)
            {
                MessageBox.Show($"Pedido de '{pedido.Nombre}' confirmado!");
                // Aquí puedes agregar lógica de confirmación real
            }
        }

        private void GuardarTodosPedidos()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var p in Pedidos)
            {
                sb.AppendLine($"{p.Nombre}|{p.Ciudad}|{p.Productos}|{p.Categoria}|{p.Fecha}");
            }
            File.WriteAllText(rutaPedidos, sb.ToString(), Encoding.UTF8);
        }
    }
}

