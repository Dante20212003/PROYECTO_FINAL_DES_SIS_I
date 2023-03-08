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

namespace PROYECTO_FINAL_DES_SIS_I.Components
{
    
    public partial class CardDashboard : UserControl
    {
        public CardDashboard()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty UsernameProperty =
            DependencyProperty.Register("Titulo", typeof(string), typeof(CardDashboard), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty AgeProperty =
            DependencyProperty.Register("Total", typeof(int), typeof(CardDashboard), new PropertyMetadata(0));

        public static readonly DependencyProperty FavoriteColorProperty =
            DependencyProperty.Register("Color", typeof(Color), typeof(CardDashboard), new PropertyMetadata(Color.FromRgb(0, 0, 0)));

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
        }
    }
}
