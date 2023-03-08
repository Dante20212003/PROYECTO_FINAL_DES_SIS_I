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
using ComboBox = System.Windows.Controls.ComboBox;
using Checbox = System.Windows.Controls.CheckBox;
using MessageBox = HandyControl.Controls.MessageBox;
using ToastNotifications.Position;


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
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListarUsuarios();
        }

        // LISTA LOS USUARIOS EN EL DATAGRID
        public void ListarUsuarios(string query = "")
        {
            if (DataGrid != null)
            {
                var data = usuario.GetUsuarios(limit, offset, query);
                int totalUsuarios = usuario.CountUsuarios(limit, offset, query);

                DataGrid.ItemsSource = data;

                decimal total = (decimal)totalUsuarios / limit;
                paginacion.MaxPageCount = (int)Math.Ceiling(total);
            }
        }

        // ACTIVACION O DESACTIVACION DE USUARIO
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {

            if (DataGrid.SelectedItems.Count == 0)
            {
                MainWindow._ts.ShowInformation("Seleccione un registro");
                return;
            }

            Usuario usuarioRow = (Usuario)DataGrid.SelectedItem;

            if (usuarioRow.Username == "admin")
            {
                usuarioRow.Estado = true;
                MainWindow.mostrarToast(MainWindow._ts.ShowError, "No puedes desactivar al usuario admin");
                DataGrid.Items.Refresh();
                return;
            }

            usuarioRow.ActualizarUsuario();
            MainWindow.mostrarToast(MainWindow._ts.ShowSuccess, "Usuario actualizado con exito.");

        }

        // ACTIVACION O DESACTIVACION DE USUARIOS MULTIPLE || MENU ITEMS
        private void toogleEstadoCtxItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBoxResult.Yes;
            int totalSeleccionados = DataGrid.SelectedItems.Count;


            if (totalSeleccionados == 0)
            {
                MainWindow._ts.ShowInformation("Seleccione un registro");
                return;
            }

            if (totalSeleccionados > 1)
            {
                result = MessageBox.Show("Esta por modificar multiples datos. Esta seguro de continuar?", "Alerta", MessageBoxButton.YesNo, MessageBoxImage.Warning);
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
                if (usuarioRow.Username == "admin")
                {
                    MainWindow.mostrarToast(MainWindow._ts.ShowError, "No puedes desactivar al usuario admin");
                    return;
                }
                else
                {
                    usuarioRow.Estado = opcion == "Activar" ? true : false;
                    usuarioRow.ActualizarUsuario();
                }

            }

            DataGrid.Items.Refresh();
            DataGrid.SelectedItem = null;

            string msg = totalSeleccionados <= 1 ? "Usuario actualizado con exito" : $"{totalSeleccionados} Usuarios actualizados con exito.";
            MainWindow.mostrarToast(MainWindow._ts.ShowSuccess, msg);
        }

        // CAMBIO DE LIMITE DE DATOS DE COMBOBOX
        private void cbxLimit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbi = (ComboBoxItem)cbxLimit.SelectedItem;
            limit = int.Parse(cbi.Content.ToString());
            ListarUsuarios(txtSearch != null ? txtSearch.Text : "");
        }

        // CONTROL DE LA PAGINACION
        private void paginacion_PageUpdated(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            int pagina = int.Parse(e.Info.ToString());
            offset = (pagina * limit) - limit;
            ListarUsuarios(txtSearch.Text);
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

        private void Search_SearchStarted(object sender, HandyControl.Data.FunctionEventArgs<string> e)
        {
            string query = ((SearchBar)sender).Text;
            if (query.Length > 0)
            {
                ListarUsuarios(query);
            }
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txt = ((SearchBar)sender).Text;

            if (txt.Length == 0)
            {
                ListarUsuarios();
            }

        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            Modal();
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItems.Count == 0)
            {
                MainWindow._ts.ShowInformation("Seleccione un registro");
                return;
            }

            Usuario usuario = (Usuario)DataGrid.SelectedItems[0];

            if (usuario.Username == "admin")
            {
                if (MainWindow.usuario.Username == "admin")
                {
                    Modal(true, usuario);
                }

                MainWindow._ts.ShowError("No puedes editar al administrador");
                return;
            }
            Modal(true, usuario);
        }

        private void Modal(bool isEdit = false, Usuario usuario = null)
        {
            PopupWindow popup = new PopupWindow()
            {

                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                Effect = null,
            };

            if (isEdit) popup.PopupElement = new CrearUsuario(usuario);
            else popup.PopupElement = new CrearUsuario();

            if (popup.ShowDialog() != true)
            {
                ListarUsuarios();
            }
        }
    }
}
