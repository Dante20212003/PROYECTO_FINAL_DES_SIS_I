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
        private Usuario usuario = new Usuario();
        public ListaUsuarios()
        {
            InitializeComponent();
            listarUsuarios();
        }

        public void listarUsuarios()
        {
            if (DataGrid != null)
            {
                DataGrid.ItemsSource = usuario.getUsuarios(limit, offset);
            }
        }

        private void cbxLimit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBoxItem cbi = (ComboBoxItem)cbxLimit.SelectedItem;
            limit = int.Parse(cbi.Content.ToString());

            listarUsuarios();
        }

        private void desactivarCtxItem_Click(object sender, RoutedEventArgs e)
        {
            foreach (Usuario usuarioRow in DataGrid.SelectedItems)
            {
                usuarioRow.Estado = false;
                MessageBox.Show(usuarioRow.Estado.ToString());
                usuario.updateUsuario(usuarioRow);
            }
        }
        private void activarCtxItem_Click(object sender, RoutedEventArgs e)
        {
            foreach (Usuario usuarioRow in DataGrid.SelectedItems)
            {
                usuarioRow.Estado = true;
                MessageBox.Show(usuarioRow.Estado.ToString());
                usuario.updateUsuario(usuarioRow);
            }
        }

        private void DataGrid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {


            var scrollViewer = (ScrollViewer)sender;
            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox x = (CheckBox)sender;
            Usuario usuarioRow = (Usuario)DataGrid.SelectedItem;
            MessageBox.Show("CABIAR ESTADO " + x.IsChecked + " a " + usuarioRow.Nombre);
            usuario.updateUsuario(usuarioRow);
        }

    }
}
