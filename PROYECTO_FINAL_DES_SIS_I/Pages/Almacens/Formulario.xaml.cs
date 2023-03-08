using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace PROYECTO_FINAL_DES_SIS_I.Pages.Almacens
{
    /// <summary>
    /// Interaction logic for Formulario.xaml
    /// </summary>
    public partial class Formulario : Page
    {
        Almacen almacen = new Almacen();
        public Formulario()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            almacen = almacen.GetAlmacen(MainWindow.usuario.Almacen_id);

            if (almacen != null)
            {
                txtNombre.Text = almacen.Nombre;
                txtDireccion.Text = almacen.Direccion;
                txtTelefono.Text = almacen.Telefono;
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Validar();

            if (!ValidateNombre(txtNombre.Text) || !ValidateTelefono(txtTelefono.Text) || !ValidateDireccion(txtDireccion.Text))
            {
                MainWindow._ts.ShowError("Revise que todos los datos sean validos");
                return;
            }

            Almacen almacen = new Almacen()
            {
                Id = this.almacen.Id,
                Nombre = txtNombre.Text,
                Telefono = txtTelefono.Text,
                Direccion = txtDireccion.Text
            };
            almacen.ActualizarAlmacen();

            MainWindow._ts.ShowSuccess("Almacen actualizado con exito");

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            string campo = txt.Name;

            if (campo == "txtNombre")
            {
                ValidateNombre(txt.Text);
                return;
            };
            if (campo == "txtTelefono")
            {
                ValidateTelefono(txt.Text);
                return;
            }
            if (campo == "txtDireccion")
            {
                ValidateDireccion(txt.Text);
                return;
            }
        }

        private bool ValidateNombre(string value)
        {
            if (value == "")
            {
                lblNombre.Content = "El nombre es requerido";
                lblNombre.Visibility = Visibility.Visible;
                return false;
            }

            if (value.Length <= 3)
            {
                lblNombre.Content = "El nombre debe tener mas de 3 caracteres";
                lblNombre.Visibility = Visibility.Visible;
                return false;
            }

            lblNombre.Visibility = Visibility.Collapsed;
            return true;
        }

        private bool ValidateTelefono(string value)
        {

            if (value.Length > 0 && value.Length <= 7)
            {
                lblTelefono.Content = "El telefono debe tener al menos 8 digitos";
                lblTelefono.Visibility = Visibility.Visible;
                return false;
            }

            if (value.Length > 0 && value.Length >= 12)
            {
                lblTelefono.Content = "El telefono no es valido";
                lblTelefono.Visibility = Visibility.Visible;
                return false;
            }

            lblTelefono.Visibility = Visibility.Collapsed;
            return true;
        }

        private bool ValidateDireccion(string value)
        {
            if (value == "")
            {
                lblDireccion.Content = "La direccion es requerido";
                lblDireccion.Visibility = Visibility.Visible;
                return false;
            }

            if (value.Length <= 5)
            {
                lblDireccion.Content = "La direccion debe tener al menos 5 caracteres";
                lblDireccion.Visibility = Visibility.Visible;
                return false;
            }

            lblDireccion.Visibility = Visibility.Collapsed;
            return true;
        }

        private void OnlyNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Validar()
        {
            ValidateNombre(txtNombre.Text);
            ValidateTelefono(txtTelefono.Text);
            ValidateDireccion(txtDireccion.Text);
        }

    }
}
