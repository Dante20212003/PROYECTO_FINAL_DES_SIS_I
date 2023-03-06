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
using HandyControl.Controls;
using PROYECTO_FINAL_DES_SIS_I.Pages.Usuarios;
using CNegocio;
using MessageBox = HandyControl.Controls.MessageBox;
using Window = System.Windows.Window;

namespace PROYECTO_FINAL_DES_SIS_I.Pages.Productos
{
    /// <summary>
    /// Interaction logic for ListaProductos.xaml
    /// </summary>
    public partial class ListaProductos : Page
    {

        private int limit = 10;
        private int offset = 0;

        private Zapato zapato = new Zapato();

        public ListaProductos()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListarZapatos();
        }

        // LISTA LOS USUARIOS EN EL DATAGRID
        public void ListarZapatos(string query = "")
        {
            if (DataGrid != null)
            {
                var data = zapato.GetZapatos(limit, offset, query);
                int totalUsuarios = zapato.CountZapatos(limit, offset, query);

                DataGrid.ItemsSource = data;

                decimal total = (decimal)totalUsuarios / limit;
                paginacion.MaxPageCount = (int)Math.Ceiling(total);
            }
        }

        // ACTIVACION O DESACTIVACION DE ZAPATOS
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {

            Zapato zapatoRow = (Zapato)DataGrid.SelectedItem;
            zapatoRow.ActualizarZapato();
            MainWindow.mostrarToast(MainWindow._ts.ShowSuccess, "Zapato actualizado con exito.");

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

            foreach (Zapato zapatoRow in DataGrid.SelectedItems)
            {
                zapatoRow.Estado = opcion == "Activar" ? true : false;
                zapatoRow.ActualizarZapato();
            }

            DataGrid.Items.Refresh();
            DataGrid.SelectedItem = null;

            string msg = totalSeleccionados <= 1 ? "Zapato actualizado con exito" : $"{totalSeleccionados} Zapatos actualizados con exito.";
            MainWindow.mostrarToast(MainWindow._ts.ShowSuccess, msg);
        }

        // CAMBIO DE LIMITE DE DATOS DE COMBOBOX
        private void cbxLimit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbi = (ComboBoxItem)cbxLimit.SelectedItem;
            limit = int.Parse(cbi.Content.ToString());
            ListarZapatos(txtSearch != null ? txtSearch.Text : "");
        }

        // CONTROL DE LA PAGINACION
        private void paginacion_PageUpdated(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            int pagina = int.Parse(e.Info.ToString());
            offset = (pagina * limit) - limit;
            ListarZapatos(txtSearch.Text);
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
                ListarZapatos(query);
            }
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txt = ((SearchBar)sender).Text;

            if (txt.Length == 0)
            {
                ListarZapatos();
            }

        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            var mw = Application.Current.Windows
     .Cast<Window>()
     .FirstOrDefault(window => window is MainWindow) as MainWindow;

            mw.mainNavigaion.Content = new CrearProducto();
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            Zapato zapato = (Zapato)DataGrid.SelectedItems[0];
            Modal(true, zapato);
        }

        private void Modal(bool isEdit = false, Zapato zapato = null)
        {

            var mw = Application.Current.Windows
    .Cast<Window>()
    .FirstOrDefault(window => window is MainWindow) as MainWindow;

            mw.mainNavigaion.Content = new CrearProducto(zapato);

            /*PopupWindow popup = new PopupWindow()
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                Effect = null,
            };*/
            /*

                        if (isEdit) popup.PopupElement = new CrearUsuario(zapato);
                        else popup.PopupElement = new CrearUsuario();

                        if (popup.ShowDialog() != true)
                        {
                            ListarZapatos();
                        }*/
        }
    }
}
