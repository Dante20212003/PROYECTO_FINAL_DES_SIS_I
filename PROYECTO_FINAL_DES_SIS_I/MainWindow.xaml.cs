﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HandyControl.Data;
using MessageBox = HandyControl.Controls.MessageBox;
using SideMenuItem = HandyControl.Controls.SideMenuItem;
using CNegocio;

namespace PROYECTO_FINAL_DES_SIS_I
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isMaximized = false;
        Dictionary<string, string> paginas = new Dictionary<string, string>() {
            {"Analiticas", "Pages/Principal.xaml"},
            {"Lista de Usuarios", "Pages/Usuarios/ListaUsuarios.xaml"},
            {"Agregar Usuario", "Pages/Usuarios/CrearUsuario.xaml"}
        };

        public MainWindow()
        {
            InitializeComponent();
            mainNavigaion.Navigate(new Uri("Pages/Principal.xaml", UriKind.RelativeOrAbsolute));

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
            if (e.ClickCount != 2) return;

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
            }
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {

            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;

        }

        private void SideMenu_SelectionChanged(object sender, HandyControl.Data.FunctionEventArgs<object> e)
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
    }
}
