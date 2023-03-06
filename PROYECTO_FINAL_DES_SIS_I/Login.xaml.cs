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
using CDatos;
using CNegocio;
using HandyControl.Controls;
using PROYECTO_FINAL_DES_SIS_I.Pages.Usuarios;
using Window = HandyControl.Controls.Window;

namespace PROYECTO_FINAL_DES_SIS_I
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {

            /*PopupWindow popup = new PopupWindow()
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                Effect = null,
            };


            popup.PopupElement = new CrearUsuario();

            popup.ShowDialog();*/


            Usuario usuario = new Usuario().Login(txtUsuario.Text, txtContrasena.Password);

            if (usuario != null)
            {
                MainWindow main = new MainWindow(usuario);
                main.Show();
                this.Close();
            }
;
        }
    }
}
