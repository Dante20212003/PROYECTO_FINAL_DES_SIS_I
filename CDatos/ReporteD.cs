using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDatos
{
    public class ReporteD
    {
        ConexionDB conexion = new ConexionDB();

        public DataTable SelectData()
        {
            string sql = "SELECT r.id, r.observaciones, r.usuario_id, p.nombre usuario, r.fecha FROM Reporte r, Usuario u, Persona p WHERE r.usuario_id = u.id AND u.persona_id = p.id ";
                

            return conexion.Select(sql).Tables[0];
        }

        public void InsertData(string observaciones, int usuario_id)
        {
            string sql = $"INSERT INTO Reporte(observaciones, usuario_id) VALUES('{observaciones}', {usuario_id})";
            conexion.InsertOrUpdate(sql);
        }
    }
}
