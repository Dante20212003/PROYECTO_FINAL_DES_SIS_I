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

namespace PROYECTO_FINAL_DES_SIS_I.Pages
{
    /// <summary>
    /// Interaction logic for GenerarReporte.xaml
    /// </summary>
    public partial class GenerarReporte : Page
    {
        public GenerarReporte()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Reporte reporte = new Reporte()
            { 
                Observaciones = txtObservacion.Text,
                Usuario_id = MainWindow.usuario.Id
            };

            reporte.CrearReporte();


            ReporteView reporteView = new ReporteView();
            reporteView.Show();
        }
    }
}
