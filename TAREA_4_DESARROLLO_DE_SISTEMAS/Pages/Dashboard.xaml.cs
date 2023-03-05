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

namespace TAREA_4_DESARROLLO_DE_SISTEMAS.Pages
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        public Dashboard()
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
            var data = pedido.GetPedidos(true);

            DataGrid.ItemsSource = data;
        }
    }
}
