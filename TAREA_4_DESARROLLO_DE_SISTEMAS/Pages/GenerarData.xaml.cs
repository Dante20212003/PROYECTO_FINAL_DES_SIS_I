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
    /// Interaction logic for GenerarData.xaml
    /// </summary>
    public partial class GenerarData : Page
    {
        public GenerarData()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Content.ToString() == "Generar Clientes")
            {
                Cliente cl = new Cliente();
                cl.GenerarClientes(int.Parse(txtCliente.Text));
                MainWindow._ts.ShowSuccess("Clientes generados exitosamente");
            }
            if (btn.Content.ToString() == "Generar Productos")
            {
                Producto pr = new Producto();
                pr.GenerarProductos(int.Parse(txtProducto.Text));
                MainWindow._ts.ShowSuccess("Productos generados exitosamente");
            }
            if (btn.Content.ToString() == "Generar Pedidos")
            {

            }

        }
    }
}
