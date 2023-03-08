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
using CNegocio;
using HandyControl.Controls;

namespace PROYECTO_FINAL_DES_SIS_I.Pages
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        //CARDS CON TOTALES
        //LISTAR TOP 10 PRODUCTOS
        //PRODUCTOS SIN STOCK
        Zapato zapato = new Zapato();
        Usuario usuario = new Usuario();


        public Dashboard()
        {
            InitializeComponent();
        }

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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            ListarZapatosStock();
            ListarZapatosDia();
            GetDataCard();
        }

        public void GetDataCard()
        {

            lblProductosTotal.Total = zapato.CountZapatos(500, 0, "");

            lblUsuariosTotal.Total = usuario.CountUsuarios();

        }
        public void ListarZapatosStock()
        {
            if (DataGridZapatosSinStock != null)
            {

                DataGridZapatosSinStock.ItemsSource = zapato.GetZapatos(500, 0, "", false, true);
            }
        }

        public void ListarZapatosDia()
        {
            if (DataGridZapatosDia != null)
            {
                var data = zapato.GetZapatos(500, 0, "", false, false, true);
                DataGridZapatosDia.ItemsSource = data;

                lblZapDia.Total = data.Count;
            }
        }
    }
}
