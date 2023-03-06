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
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using ComboBox = System.Windows.Controls.ComboBox;
using MessageBox = HandyControl.Controls.MessageBox;
using TextBox = System.Windows.Controls.TextBox;

namespace PROYECTO_FINAL_DES_SIS_I.Pages.Usuarios
{
    public partial class CrearUsuario : UserControl
    {
        private Usuario usuario;
        public CrearUsuario(Usuario _usuario = null)
        {
            InitializeComponent();

            if (_usuario != null)
            {
                usuario = _usuario;

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListarProductos();
            ListarClientes();
            ListarEstados();
            ListarHorarios();

            cbxEstado.SelectedValue = "True";
            cbxHorario.SelectedValue = "Mañana";

            if (cbxRol.Items.Count > 0) cbxRol.SelectedIndex = 1;
            if (cbxAlmacen.Items.Count > 0) cbxAlmacen.SelectedIndex = 0;

            if (usuario != null)
            {
                txtNombre.Text = usuario.Nombre;
                txtApellido.Text = usuario.Apellido;
                txtCi.Text = usuario.Ci;
                txtTelefono.Text = usuario.Telefono;
                txtUsername.Text = usuario.Username;

                cbxEstado.SelectedValue = usuario.Estado.ToString();
                cbxHorario.SelectedValue = usuario.HorarioLaboral;
                cbxRol.SelectedValue = usuario.Rol_id;
                cbxAlmacen.SelectedValue = usuario.Almacen_id;


                if (usuario.Username == "admin")
                {
                    txtUsername.IsEnabled = false;
                    cbxRol.IsEnabled = false;
                    cbxEstado.IsEnabled = false;
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

        public void ListarHorarios()
        {
            List<object> data = new List<object>()
            {
                new {Value = "Mañana", Nombre = "Mañana" },
                new {Value = "Medio Dia", Nombre = "Medio Dia" },
                new {Value = "Tarde", Nombre = "Tarde" },
                new {Value = "Noche", Nombre = "Noche" }
            };

            cbxHorario.DisplayMemberPath = "Nombre";
            cbxHorario.SelectedValuePath = "Value";
            cbxHorario.ItemsSource = data;
        }

        public void ListarProductos()
        {
            Rol rol = new Rol();
            var data = rol.GetRoles();

            cbxRol.DisplayMemberPath = "Nombre";
            cbxRol.SelectedValuePath = "Id";
            cbxRol.ItemsSource = data;
        }

        public void ListarClientes()
        {

            Almacen almacen = new Almacen();
            var data = almacen.GetAlmacenes();

            cbxAlmacen.DisplayMemberPath = "Nombre";
            cbxAlmacen.SelectedValuePath = "Id";
            cbxAlmacen.ItemsSource = data;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            bool isError = false;

            Validar();

            if (!ValidateNombre(txtNombre.Text) || !ValidateApellido(txtApellido.Text)
                || !ValidateCi(txtCi.Text) || !ValidateTelefono(txtTelefono.Text) ||
                !ValidateUsername(txtUsername.Text) || !ValidateContrasena(txtContrasena.Text) || !ValidateHorario(cbxHorario.Text)
                || !ValidateRol(cbxRol.Text) || !ValidateAlmacen(cbxAlmacen.Text)) isError = true;

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

            int almacen_id = ((Almacen)cbxAlmacen.SelectedItem).Id;
            int rol_id = ((Rol)cbxRol.SelectedItem).Id;

            Usuario usuario = new Usuario()
            {

                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Ci = txtCi.Text,
                Telefono = txtTelefono.Text,
                Username = txtUsername.Text,
                Contrasena = txtContrasena.Text,
                HorarioLaboral = cbxHorario.Text,
                Rol_id = rol_id,
                Almacen_id = almacen_id,
                Estado = bool.Parse(cbxEstado.SelectedValue.ToString())
            };

            if (this.usuario != null)
            {
                usuario.Id = this.usuario.Id;
                usuario.ActualizarUsuario(txtContrasena.Text.Length > 0);
                MainWindow._ts.ShowSuccess("Usuario actualizado con exito");
            }
            else
            {
                usuario.CrearUsuario();
                ClearInputs();
                MainWindow._ts.ShowSuccess("Usuario creado con exito");
            }
        }

        private void ClearInputs()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtCi.Text = "";
            txtTelefono.Text = "";
            txtUsername.Text = "";
            txtContrasena.Text = "";

            lblNombre.Visibility = Visibility.Collapsed;
            lblApellido.Visibility = Visibility.Collapsed;
            lblCi.Visibility = Visibility.Collapsed;
            lblTelefono.Visibility = Visibility.Collapsed;
            lblUsername.Visibility = Visibility.Collapsed;
            lblContrasena.Visibility = Visibility.Collapsed;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            string campo = txt.Name;

            Random random = new Random();
            string newNombre = txtNombre.Text.Split(' ')[0];
            string newApellido = txtApellido.Text.Split(' ')[0];

            if (campo == "txtNombre")
            {
                if (usuario == null) txtUsername.Text = newNombre + newApellido + random.Next(999);
                ValidateNombre(txt.Text);
                return;
            };
            if (campo == "txtApellido")
            {
                if (usuario == null) txtUsername.Text = newNombre + newApellido + random.Next(999);
                ValidateApellido(txt.Text);
                return;
            }

            if (campo == "txtCi")
            {
                ValidateCi(txt.Text);
                return;
            }

            if (campo == "txtTelefono")
            {
                ValidateTelefono(txt.Text);
                return;
            }

            if (campo == "txtUsername")
            {
                ValidateUsername(txt.Text);
                return;
            }


            if (campo == "txtContrasena")
            {
                ValidateContrasena(txt.Text);
                return;
            }


            if (campo == "cbxHorario")
            {
                ValidateHorario(txt.Text);
                return;
            }


            if (campo == "cbxRol")
            {
                ValidateRol(txt.Text);
                return;
            }


            if (campo == "cbxAlmacen")
            {
                ValidateAlmacen(txt.Text);
                return;
            }
        }

        private void cbxRol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string cbxRol = (sender as ComboBox).SelectedItem as string;
            ValidateRol(cbxRol);
        }

        private void cbxAlmacen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string cbxAlmacen = (sender as ComboBox).SelectedItem as string;
            ValidateAlmacen(cbxAlmacen);
        }

        private void cbxHorario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string cbxHorario = (sender as ComboBox).SelectedItem as string;
            ValidateHorario(cbxHorario);
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

        private bool ValidateApellido(string value)
        {
            if (value == "")
            {
                lblApellido.Content = "El apellido es requerido";
                lblApellido.Visibility = Visibility.Visible;
                return false;
            }

            if (value.Length <= 3)
            {
                lblApellido.Content = "El apellido debe tener mas de 3 caracteres";
                lblApellido.Visibility = Visibility.Visible;
                return false;
            }

            lblApellido.Visibility = Visibility.Collapsed;
            return true;
        }

        private bool ValidateCi(string value)
        {
            if (value.Length > 0 && value.Length <= 7)
            {
                lblCi.Content = "El ci debe tener al menos 8 digitos";
                lblCi.Visibility = Visibility.Visible;
                return false;
            }

            if (value.Length > 0 && value.Length >= 12)
            {
                lblCi.Content = "El ci no es valido";
                lblCi.Visibility = Visibility.Visible;
                return false;
            }

            lblCi.Visibility = Visibility.Collapsed;
            return true;
        }

        private bool ValidateTelefono(string value)
        {
            if (value == "")
            {
                lblTelefono.Content = "El telefono es requerido";
                lblTelefono.Visibility = Visibility.Visible;
                return false;
            }

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

        private bool ValidateUsername(string value)
        {
            if (value == "")
            {
                lblUsername.Content = "El usuario es requerido";
                lblUsername.Visibility = Visibility.Visible;
                return false;
            }

            Usuario usuario = new Usuario();



            if (usuario.CheckSiExisteUsuario("username", value))
            {
                if (this.usuario != null && value == this.usuario.Username)
                {
                    lblUsername.Visibility = Visibility.Collapsed;
                    return true;
                }

                lblUsername.Content = "El usuario ya esta registrado";
                lblUsername.Visibility = Visibility.Visible;
                return false;
            }

            lblUsername.Visibility = Visibility.Collapsed;
            return true;
        }

        private bool ValidateContrasena(string value)
        {
            if (this.usuario == null && value == "")
            {
                lblContrasena.Content = "La contraseña es requerido";
                lblContrasena.Visibility = Visibility.Visible;
                return false;
            }



            lblContrasena.Visibility = Visibility.Collapsed;
            return true;
        }

        private bool ValidateHorario(string value)
        {
            if (value == "")
            {
                lblHorario.Content = "El horario es requerido";
                lblHorario.Visibility = Visibility.Visible;
                return false;
            }


            lblRol.Visibility = Visibility.Collapsed;
            return true;
        }

        private bool ValidateRol(string value)
        {
            if (value == "")
            {
                lblRol.Content = "El rol es requerido";
                lblRol.Visibility = Visibility.Visible;
                return false;
            }

            try
            {
                int rol_id = ((Rol)cbxRol.SelectedItem).Id;
            }
            catch
            {
                lblRol.Content = "Compruebe que el rol sea valido";
                lblRol.Visibility = Visibility.Visible;
                return false;
            }

            lblRol.Visibility = Visibility.Collapsed;
            return true;
        }

        private bool ValidateAlmacen(string value)
        {

            if (value == "")
            {
                lblAlmacen.Content = "El almacen es requerido";
                lblAlmacen.Visibility = Visibility.Visible;
                return false;
            }

            try
            {
                int cliente_id = ((Almacen)cbxAlmacen.SelectedItem).Id;
            }
            catch
            {
                lblAlmacen.Content = "Compruebe que el almacen sea valido";
                lblAlmacen.Visibility = Visibility.Visible;
                return false;
            }

            lblAlmacen.Visibility = Visibility.Collapsed;
            return true;
        }

        private void OnlyInt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regexInt = new Regex("[^0-9]+");
            e.Handled = regexInt.IsMatch(e.Text);
        }

        private void Validar()
        {
            ValidateNombre(txtNombre.Text);
            ValidateApellido(txtApellido.Text);
            ValidateCi(txtCi.Text);
            ValidateTelefono(txtTelefono.Text);
            ValidateUsername(txtUsername.Text);
            ValidateContrasena(txtContrasena.Text);
            ValidateHorario(cbxHorario.Text);
            ValidateRol(cbxRol.Text);
            ValidateAlmacen(cbxAlmacen.Text);

            /* ValidateCantidad(txtCantidad.Text);
             ValidateMonto(txtMonto.Text);
             ValidateFecha(txtFecha.Text);*/
        }

        private void GenerarContrasena_Click(object sender, RoutedEventArgs e)
        {
            Guid guid = Guid.NewGuid();
            string newApellido = txtApellido.Text.Split(' ')[0].ToLower();
            Random random = new Random();

            txtContrasena.Text = newApellido + guid.ToString().Split('-')[0].Substring(0, newApellido.Length > 3 ? 3 : 8) + random.Next(0, 99);
        }
    }
}
