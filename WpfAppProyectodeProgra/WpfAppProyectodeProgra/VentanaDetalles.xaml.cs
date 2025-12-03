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
using System.Diagnostics;
using System.Windows.Navigation;
using System.Collections.ObjectModel;


namespace WpfAppProyectodeProgra
{
    public partial class VentanaDetalles : Window
    {
        public Distribuidora Distribuidora { get; set; }

        public VentanaDetalles(Distribuidora distribuidora)
        {
            InitializeComponent();
            Distribuidora = distribuidora;
            DataContext = Distribuidora;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        private void BtnAgregarPedido_Click(object sender, RoutedEventArgs e)
        {
            // Obtenemos la ventana de pedidos desde la página principal
            if (Application.Current.Windows.OfType<PedidosRealizados>().FirstOrDefault() is PedidosRealizados ventanaPedidos)
            {
                ventanaPedidos.AgregarPedido(Distribuidora);
                MessageBox.Show($"Pedido agregado a '{Distribuidora.Nombre}'");
            }
            else
            {
                MessageBox.Show("Abra primero la ventana de Pedidos Realizados desde la página principal.");
            }
        }

    }
}