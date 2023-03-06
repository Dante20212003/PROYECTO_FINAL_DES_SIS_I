using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDatos
{
    public class AlmacenD
    {
        ConexionDB conexion = new ConexionDB();
        public DataTable SelectData(string query = "")
        {
            string sql = "SELECT * FROM Almacen";

            return conexion.Select(sql).Tables[0];
        }

        public void InsertData(string nombre, string telefono, string direccion)
        {
            string sql = $"INSERT INTO Almacen VALUES('{nombre}', '{direccion}', '{telefono}')";
            conexion.InsertOrUpdate(sql);
        }

        public void UpdateData(int id, string nombre, string telefono, string direccion)
        {
            string sql = $"UPDATE Almacen SET nombre='{nombre}', telefono='{telefono}', direccion={direccion} " +
                $"WHERE id={id};";
            conexion.InsertOrUpdate(sql);
        }
    }
}
