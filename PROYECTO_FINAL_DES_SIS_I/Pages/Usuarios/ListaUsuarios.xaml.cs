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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CNegocio;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using static HandyControl.Tools.Interop.InteropValues;
using MessageBox = HandyControl.Controls.MessageBox;
using ScrollViewer = HandyControl.Controls.ScrollViewer;
using TextBox = HandyControl.Controls.TextBox;

namespace PROYECTO_FINAL_DES_SIS_I.Pages.Usuarios
{
    /// <summary>
    /// Interaction logic for ListaUsuarios.xaml
    /// </summary>
    public partial class ListaUsuarios : Page
    {
        private int limit = 10;
        private int offset = 0;
        private string[] busqueda = new string[] { "%", "%" };

        private Usuario usuario = new Usuario();
        public ListaUsuarios()
        {
            InitializeComponent();

            listarUsuarios();
        }

        // LISTA LOS USUARIOS EN EL DATAGRID
        public void listarUsuarios(string buscar = "")
        {
            if (DataGrid != null)
            {
                string busquedaResult = $"{busqueda[0]}{buscar}{busqueda[1]}";

                var data = usuario.getUsuarios(limit, offset, busquedaResult);
                int totalUsuarios = usuario.GetTotalUsuarios(limit, offset, busquedaResult);

                DataGrid.ItemsSource = data;

                decimal total = (decimal)totalUsuarios / limit;
                paginacion.MaxPageCount = (int)Math.Ceiling(total);
            }
        }

        // ACTIVACION O DESACTIVACION DE USUARIO
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuarioRow = (Usuario)DataGrid.SelectedItem;
            usuarioRow.ActualizarUsuario();
            MainWindow.mostrarToast(MainWindow._ts.ShowSuccess, "Usuario actualizado con exito.");
        }

        // ACTIVACION O DESACTIVACION DE USUARIOS MULTIPLE || MENU ITEMS
        private void toogleEstadoCtxItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBoxResult.Yes;
            int totalSeleccionados = DataGrid.SelectedItems.Count;

            if (totalSeleccionados > 1)
            {
                result = MessageBox.Show("Esta por modificar multiples datos. Esta seguro de continuar?", "awd", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            }

            if (result == MessageBoxResult.No)
            {
                MainWindow.mostrarToast(MainWindow._ts.ShowWarning, "Operacion cancelada.");
                return;
            }

            MenuItem menuItem = (MenuItem)sender;
            string opcion = menuItem.Header.ToString();

            foreach (Usuario usuarioRow in DataGrid.SelectedItems)
            {
                usuarioRow.Estado = opcion == "Activar" ? true : false;
                usuarioRow.ActualizarUsuario();
            }

            DataGrid.Items.Refresh();
            DataGrid.SelectedItem = null;

            string msg = totalSeleccionados <= 1 ? "Usuario actualizado con exito" : $"{totalSeleccionados} Usuarios actualizados con exito.";
            MainWindow.mostrarToast(MainWindow._ts.ShowSuccess, msg);

        }

        //SCROLL DE TABLA
        private void DataGrid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {

            if (Keyboard.Modifiers.Equals(ModifierKeys.Control))
            {
                if (e.Delta < 0)
                {
                    scrollTabla.LineRight();
                }
                else
                {
                    scrollTabla.LineLeft();
                }
            }
            else
            {
                scrollTabla.ScrollToVerticalOffset(scrollTabla.VerticalOffset - e.Delta);
            }
        }

        // CAMBIO DE LIMITE DE DATOS DE COMBOBOX
        private void cbxLimit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbi = (ComboBoxItem)cbxLimit.SelectedItem;
            limit = int.Parse(cbi.Content.ToString());
            listarUsuarios(txtSearch != null ? txtSearch.Text : "");

        }

        // CONTROL DE LA PAGINACION
        private void paginacion_PageUpdated(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            int pagina = int.Parse(e.Info.ToString());
            offset = (pagina * limit) - limit;
            listarUsuarios(txtSearch.Text);
        }

        // EXPANCION DE FILA PARA DATAGRID
        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    row.DetailsVisibility = row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    break;
                }
        }

        public void BuscarXFiltro(bool clear = false)
        {
            if (txtSearch.Text.Length > 0)
            {
                txtSearch.Text = clear ? "" : txtSearch.Text;
                listarUsuarios(txtSearch.Text);
            }
            else
            {
                MainWindow.mostrarToast(MainWindow._ts.ShowInformation, "Ingrese un valor para buscar");
            }

        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            BuscarXFiltro();
        }

        private void BuscarKeyEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case Key.Enter:
                        BuscarXFiltro(true);
                        break;

                }
                return;
            }

            if (e.Key == Key.Enter)
            {
                BuscarXFiltro();
                return;
            }


        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
            paginacion.PageIndex = 1;
            listarUsuarios();
        }

        private void FilterTypeItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem opcion = (MenuItem)sender;
            string filtro = opcion.Header.ToString();

            if (filtro == "Contenga")
            {
                busqueda[0] = "%";
                busqueda[1] = "%";

            }
            else if (filtro == "Empieze por")
            {
                busqueda[0] = "";
                busqueda[1] = "%";
            }
            else if (filtro == "Termine por")
            {
                busqueda[0] = "%";
                busqueda[1] = "";
            }
            else if (filtro == "Sea igual a")
            {
                busqueda[0] = "";
                busqueda[1] = "";
            }
        }
    }
}
