using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CDatos
{
    public class AlmacenD
    {
        ConexionDB conexion = new ConexionDB();
        public DataTable SelectData()
        {
            string sql = "SELECT * From Almacen";

            return conexion.Select(sql).Tables[0];
        }

        public DataRow SelectOneData(int id)
        {
            string sql = $"SELECT TOP 1 a.id, a.nombre, a.direccion, a.telefono FROM Almacen a, Usuario u";

            return conexion.Select(sql).Tables[0].Rows[0];
        }

        public void InsertData(string nombre, string telefono, string direccion)
        {
            string sql = $"INSERT INTO Almacen VALUES('{nombre}', '{direccion}', '{telefono}')";
            conexion.InsertOrUpdate(sql);
        }

        public void UpdateData(int id, string nombre, string telefono, string direccion)
        {
            string sql = $"UPDATE Almacen SET nombre='{nombre}', telefono='{telefono}', direccion='{direccion}' " +
                $"WHERE id={id};";
            MessageBox.Show(sql);
            conexion.InsertOrUpdate(sql);
        }
    }
}
