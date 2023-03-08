using System;
using System.Windows;
using System.Windows.Controls;
using CNegocio;

namespace PROYECTO_FINAL_DES_SIS_I.Pages
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

            try
            {
                if (btn.Content.ToString() == "Generar Usuarios")
                {
                    Usuario user = new Usuario();
                    user.GenerarUsuarios(int.Parse(txtUsuario.Text));
                    MainWindow._ts.ShowSuccess($"{txtUsuario.Text} Clientes generados exitosamente");
                }
                if (btn.Content.ToString() == "Generar Productos")
                {
                    Zapato zapato = new Zapato();
                    zapato.GenerarZapatos(int.Parse(txtProducto.Text));
                    MainWindow._ts.ShowSuccess($"{txtProducto.Text} Zapatos generados exitosamente");
                }
                if (btn.Content.ToString() == "Generar Pedidos")
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
