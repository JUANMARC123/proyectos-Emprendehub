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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
namespace WpfAppProyectodeProgra
{
    /// <summary>
    ///
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string rutaArchLogin = "c:\\registros\\loginUsrs.txt";

        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            string correo = txtCorreo.Text;
            string contra = txtPassword.Password;

            if (correo == "" || contra == "")
            {
                lblMensaje.Foreground = Brushes.Red;
                lblMensaje.Content = "Debe llenar TODOS los campos_ :(";
            }
            else
            {
                try
                {
                    if (!File.Exists(rutaArchLogin))
                    {
                        lblMensaje.Foreground = Brushes.Red;
                        lblMensaje.Content = "La ruta o el archivo no existen!!!!";
                        return;

                    }
                    else
                    {
                        var contenidoArch = File.ReadAllLines(rutaArchLogin);
                        bool encontrado = false;
                        foreach (var linea in contenidoArch)
                        {
                            var partes = linea.Split(',');
                            if (correo.Equals(partes[3]) && contra.Equals(partes[4]))
                            {
                                encontrado = true;
                                inicio_del_programa winP = new inicio_del_programa();
                                winP.Show();
                                this.Close();
                            }
                            else
                            {
                                lblMensaje.Foreground = Brushes.Red;
                                lblMensaje.Content = "no esta registyrado";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR:" + ex.Message);
                }
                //string datos = lblCorreo.Content.ToString() +


                //"\n" + "Contraseña:" + txtPasword.Password;
                // lblMensaje.Foreground = Brushes.Green;



            }


        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {

            txtCorreo.Text = "";
            txtPassword.Password = "";


        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            sing_up winSignUp = new sing_up();
            winSignUp.Show();
            this.Close();

        }
    }
}
