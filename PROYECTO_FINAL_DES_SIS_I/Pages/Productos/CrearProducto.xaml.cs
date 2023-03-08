using System;
using System.Collections.Generic;
using System.IO;
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
using CDatos;
using CNegocio;
using HandyControl.Controls;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Application;
using ComboBox = System.Windows.Controls.ComboBox;
using MessageBox = HandyControl.Controls.MessageBox;
using TextBox = System.Windows.Controls.TextBox;
using Window = System.Windows.Window;

namespace PROYECTO_FINAL_DES_SIS_I.Pages.Productos
{
    /// <summary>
    /// Interaction logic for CrearProducto.xaml
    /// </summary>
    public partial class CrearProducto : Page
    {
        private string imgUrl = "";
        private Zapato zapato;

        public CrearProducto(Zapato _zapato = null)
        {
            InitializeComponent();

            if (_zapato != null)
            {
                zapato = _zapato;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListarEstados();

            cbxEstado.SelectedValue = "True";
            cbxUsuario.Text = MainWindow.usuario.Nombre + " " + MainWindow.usuario.Apellido;

            if (zapato != null)
            {
                txtCodigo.Text = zapato.Codigo;
                txtNombre.Text = zapato.Nombre;
                txtModelo.Text = zapato.Modelo;
                txtTalla.Text = zapato.Talla;
                txtColor.Text = zapato.Color;
                txtStock.Text = zapato.Stock.ToString();
                txtPrecio.Text = zapato.Precio.ToString();

                btnAgregar.Content = "Actualizar";
                btnCambiarImagen.Visibility = Visibility.Visible;

                string url = zapato.Img;

                if (zapato.Img.Length == 0) url = "https://images.vexels.com/media/users/3/142961/isolated/preview/9031943c6d5353510bc611c6be779b2c-zapatos-rojos-zapatillas-ropa.png";

                BitmapImage image = new BitmapImage(new Uri(url, UriKind.Absolute));
                imageView.Source = image;
                imageView.Visibility = Visibility.Visible;
                imagenProduct.Visibility = Visibility.Collapsed;


                try
                {

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                cbxEstado.SelectedValue = zapato.Estado.ToString();
            }
        }

        public void ListarEstados()
        {
            List<object> data = new List<object>()
            {
                new {Value = "True", Nombre = "Activo" },
                new {Value = "False", Nombre = "Desactivado" },
            };

            cbxEstado.DisplayMemberPath = "Nombre";
            cbxEstado.SelectedValuePath = "Value";
            cbxEstado.ItemsSource = data;

        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            bool isError = false;

            Validar();

            if (!ValidateCodigo(txtCodigo.Text) || !ValidateNombre(txtNombre.Text)
                || !ValidateModelo(txtModelo.Text) || !ValidateTalla(txtTalla.Text) ||
                !ValidateColor(txtColor.Text) || !ValidateStock(txtStock.Text) || !ValidatePrecio(txtPrecio.Text)
                || !ValidateUsuario(cbxUsuario.Text)) isError = true;

            if (isError)
            {
                MainWindow._ts.ShowError("Revise que todos los datos sean validos");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Desea confirmar el registro?", "Alerta", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
            {
                MainWindow._ts.ShowInformation("Operacion cancelada");
                return;
            }

            MessageBox.Show(imagenProduct.Uri.ToString());
            Zapato zapato = new Zapato()
            {
                Nombre = txtCodigo.Text,
                Codigo = txtCodigo.Text,
                Modelo = txtModelo.Text,
                Talla = txtTalla.Text,
                Color = txtColor.Text,
                Stock = int.Parse(txtStock.Text),
                Precio = decimal.Parse(txtPrecio.Text),
                Img = imagenProduct.Uri.ToString().Replace("file:///", ""),
                Usuario_id = MainWindow.usuario.Id,
                Estado = bool.Parse(cbxEstado.SelectedValue.ToString()),
            };

            MessageBox.Show(zapato.Img);

            if (this.zapato != null)
            {
                zapato.Id = this.zapato.Id;
                zapato.ActualizarZapato();
                MainWindow._ts.ShowSuccess("Zapato actualizado con exito");
            }
            else
            {
                zapato.CrearZapato();
                ClearInputs();
                MainWindow._ts.ShowSuccess("Zapato creado con exito");
            }
        }

        private void ClearInputs()
        {
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtModelo.Text = "";
            txtTalla.Text = "";
            txtColor.Text = "";
            txtStock.Text = "";
            txtPrecio.Text = "";

            lblCodigo.Visibility = Visibility.Collapsed;
            lblNombre.Visibility = Visibility.Collapsed;
            lblModelo.Visibility = Visibility.Collapsed;
            lblTalla.Visibility = Visibility.Collapsed;
            lblColor.Visibility = Visibility.Collapsed;
            lblStock.Visibility = Visibility.Collapsed;
            lblPrecio.Visibility = Visibility.Collapsed;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            string campo = txt.Name;


            if (campo == "txtCodigo")
            {
                ValidateCodigo(txt.Text);
                return;
            };
            if (campo == "txtNombre")
            {
                ValidateNombre(txt.Text);
                return;
            };
            if (campo == "txtModelo")
            {

                ValidateModelo(txt.Text);
                return;
            }

            if (campo == "txtTalla")
            {
                ValidateTalla(txt.Text);
                return;
            }

            if (campo == "txtColor")
            {
                ValidateColor(txt.Text);
                return;
            }

            if (campo == "txtStock")
            {
                ValidateStock(txt.Text);
                return;
            }


            if (campo == "txtPrecio")
            {
                ValidatePrecio(txt.Text);
                return;
            }
        }

        private bool ValidateCodigo(string value)
        {
            if (value == "")
            {
                lblCodigo.Content = "El codigo es requerido";
                lblCodigo.Visibility = Visibility.Visible;
                return false;
            }

            if (value.Length <= 3)
            {
                lblCodigo.Content = "El codigo debe tener mas de 3 caracteres";
                lblCodigo.Visibility = Visibility.Visible;
                return false;
            }

            lblCodigo.Visibility = Visibility.Collapsed;
            return true;
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

        private bool ValidateModelo(string value)
        {
            if (value == "")
            {
                lblModelo.Content = "El modelo es requerido";
                lblModelo.Visibility = Visibility.Visible;
                return false;
            }

            if (value.Length <= 3)
            {
                lblModelo.Content = "El modelo debe tener mas de 3 caracteres";
                lblModelo.Visibility = Visibility.Visible;
                return false;
            }

            lblModelo.Visibility = Visibility.Collapsed;
            return true;
        }

        private bool ValidateTalla(string value)
        {
            if (value == "")
            {

                lblTalla.Content = "La talla es requerida";
                lblTalla.Visibility = Visibility.Visible;
                return false;
            }

            lblTalla.Visibility = Visibility.Collapsed;
            return true;
        }

        private bool ValidateColor(string value)
        {
            if (value == "")
            {
                lblColor.Content = "El color es requerido";
                lblColor.Visibility = Visibility.Visible;
                return false;
            }

            lblColor.Visibility = Visibility.Collapsed;
            return true;
        }

        private bool ValidateStock(string value)
        {
            if (value == "")
            {
                lblStock.Content = "El stock es requerido";
                lblStock.Visibility = Visibility.Visible;
                return false;
            }



            lblStock.Visibility = Visibility.Collapsed;
            return true;
        }

        private bool ValidatePrecio(string value)
        {
            if (value == "")
            {
                lblPrecio.Content = "El precio es requerido";
                lblPrecio.Visibility = Visibility.Visible;
                return false;
            }

            lblPrecio.Visibility = Visibility.Collapsed;
            return true;
        }

        private bool ValidateUsuario(string value)
        {
            if (value == "")
            {
                lblUsuario.Content = "El usuario es requerido";
                lblUsuario.Visibility = Visibility.Visible;
                return false;
            }

            lblUsuario.Visibility = Visibility.Collapsed;
            return true;
        }

        private void OnlyInt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regexInt = new Regex("[^0-9]+");
            e.Handled = regexInt.IsMatch(e.Text);
        }

        private void OnlyDecimal_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[,][0-9]+$|^[0-9]*[,]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void ImageSelector_ImageSelected(object sender, RoutedEventArgs e)
        {
            ImageSelector img = (ImageSelector)sender;
        }

        private void cbxUsuario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string cbxUsuario = (sender as ComboBox).SelectedItem as string;

            ValidateUsuario(cbxUsuario);
        }

        private void Validar()
        {
            ValidateCodigo(txtCodigo.Text);
            ValidateNombre(txtNombre.Text);
            ValidateModelo(txtModelo.Text);
            ValidateTalla(txtTalla.Text);
            ValidateColor(txtColor.Text);
            ValidateStock(txtStock.Text);
            ValidatePrecio(txtPrecio.Text);
            ValidateUsuario(cbxUsuario.Text);


            /* ValidateCantidad(txtCantidad.Text);
             ValidateMonto(txtMonto.Text);
             ValidateFecha(txtFecha.Text);*/
        }

        private void BtnRegresarLista_Click(object sender, RoutedEventArgs e)
        {
            var mw = Application.Current.Windows
    .Cast<Window>()
    .FirstOrDefault(window => window is MainWindow) as MainWindow;

            mw.mainNavigaion.Content = new ListaProductos();
        }

        private void CambiarImagen_Click(object sender, RoutedEventArgs e)
        {
            imagenProduct.Visibility = Visibility.Visible;
            imageView.Visibility = Visibility.Collapsed;
        }
    }
}
