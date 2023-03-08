using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using CDatos;
using HandyControl.Controls;

namespace CNegocio
{
    public class Zapato
    {
        private int id;
        private string codigo;
        private string nombre;
        private string modelo;
        private string talla;
        private string color;
        private int stock;
        private decimal precio;
        private string img;
        private int usuario_id;
        private string usuario;
        private string fecha;
        private bool estado;

        ZapatoD zapatoD = new ZapatoD();

        public int Id { get => id; set => id = value; }
        public string Codigo { get => codigo; set => codigo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Modelo { get => modelo; set => modelo = value; }
        public string Talla { get => talla; set => talla = value; }
        public string Color { get => color; set => color = value; }
        public int Stock { get => stock; set => stock = value; }
        public decimal Precio { get => precio; set => precio = value; }
        public string Img { get => img; set => img = value; }
        public int Usuario_id { get => usuario_id; set => usuario_id = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public bool Estado { get => estado; set => estado = value; }

        public int CountZapatos(int limit = 10, int offset = 0, string busqueda = "%%")
        {
            DataRow R = zapatoD.SelectData(limit, offset, busqueda, true).Rows[0];
            return int.Parse(R["totalZapatos"].ToString());
        }

        public List<Zapato> GetZapatos(int limit = 10, int offset = 0, string busqueda = "", bool isCount = false, bool isStock = false, bool today = false)
        {
            List<Zapato> listaZapatos = new List<Zapato>();

            foreach (DataRow R in zapatoD.SelectData(limit, offset, busqueda, isCount, isStock, today).Rows)
            {
                Zapato zapato = new Zapato
                {
                    Id = int.Parse(R["id"].ToString()),
                    Nombre = R["nombre"].ToString(),
                    Codigo = R["Codigo"].ToString(),
                    Modelo = R["modelo"].ToString(),
                    Talla = R["talla"].ToString(),
                    Color = R["color"].ToString(),
                    Stock = int.Parse(R["stock"].ToString()),
                    Precio = decimal.Parse(R["precio"].ToString()),
                    Img = R["img"].ToString(),
                    Usuario_id = int.Parse(R["usuario_id"].ToString()),
                    Usuario = R["usuario"].ToString(),
                    Fecha = Convert.ToDateTime(R["fecha"].ToString()).ToString("dddd dd MMMM 'de' yyyy hh:mm", CultureInfo.CreateSpecificCulture("es-ES")),
                    Estado = bool.Parse(R["estado"].ToString()),
                };

                if (zapato.Img.Length == 0) zapato.Img = "https://images.vexels.com/media/users/3/142961/isolated/preview/9031943c6d5353510bc611c6be779b2c-zapatos-rojos-zapatillas-ropa.png";

                listaZapatos.Add(zapato);
            }

            return listaZapatos;
        }

        public void CrearZapato()
        {
            zapatoD.InsertData(Codigo, Nombre, Modelo, Talla, Color, Stock, Precio, Img, Usuario_id, Estado);
        }

        public void ActualizarZapato()
        {
            zapatoD.UpdateData(Id, Codigo, Nombre, Modelo, Talla, Color, Stock, Precio, Img, Usuario_id, Estado);
        }

        public void GenerarZapatos(int limit)
        {
            try
            {
                Usuario usuario = new Usuario();
                int limitUsuario = usuario.CountUsuarios();

                Faker faker = new Faker("es_MX");

                var colores = new[] { "Rojo", "Medio Dia", "Tarde", "Noche" };

                for (int i = 0; i < limit; i++)
                {
                    int user_id = faker.Random.Number(1, limitUsuario);
                    if (usuario.CheckSiExisteUsuario("u.id", user_id.ToString()))
                    {
                        Zapato zapato = new Zapato()
                        {
                            codigo = faker.Commerce.Ean8(),
                            nombre = faker.Commerce.ProductName(),
                            modelo = faker.Random.Words(faker.Random.Number(1, 3)),
                            talla = faker.Random.Word(),
                            color = faker.Commerce.Color(),
                            stock = faker.Random.Number(0, 300),
                            precio = decimal.Parse(faker.Commerce.Price(100, 700)),

                            usuario_id = user_id,
                            fecha = faker.Date.Recent().ToString(),
                            estado = faker.Random.Bool()
                        };

                        zapato.CrearZapato();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"ERROR AL GENERAR \n{e.Message}");
            }
        }

        public List<Zapato> GetAll()
        {
            List<Zapato> listaZapatos = new List<Zapato>();

            foreach (DataRow R in zapatoD.SelectAllData().Rows)
            {
                Zapato zapato = new Zapato
                {
                    Id = int.Parse(R["id"].ToString()),
                    Nombre = R["nombre"].ToString(),
                    Codigo = R["Codigo"].ToString(),
                    Modelo = R["modelo"].ToString(),
                    Talla = R["talla"].ToString(),
                    Color = R["color"].ToString(),
                    Stock = int.Parse(R["stock"].ToString()),
                    Precio = decimal.Parse(R["precio"].ToString()),
                    Img = R["img"].ToString(),
                    Usuario = R["usuario"].ToString(),
                    Fecha = Convert.ToDateTime(R["fecha"].ToString()).ToString("dddd dd MMMM 'de' yyyy hh:mm", CultureInfo.CreateSpecificCulture("es-ES")),
                    Estado = bool.Parse(R["estado"].ToString()),
                };

                if (zapato.Img.Length == 0) zapato.Img = "https://images.vexels.com/media/users/3/142961/isolated/preview/9031943c6d5353510bc611c6be779b2c-zapatos-rojos-zapatillas-ropa.png";

                listaZapatos.Add(zapato);
            }

            return listaZapatos;
        }
        /*
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
         */
    }
}
