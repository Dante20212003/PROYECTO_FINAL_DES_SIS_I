using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDatos;

namespace CNegocio
{
    public class Rol
    {
        private int id;
        private string nombre;
        private bool estado;

        RolD rolD = new RolD();

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public bool Estado { get => estado; set => estado = value; }

        public List<Rol> GetRoles(string busqueda = "")
        {
            List<Rol> listaRoles = new List<Rol>();

            foreach (DataRow R in rolD.SelectData(busqueda).Rows)
            {
                Rol rol = new Rol
                {
                    Id = int.Parse(R["id"].ToString()),
                    Nombre = R["nombre"].ToString(),
                    Estado = bool.Parse(R["estado"].ToString()),
                };

                listaRoles.Add(rol);
            }

            return listaRoles;
        }

        public void CrearRol()
        {
            rolD.InsertData(Nombre, Estado);
        }

        public void ActualizarRol()
        {
            rolD.UpdateData(Id, Nombre, Estado);
        }
    }
}
