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
        private int rol_id;
        private string rol;
        private int almacen_id;
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

        public int GetTotalUsuarios(int limit = 10, int offset = 0, string busqueda = "%%")
        {
            DataRow R = usuarioD.SelectData(limit, offset, busqueda, true).Rows[0];
            return int.Parse(R["totalUsuarios"].ToString());
        }
        public List<Usuario> getUsuarios(int limit = 10, int offset = 0, string busqueda = "")
        {
            List<Usuario> listaUsuarios = new List<Usuario>();

            foreach (DataRow R in usuarioD.SelectData(limit, offset, busqueda).Rows)
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
                    Fecha = R["fecha"].ToString(),
                    Estado = bool.Parse(R["estado"].ToString()),
                };

                listaUsuarios.Add(usuario);
            }

            return listaUsuarios;
        }

        public void ActualizarUsuario()
        {
            usuarioD.UpdateData(this.id, this.nombre, this.apellido,
                this.ci, this.telefono, this.username, this.contrasena, this.horarioLaboral, this.Rol_id, this.Almacen_id, this.estado);
        }
        public void GenerarUsuarios()
        {
            try
            {
                Faker faker = new Faker("es");

                for (int i = 0; i < 200; i++)
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
                        rol_id = 1,
                        almacen_id = 1,
                        estado = faker.Random.Bool(),
                    };

                    usuarioD.InsertData(usuario.nombre, usuario.apellido, usuario.ci, usuario.telefono, usuario.username, usuario.contrasena, usuario.horarioLaboral, usuario.rol_id, usuario.almacen_id, usuario.estado);

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
        }
    }
}
