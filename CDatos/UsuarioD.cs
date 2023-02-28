using System.Data;
using System.Data.SqlClient;

namespace CDatos
{
    public class UsuarioD
    {
        ConexionDB conexion = new ConexionDB();

        public DataTable SelectData(int limit = 10, int offset = 0)
        {
            string sql = "SELECT * FROM Usuario " +
                "ORDER BY id " +
                $"OFFSET {offset} ROWS FETCH FIRST {limit} ROWS ONLY";
            return conexion.Select(sql).Tables[0];
        }
    }
}
