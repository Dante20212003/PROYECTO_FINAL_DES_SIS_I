using System;
using System.Data;
using System.Data.SqlClient;

namespace CDatos
{
    public class UsuarioD
    {
        ConexionDB conexion = new ConexionDB();

        public DataTable SelectData(int limit = 10, int offset = 0)
        {
            string query = "SELECT * FROM Usuario " +
                "ORDER BY id " +
                $"OFFSET {offset} ROWS FETCH FIRST {limit} ROWS ONLY";
            return conexion.Select(query).Tables[0];
        }

        public void UpdateData(int id, string name, string apellido, string ci, string telefono, string username, string contrasena, string horarioLaboral, bool estado)
        {
            string query = $"UPDATE Usuario SET nombre='{name}', apellido='{apellido}', ci='{ci}', telefono='{telefono}', username='{username}', " +
                $"contrasena='{contrasena}', horarioLaboral='{horarioLaboral}', estado={Convert.ToInt32(estado)} " +
                $"WHERE id={id};";

            conexion.Update(query);
        }
    }
}
