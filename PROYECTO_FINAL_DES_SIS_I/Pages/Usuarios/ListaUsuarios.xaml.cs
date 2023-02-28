using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for ListaUsuarios.xaml
    /// </summary>
    public partial class ListaUsuarios : Page
    {
        private int limit = 10;
        private int offset = 0;

        public ListaUsuarios()
        {
            InitializeComponent();
            listarUsuarios();
        }

        public void listarUsuarios()
        {
            Usuario usuario = new Usuario();

            DataGrid.ItemsSource = usuario.getUsuarios(limit, offset);
        }

        private void cbxLimit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbi = (ComboBoxItem)cbxLimit.SelectedItem;
            limit = int.Parse(cbi.Content.ToString());

            listarUsuarios();
        }

        private void contextMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Usuario row = (Usuario)DataGrid.SelectedItems[0];
                MessageBox.Show(row.Nombre);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }


        }
    }
}
