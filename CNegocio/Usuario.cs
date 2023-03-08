using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Bogus;
using CDatos;
using MessageBox = HandyControl.Controls.MessageBox;

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
        private int rol_id;
        private string rol;
        private int almacen_id;
        private int persona_id;
        private string almacen;
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
        public int Rol_id { get => rol_id; set => rol_id = value; }
        public string Rol { get => rol; set => rol = value; }
        public int Almacen_id { get => almacen_id; set => almacen_id = value; }
        public string Almacen { get => almacen; set => almacen = value; }
        public int Persona_id { get => persona_id; set => persona_id = value; }

        public int CountUsuarios(int limit = 10, int offset = 0, string busqueda = "%%")
        {
            DataRow R = usuarioD.SelectData(limit, offset, busqueda, true).Rows[0];
            return int.Parse(R["totalUsuarios"].ToString());
        }

        public List<Usuario> GetUsuarios(int limit = 10, int offset = 0, string busqueda = "")
        {
            List<Usuario> listaUsuarios = new List<Usuario>();

            foreach (DataRow R in usuarioD.SelectData(limit, offset, busqueda, false).Rows)
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
                    Rol_id = int.Parse(R["rol_id"].ToString()),
                    Rol = R["rol"].ToString(),
                    Almacen_id = int.Parse(R["almacen_id"].ToString()),
                    Almacen = R["almacen"].ToString(),
                    Persona_id = int.Parse(R["persona_id"].ToString()),
                    Fecha = Convert.ToDateTime(R["fecha"].ToString()).ToString("dddd dd MMMM 'de' yyyy hh:mm", CultureInfo.CreateSpecificCulture("es-ES")),
                    Estado = bool.Parse(R["estado"].ToString()),
                };

                listaUsuarios.Add(usuario);
            }

            return listaUsuarios;
        }

        public void CrearUsuario()
        {
            usuarioD.InsertData(Nombre, Apellido, Ci, Telefono, Username, HashPassword(Contrasena), HorarioLaboral, Rol_id, Almacen_id, Estado);
        }

        public void ActualizarUsuario(bool isPassword = false)
        {
            string password = Contrasena;
            if (isPassword) password = HashPassword(Contrasena);

            usuarioD.UpdateData(Id, Nombre, Apellido, Ci, Telefono, Username, password, HorarioLaboral, Rol_id, Almacen_id, Persona_id, Estado, isPassword);
        }

        public Usuario Login(string username, string password)
        {
            DataRow R = usuarioD.SelectOneData("username", username);

            if (R != null)
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
                    Rol_id = int.Parse(R["rol_id"].ToString()),
                    Rol = R["rol"].ToString(),
                    Almacen_id = int.Parse(R["almacen_id"].ToString()),
                    Almacen = R["almacen"].ToString(),
                    Persona_id = int.Parse(R["persona_id"].ToString()),
                    Estado = bool.Parse(R["estado"].ToString()),
                };

                if (HashPassword(password).Equals(usuario.Contrasena))
                {
                    RolD rolD = new RolD();

                    if (usuario.Estado && bool.Parse(rolD.SelectOneData("nombre", usuario.Rol)["estado"].ToString()))
                    {
                        return usuario;
                    }

                    MessageBox.Show("El usuario no esta activo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    return null;
                };
            }

            MessageBox.Show("Usuario o contrasena incorrectas", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            return null;
        }

        public bool CheckSiExisteUsuario(string campo, string value)
        {
            return (usuarioD.SelectOneData(campo, value) != null);
        }

        private string HashPassword(string contrasena)
        {
            var sha = SHA256.Create();
            var byteArray = Encoding.Default.GetBytes(contrasena);
            var hash = sha.ComputeHash(byteArray);

            return Convert.ToBase64String(hash);
        }

        //FUNCION DE PRUEBA
        public void GenerarUsuarios(int limit)
        {
            try
            {
                Faker faker = new Faker("es_MX");

                var horarios = new[] { "Manana", "Medio Dia", "Tarde", "Noche" };

                for (int i = 0; i < limit; i++)
                {
                    string nombre = faker.Name.FirstName();
                    string apellido = faker.Name.LastName();

                    Usuario usuario = new Usuario()
                    {
                        nombre = nombre,
                        apellido = apellido,
                        ci = faker.Random.AlphaNumeric(9),
                        telefono = faker.Phone.PhoneNumber("########"),
                        username = $"{nombre}_{apellido}{faker.Random.Number(0, 999)}",
                        contrasena = "123456",
                        horarioLaboral = faker.PickRandom(horarios),
                        rol_id = 1,
                        almacen_id = 1,
                        estado = faker.Random.Bool(),
                    };


                    if (usuario.CheckSiExisteUsuario("username", usuario.username)) usuario.username += faker.Random.Word();

                    usuario.CrearUsuario();

                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"ERROR AL GENERAR \n{e.Message}");
            }
        }

        
    }
}
