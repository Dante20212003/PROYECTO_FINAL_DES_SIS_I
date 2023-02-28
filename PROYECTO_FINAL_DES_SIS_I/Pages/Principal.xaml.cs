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
using Bogus;
using PROYECTO_FINAL_DES_SIS_I.Model;

namespace PROYECTO_FINAL_DES_SIS_I.Pages
{
    /// <summary>
    /// Interaction logic for Principal.xaml
    /// </summary>
    public partial class Principal : Page
    {
        public Principal()
        {

            try
            {
                InitializeComponent();
                List<Zapato> ListaZapatos = new List<Zapato>();
                var faker = new Faker("es");

                for (int i = 0; i < 10; i++)
                {
                    var o = new Zapato()
                    {
                        id = 1,
                        codigo = faker.Finance.CreditCardNumber(),
                        nombre = faker.Random.Word(),
                        modelo = faker.Vehicle.Model(),
                        talla = faker.Random.Number(4, 60).ToString(),
                        color = faker.Commerce.Color(),
                        stock = faker.Random.Number(0, 100),
                        precio = decimal.Parse(faker.Commerce.Price(100, 500)),
                        img = faker.Image.LoremFlickrUrl(320,240,"shoes"),
                        usuario_id = faker.Name.FullName(),
                        fecha = faker.Date.Recent().ToString(),
                        estado = faker.Random.Bool()

                    };


                    ListaZapatos.Add(o);
                }
               



                DataGrid.ItemsSource = ListaZapatos;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());

            }
        }
    }
}
