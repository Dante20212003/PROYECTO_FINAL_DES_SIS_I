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

namespace PROYECTO_FINAL_DES_SIS_I.Pages.Reportes
{
    /// <summary>
    /// Interaction logic for Reportes.xaml
    /// </summary>
    public partial class Reportes : Page
    {
        private Reporte reporte = new Reporte();
        public Reportes()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListarReportes();
        }

        public void ListarReportes()
        {
            if (DataGrid != null)
            {
                var data = reporte.GetReportes();

                DataGrid.ItemsSource = data;
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
