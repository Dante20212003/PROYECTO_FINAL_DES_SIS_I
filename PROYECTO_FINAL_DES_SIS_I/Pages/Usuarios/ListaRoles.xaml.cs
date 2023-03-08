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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CDatos;
using CNegocio;
using HandyControl.Controls;
using MessageBox = HandyControl.Controls.MessageBox;

namespace PROYECTO_FINAL_DES_SIS_I.Pages.Usuarios
{
    /// <summary>
    /// Interaction logic for ListaRoles.xaml
    /// </summary>
    public partial class ListaRoles : Page
    {
        Rol rol = new Rol();
        public ListaRoles()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListarRoles();
        }

        // LISTA LOS USUARIOS EN EL DATAGRID
        public void ListarRoles(string query = "")
        {
            if (DataGrid != null)
            {
                var data = rol.GetRoles(query);

                DataGrid.ItemsSource = data;
            }
        }

        // ACTIVACION O DESACTIVACION DE ROLES
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {

            Rol rolRow = (Rol)DataGrid.SelectedItem;

            if (rolRow.Id == 1)
            {
                MainWindow.mostrarToast(MainWindow._ts.ShowError, "No puedes modificar el estado de los administradores");
                return;
            }

            rolRow.ActualizarRol();
            MainWindow.mostrarToast(MainWindow._ts.ShowSuccess, "Rol actualizado con exito.");

        }

        // ACTIVACION O DESACTIVACION DE USUARIOS MULTIPLE || MENU ITEMS
        private void toogleEstadoCtxItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBoxResult.Yes;
            int totalSeleccionados = DataGrid.SelectedItems.Count;

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

            foreach (Rol rolRow in DataGrid.SelectedItems)
            {

                if (rolRow.Id == 1)
                {
                    MainWindow.mostrarToast(MainWindow._ts.ShowError, "No puedes modificar el estado de los administradores");
                    return;
                }

                rolRow.Estado = opcion == "Activar" ? true : false;
                rolRow.ActualizarRol();
            }

            DataGrid.Items.Refresh();
            DataGrid.SelectedItem = null;

            string msg = totalSeleccionados <= 1 ? "Rol actualizado con exito" : $"{totalSeleccionados} Roles actualizados con exito.";
            MainWindow.mostrarToast(MainWindow._ts.ShowSuccess, msg);
        }

        private void Search_SearchStarted(object sender, HandyControl.Data.FunctionEventArgs<string> e)
        {
            string query = ((SearchBar)sender).Text;
            if (query.Length > 0)
            {
                ListarRoles(query);
            }
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txt = ((SearchBar)sender).Text;

            if (txt.Length == 0)
            {
                ListarRoles();
            }

        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {

            if (DataGrid.SelectedItems.Count == 0)
            {
                MainWindow._ts.ShowInformation("Seleccione un registro");
                return;
            }

            Rol rol = (Rol)DataGrid.SelectedItems[0];
            if (rol.Id == 1)
            {
                if (MainWindow.usuario.Username == "admin")
                {
                    Modal(true, rol);
                }
                MainWindow._ts.ShowError("No puedes editar el rol Administrador");
                return;
            }
            Modal(true, rol);
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            Modal();
        }

        private void Modal(bool isEdit = false, Rol rol = null)
        {
            PopupWindow popup = new PopupWindow()
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                Effect = null,
            };

            if (isEdit) popup.PopupElement = new CrearRol(rol);
            else popup.PopupElement = new CrearRol();

            if (popup.ShowDialog() != true)
            {
                ListarRoles();
            }
        }
    }
}
