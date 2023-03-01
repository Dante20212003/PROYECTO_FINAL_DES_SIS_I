using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Bogus;
using CDatos;

namespace CNegocio
{
    public class Usuario
    {
        private int id;
        private string nombre;
        private string apellido;
        private string ci;
        private string telefono;
        private string username;
        private string contrasena;
        private string horarioLaboral;
        private string fecha;
        private bool estado;



        private UsuarioD usuarioD = new UsuarioD();

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Ci { get => ci; set => ci = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Username { get => username; set => username = value; }
        public string Contrasena { get => contrasena; set => contrasena = value; }
        public string HorarioLaboral { get => horarioLaboral; set => horarioLaboral = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public bool Estado { get => estado; set => estado = value; }

        public List<Usuario> getUsuarios(int limit = 10, int offset = 0)
        {
            List<Usuario> listaUsuarios = new List<Usuario>();

            foreach (DataRow R in usuarioD.SelectData(limit, offset).Rows)
            {
                Usuario usuario = new Usuario
                {
                    Id = int.Parse(R["id"].ToString()),
                    Nombre = R["nombre"].ToString(),
                    Apellido = R["apellido"].ToString(),
                    Ci = R["ci"].ToString(),
                    Telefono = R["telefono"].ToString(),
                    Username = R["username"].ToString(),
                    Contrasena = R["contrasena"].ToString(),
                    HorarioLaboral = R["horarioLaboral"].ToString(),
                    Fecha = R["fecha"].ToString(),
                    Estado = bool.Parse(R["estado"].ToString()),
                };

                listaUsuarios.Add(usuario);
            }

            return listaUsuarios;
        }

        public void updateUsuario(Usuario usuario, bool isMultEdit = false)
        {

            usuarioD.UpdateData(usuario.id, usuario.nombre, usuario.apellido, 
                usuario.ci, usuario.telefono, usuario.username, usuario.contrasena, usuario.horarioLaboral, 
                isMultEdit ? !usuario.Estado : usuario.estado);
        }
        public void crearUsuario()
        {
            try
            {
                Faker faker = new Faker("es");
                ConexionDB db = new ConexionDB();

                for (int i = 0; i < 500; i++)
                {
                    string nombre = faker.Name.FirstName();
                    string apellido = faker.Name.LastName();

                    Usuario usuario = new Usuario()
                    {
                        nombre = nombre,
                        apellido = apellido,
                        ci = faker.Random.AlphaNumeric(9),
                        telefono = faker.Phone.PhoneNumberFormat(),
                        username = $"{nombre}_{apellido}{faker.Random.Number(0, 999)}",
                        contrasena = "123456",
                        horarioLaboral = "12:00am - 20:00pm",
                        estado = faker.Random.Bool(),
                    };


                    db.Insert(usuario.nombre, usuario.apellido, usuario.ci, usuario.telefono, usuario.username, usuario.contrasena, usuario.horarioLaboral, usuario.estado);

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
        }
    }
}
