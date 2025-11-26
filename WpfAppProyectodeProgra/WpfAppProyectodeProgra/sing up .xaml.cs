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
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
namespace WpfAppProyectodeProgra
{
    /// <summary>
    /// Lógica de interacción para sing_up.xaml
    /// </summary>
    public partial class sing_up : Window
    {
        private readonly string rutaArchLogin = "c:\\registros\\loginUsrs.txt";
        public sing_up()
        {
            InitializeComponent();
        }
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {

            if (txtNombre.Text == "" || txtAP.Text == "" || txtCelular.Text == "" || txtFNacimiento.Text == "" || txtPasword.Password == "")
            {
                int pausa = 100;
                lblMensaje.Content = "Debe llenar TODOS los campos_ :(";

                Console.Beep(440, 500); Thread.Sleep(pausa); // A
                Console.Beep(440, 500); Thread.Sleep(pausa); // A
                Console.Beep(440, 500); Thread.Sleep(pausa); // A
                Console.Beep(349, 350); Thread.Sleep(pausa); // F
                Console.Beep(440, 500); Thread.Sleep(pausa); // A
                Console.Beep(349, 350); Thread.Sleep(pausa); // F
                Console.Beep(440, 500); Thread.Sleep(pausa); // A
            }
            else
            {
                //string correo = txtNombre.Text[0] + txtAP.Text.ToLower() + txtAM.Text.ToLower()[0] + "@univalle.edu";
                //string datos = lblNombre.Content.ToString() + txtNombre.Text +
                //"\n" + lblAP.Content.ToString() + txtAP.Text +
                //"\n" + lblAM.Content.ToString() + txtAM.Text +
                //"\n" + lblCelular.Content.ToString() + txtCelular.Text +
                //"\n" + lblFNacimiento.Content.ToString() + txtFNacimiento.Text +
                //"\n" + correo +
                //"\n" + "Contraseña:" + txtPasword.Password;
                //File.AppendAllText(rutaArchLogin, datos, Encoding.UTF8);

                //lblMensaje.Foreground = Brushes.Green;
                //lblMensaje.Content = "Bienvenido/a__" + txtNombre.Text.ToLower() + "__" + txtAP.Text.ToLower() + "_" + txtAM.Text.ToLower() + "!:)";
                //wpfwinpeincipal principal = new wpfwinpeincipal();
                //principal.Show();
                //this.Close();


                string letterPattern = @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$";
                string numericPattern = @"^[0-9]{8,}$"; // mínimo 8 dígitos

                // Validar nombre
                if (!Regex.IsMatch(txtNombre.Text, letterPattern))
                {
                    lblMensaje.Foreground = Brushes.Red;
                    lblMensaje.Content = "El formato del Nombre es incorrecto.";
                    txtNombre.Focus();
                    return;
                }

                // Validar apellidos
                if (!Regex.IsMatch(txtAP.Text, letterPattern))
                {
                    lblMensaje.Foreground = Brushes.Red;
                    lblMensaje.Content = "El formato del Apellido Paterno es incorrecto.";
                    txtAP.Focus();
                    return;
                }


                // Validar celular
                if (!Regex.IsMatch(txtCelular.Text, numericPattern))
                {
                    lblMensaje.Foreground = Brushes.Red;
                    lblMensaje.Content = "El número de celular debe contener al menos 8 dígitos.";
                    txtCelular.Focus();
                    return;
                }

                // Crear correo institucional
                string correo = $"{txtNombre.Text.ToLower()[0]}{txtAP.Text.ToLower()[0]}@univalle.edu";

                // Crear el texto de datos (legible)
                string datos = txtNombre.Text + " " +
                  txtAP.Text + " " + txtAP.Text + "," +
                   txtCelular.Text + "," +
                   txtFNacimiento.Text + ","
                   + correo + "," + txtPasword.Password + "\n";

                // Crear carpeta si no existe
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(rutaArchLogin));

                // Guardar datos en el archivo
                File.AppendAllText(rutaArchLogin, datos, Encoding.UTF8);

                // Mostrar mensaje de bienvenida
                lblMensaje.Foreground = Brushes.Green;
                lblMensaje.Content = $"Bienvenido/a {txtNombre.Text.ToLower()} {txtAP.Text.ToLower()} {txtAP.Text.ToLower()}! :)";

                // Abrir ventana principal
                inicio_del_programa principal = new inicio_del_programa();
                principal.Show();
                this.Close();

            }


        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtNombre.Text = "";
            txtCorreo.Text = "";
            txtPasword.Password = "";
            txtFNacimiento.Text = "";
            txtCelular.Text = "";
            txtAP.Text = "";

        }
    }
}
