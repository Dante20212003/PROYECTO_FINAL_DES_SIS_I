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
using System.Windows.Shapes;
using CNegocio;
using HandyControl.Controls;
using Microsoft.Reporting.WinForms;
using Window = System.Windows.Window;

namespace PROYECTO_FINAL_DES_SIS_I.Pages
{
    /// <summary>
    /// Interaction logic for ReporteView.xaml
    /// </summary>
    public partial class ReporteView : Window
    {
        Zapato zapato = new Zapato();
        public ReporteView()
        {
            InitializeComponent();
            // _reportViewer.Load += ReportViewer_Load;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListarZapatos();
        }

        public void ListarZapatos()
        {
            if (DataGrid != null)
            {
     
                DataGrid.ItemsSource = zapato.GetAll();

            }
        }


    }
}
