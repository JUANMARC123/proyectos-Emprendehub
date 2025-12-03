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
    /// <summary>
    /// Lógica de interacción para inicio_del_programa.xaml
    /// </summary>
    public partial class inicio_del_programa : Window
    {
        public inicio_del_programa()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Pagina_Principal principal = new Pagina_Principal();
            principal.Show();
            this.Close();

        }
    }
}
