using System;

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
using HandyControl.Controls;
using HandyControl.Interactivity;
using TAREA_4_DESARROLLO_DE_SISTEMAS;
using MessageBox = HandyControl.Controls.MessageBox;
using TextBox = HandyControl.Controls.TextBox;
using Window = System.Windows.Window;

namespace TAREA_4_DESARROLLO_DE_SISTEMAS.Pages.Clientes
{
    /// <summary>
    /// Interaction logic for ListaClientes.xaml
    /// </summary>
    public partial class ListaClientes : Page
    {
        public ListaClientes()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListarClientes();
        }

        public void ListarClientes(string buscar = "")
        {
            if (DataGrid != null)
            {
                Cliente cliente = new Cliente();
                var data = cliente.GetClientes();

                DataGrid.ItemsSource = data;
            }
        }

        private void actualizarCtxItem_Click(object sender, RoutedEventArgs e)
        {
            int totalSeleccionados = DataGrid.SelectedItems.Count;

            if (totalSeleccionados == 0)
            {
                MainWindow.mostrarToast(MainWindow._ts.ShowWarning, "Debe seleccionar un registro");
                return;
            }

            Cliente clienteRow = (Cliente)DataGrid.SelectedItem;
            ModalCliente(true, clienteRow);
        }

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

        private void BtnNuevoCliente_Click(object sender, RoutedEventArgs e)
        {
            ModalCliente();
        }

        private void ModalCliente(bool isEdit = false, Cliente cliente = null)
        {
            PopupWindow popup = new PopupWindow()
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                Effect = null,
            };

            if (isEdit) popup.PopupElement = new CrearCliente(cliente);
            else popup.PopupElement = new CrearCliente();

            if (popup.ShowDialog() != true)
            {
                ListarClientes();
            }
        }
    }
}
