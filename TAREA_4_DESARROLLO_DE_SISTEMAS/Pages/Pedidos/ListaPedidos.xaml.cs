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
using HandyControl.Controls;
using TAREA_4_DESARROLLO_DE_SISTEMAS.Pages.Clientes;
using Window = System.Windows.Window;

namespace TAREA_4_DESARROLLO_DE_SISTEMAS.Pages.Pedidos
{
    /// <summary>
    /// Interaction logic for ListaPedidos.xaml
    /// </summary>
    public partial class ListaPedidos : Page
    {
        public ListaPedidos()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListarPedidos();
        }

        public void ListarPedidos()
        {
            Pedido pedido = new Pedido();
            var data = pedido.GetPedidos();

            DataGrid.ItemsSource = data;
        }

        private void actualizarCtxItem_Click(object sender, RoutedEventArgs e)
        {
            int totalSeleccionados = DataGrid.SelectedItems.Count;

            if (totalSeleccionados == 0)
            {
                MainWindow.mostrarToast(MainWindow._ts.ShowWarning, "Debe seleccionar un registro");
                return;
            }

            Pedido pedidoRow = (Pedido)DataGrid.SelectedItem;
            ModalEditarPedido(pedidoRow);
        }

        private void BtnNuevoCliente_Click(object sender, RoutedEventArgs e)
        {
            var mw = Application.Current.Windows
    .Cast<Window>()
    .FirstOrDefault(window => window is MainWindow) as MainWindow;

            mw.mainNavigaion.Content = new CrearPedido();

        }

        private void ModalEditarPedido(Pedido pedido)
        {
            PopupWindow popup = new PopupWindow()
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                Effect = null,
            };

            popup.PopupElement = new EditarPedido(pedido);

            if (popup.ShowDialog() != true)
            {
                ListarPedidos();
            }
        }
    }
}
