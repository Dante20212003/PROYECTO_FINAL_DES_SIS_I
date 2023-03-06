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

namespace PROYECTO_FINAL_DES_SIS_I.Components
{
    /// <summary>
    /// Interaction logic for FormAlmacen.xaml
    /// </summary>
    public partial class FormAlmacen : UserControl
    {
        Almacen almacen;
        bool init;
        public FormAlmacen(bool _init = false, Almacen _almacen = null)
        {
            init = _init;
            InitializeComponent();
            if (_almacen != null)
            {
                almacen = _almacen;
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

            btnCancelar.Visibility = Visibility.Visible;

            Almacen almacen = new Almacen()
            {
                Nombre = txtNombre.Text,
                Telefono = txtTelefono.Text,
                Direccion = txtDireccion.Text
            };

            if (this.almacen != null)
            {
                almacen.Id = this.almacen.Id;
                almacen.ActualizarAlmacen();
                ClearInputs();
                MainWindow._ts.ShowSuccess("Almacen actualizado con exito");
            }
            else
            {
                init = false;
                btnCancelar.Content = "Cerrar Ventana";
                almacen.CrearAlmacen();
                MainWindow._ts.ShowSuccess("Almacen agregado con exito");
            }


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

        private void ClearInputs()
        {
            txtNombre.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
            lblNombre.Visibility = Visibility.Collapsed;
            lblTelefono.Visibility = Visibility.Collapsed;
            lblDireccion.Visibility = Visibility.Collapsed;

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

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (init)
            {
                App.Current.MainWindow.Close();
            }
        }

        /* public static readonly DependencyProperty UsernameProperty =
             DependencyProperty.Register("Titulo", typeof(string), typeof(FormAlmacen), new PropertyMetadata(string.Empty));

         public static readonly DependencyProperty AgeProperty =
             DependencyProperty.Register("Total", typeof(int), typeof(FormAlmacen), new PropertyMetadata(0));

         public static readonly DependencyProperty FavoriteColorProperty =
             DependencyProperty.Register("Color", typeof(Color), typeof(FormAlmacen), new PropertyMetadata(Color.FromRgb(0, 0, 0)));

         public string Titulo
         {
             get { return (string)GetValue(UsernameProperty); }
             set { SetValue(UsernameProperty, value); }
         }

         public int Total
         {
             get { return (int)GetValue(AgeProperty); }
             set { SetValue(AgeProperty, value); }
         }

         public Color Color
         {
             get { return (Color)GetValue(FavoriteColorProperty); }
             set { SetValue(FavoriteColorProperty, value); }
         }*/
    }
}
