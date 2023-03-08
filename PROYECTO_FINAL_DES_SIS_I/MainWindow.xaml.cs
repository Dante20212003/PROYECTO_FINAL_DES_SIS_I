using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HandyControl.Data;
using MessageBox = HandyControl.Controls.MessageBox;
using SideMenuItem = HandyControl.Controls.SideMenuItem;
using CNegocio;
using ToastNotifications.Core;
using HandyControl.Controls;
using PROYECTO_FINAL_DES_SIS_I.Components;
using ScrollViewer = System.Windows.Controls.ScrollViewer;
using HandyControl.Themes;
using Window = HandyControl.Controls.Window;

namespace PROYECTO_FINAL_DES_SIS_I
{
    public partial class MainWindow : Window
    {
        public static Usuario usuario;
        public static Toast.Toast _ts;

        Dictionary<string, string> paginas = new Dictionary<string, string>() {
            {"Analiticas", "Pages/Dashboard.xaml"},//
            {"Lista de Usuarios", "Pages/Usuarios/ListaUsuarios.xaml"},//
            {"Administracion de Roles", "Pages/Usuarios/ListaRoles.xaml"},//
            {"Lista de Zapatos", "Pages/Productos/ListaProductos.xaml"},//
             {"Agregar Zapato", "Pages/Productos/CrearProducto.xaml"},//
            {"Almacen", "Pages/Almacens/Formulario.xaml"}, //PEDINDIENTE
            {"Generacion de Data", "Pages/GenerarData.xaml"},
             {"Reportes", "Pages/Reportes/Reportes.xaml"},//
             {"Nuevo Reporte", "Pages/Reportes/GenerarReporte.xaml"},//
        };

        public MainWindow(Usuario _usuario)
        {

            InitializeComponent();


            try
            {
                usuario = _usuario;
            }
            catch
            {
                MessageBox.Show("NO TIENES UNA SESION", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _ts = new Toast.Toast();
            Unloaded += OnUnload;

            _ts.ShowSuccess($"BIENVENIDO USUARIO {usuario.Rol.ToUpper()}");

            mainNavigaion.Navigate(new Uri("Pages/Dashboard.xaml", UriKind.RelativeOrAbsolute));

            Almacen almacen = new Almacen();

            if (almacen.GetAlmacenes().Count == 0)
            {
                MessageBox.Show("VACIO");
                PopupWindow popup = new PopupWindow()
                {
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    AllowsTransparency = true,
                    WindowStyle = WindowStyle.None,
                    Effect = null,
                    ShowTitle = false,
                    ShowInTaskbar = false
                };

                popup.PopupElement = new FormAlmacen();
                popup.ShowDialog();
            }

            if (usuario.Rol != "Administrador") menuAdmin.Visibility = Visibility.Collapsed;
            lblUsuario.Content = usuario.Nombre.Split(' ')[0] + " " + usuario.Apellido.Split(' ')[0];
            lblRol.Content = usuario.Rol;
        }

        public static void mostrarToast(Action<string, MessageOptions> action, string msg = "")
        {
            MessageOptions opts = new MessageOptions
            {
                FreezeOnMouseEnter = true,
                UnfreezeOnMouseLeave = true,
                ShowCloseButton = true,
                FontSize = 14,
            };
            action(msg, opts);
        }

        private void OnUnload(object sender, RoutedEventArgs e)
        {
            _ts.OnUnloaded();
        }

        private void btnCerrarVentana(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(new MessageBoxInfo
            {
                Message = "Estas seguro de cerrar la aplicacion",
                Caption = "Alerta",
                Button = MessageBoxButton.YesNo,
                IconKey = ResourceToken.AskGeometry,
                IconBrushKey = ResourceToken.AccentBrush,
                ConfirmContent = "Si",
            }); ;

            if (result == MessageBoxResult.Yes) Close();
        }

        private void btnMaximizarVentana(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimizarVentana(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) DragMove();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            /*  if (e.ClickCount != 2) return;

              if (!isMaximized)
              {
                  WindowState = WindowState.Maximized;
                  isMaximized = true;
              }
              else
              {
                  WindowState = WindowState.Normal;
                  Width = 1080;
                  Height = 720;
                  isMaximized = false;
              }*/
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {

            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;

        }

        private void SideMenu_SelectionChanged(object sender, FunctionEventArgs<object> e)
        {
            try
            {
                SideMenuItem sideMenuItem = e.Info as SideMenuItem;
                string pagina = sideMenuItem?.Header?.ToString();
                mainNavigaion.Navigate(new Uri(paginas[pagina], UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void BtnCerrarSesion(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            this.Close();
            login.Show();
        }

        private void BtnTheme(object sender, RoutedEventArgs e)
        {
            if (ThemeManager.Current.ActualApplicationTheme.ToString() == "Light") ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
            else ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;

        }
    }
}
