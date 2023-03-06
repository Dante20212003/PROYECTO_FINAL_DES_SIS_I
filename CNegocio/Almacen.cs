using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDatos;

namespace CNegocio
{
    public class Almacen
    {
        private int id;
        private string nombre;
        private string telefono;
        private string direccion;

        AlmacenD almacenD = new AlmacenD();

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Direccion { get => direccion; set => direccion = value; }

        public List<Almacen> GetAlmacenes(string busqueda = "")
        {
            List<Almacen> listaAlmacenes = new List<Almacen>();

            foreach (DataRow R in almacenD.SelectData(busqueda).Rows)
            {
                Almacen rol = new Almacen
                {
                    Id = int.Parse(R["id"].ToString()),
                    Nombre = R["nombre"].ToString(),
                    Telefono = R["telefono"].ToString(),
                    Direccion = R["direccion"].ToString(),
                };

                listaAlmacenes.Add(rol);
            }

            return listaAlmacenes;
        }

        public void CrearAlmacen()
        {
            almacenD.InsertData(Nombre, Telefono, Direccion);
        }

        public void ActualizarAlmacen()
        {
            almacenD.UpdateData(Id, Nombre, Telefono, Direccion);
        }
    }
}
