using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CDatos
{
    public class RolD
    {
        ConexionDB conexion = new ConexionDB();

        public DataTable SelectData(string query = "")
        {
            string sql = "SELECT * FROM Rol " +
                $"WHERE nombre LIKE '%{query}%'";

            return conexion.Select(sql).Tables[0];
        }

        public DataRow SelectOneData(string campo, string value)
        {
            string sql = $"SELECT * FROM Rol WHERE {campo} = '{value}'";

            var result = conexion.Select(sql).Tables[0];

            if (result.Rows.Count > 0) return result.Rows[0];

            return null;
        }

        public void InsertData(string nombre, bool estado)
        {
            string sql = $"INSERT INTO Rol VALUES('{nombre}', {Convert.ToInt32(estado)})";
            conexion.InsertOrUpdate(sql);
        }

        public void UpdateData(int id, string nombre, bool estado)
        {
            string sql = $"UPDATE Rol SET nombre='{nombre}', estado={Convert.ToInt32(estado)} " +
                $"WHERE id={id};";
            conexion.InsertOrUpdate(sql);
        }
    }
}
