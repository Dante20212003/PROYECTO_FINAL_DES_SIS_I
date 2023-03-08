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
using CDatos;
using CNegocio;
using MessageBox = HandyControl.Controls.MessageBox;

namespace PROYECTO_FINAL_DES_SIS_I.Pages.Usuarios
{
    /// <summary>
    /// Interaction logic for CrearRol.xaml
    /// </summary>
    public partial class CrearRol : UserControl
    {

        private Rol rol;
        public CrearRol(Rol _rol = null)
        {
            InitializeComponent();

            if (_rol != null)
            {
                rol = _rol;

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListarEstados();

            cbxEstado.SelectedValue = "True";

            if (rol != null)
            {
                btnAgregar.Content = "Actualizar";
                title.Text = "Actualizar";
                txtNombre.Text = rol.Nombre;
                cbxEstado.SelectedValue = rol.Estado.ToString();

                if (rol.Id == 1)
                {
                    txtNombre.IsEnabled = false;
                    cbxEstado.IsEnabled = false;
                    btnAgregar.IsEnabled = false;
                }
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

            if (!ValidateNombre(txtNombre.Text)) isError = true;

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


            Rol rol = new Rol()
            {

                Nombre = txtNombre.Text,
                Estado = bool.Parse(cbxEstado.SelectedValue.ToString())
            };

            if (this.rol != null)
            {
                rol.Id = this.rol.Id;
                rol.ActualizarRol();
                MainWindow._ts.ShowSuccess("Rol actualizado con exito");
            }
            else
            {
                rol.CrearRol();
                ClearInputs();
                MainWindow._ts.ShowSuccess("Rol creado con exito");
            }
        }

        private void ClearInputs()
        {
            txtNombre.Text = "";

            lblNombre.Visibility = Visibility.Collapsed;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;

            ValidateNombre(txt.Text);

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
    }
}
