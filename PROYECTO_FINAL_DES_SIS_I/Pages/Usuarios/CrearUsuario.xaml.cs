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
using CNegocio;

namespace PROYECTO_FINAL_DES_SIS_I.Pages.Usuarios
{
    /// <summary>
    /// Interaction logic for CrearUsuario.xaml
    /// </summary>
    public partial class CrearUsuario : Page
    {
        public CrearUsuario()
        {
            InitializeComponent();
        }

        private void crearUsuarioDB(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Empezando a crear usuarios");
            Usuario usuario = new Usuario();
            usuario.GenerarUsuarios();

        }
    }
}
