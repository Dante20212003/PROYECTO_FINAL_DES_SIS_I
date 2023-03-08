using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace CDatos
{
    public class UsuarioD
    {
        ConexionDB conexion = new ConexionDB();

        public DataTable SelectData(int limit = 10, int offset = 0, string query = "", bool isCount = false)
        {
            string header = isCount ? "SELECT COUNT(*) totalUsuarios" : $"SELECT TOP {limit} *";


            string sql = $"{header} FROM (SELECT ROW_NUMBER() OVER (ORDER BY u.id DESC) AS numRow, " +
                    "u.id, p.nombre, p.apellido, p.ci, p.telefono, u.username, u.contrasena, u.horarioLaboral, u.rol_id, r.nombre rol, u.almacen_id, a.nombre almacen, u.persona_id, u.estado, u.fecha " +
                    "FROM Usuario u, Persona p, Rol r, Almacen a " +

                "WHERE (u.rol_id = r.id AND u.almacen_id = a.id AND u.persona_id = p.id) " +
                $"AND (p.nombre LIKE '%{query}%' COLLATE SQL_Latin1_General_CP1_CI_AI " +
                $"OR p.apellido LIKE '%{query}%' COLLATE SQL_Latin1_General_CP1_CI_AI " +
                $"OR p.ci LIKE '%{query}%' " +
                $"OR p.telefono LIKE '%{query}%' " +
                $"OR u.username LIKE '%{query}%' COLLATE SQL_Latin1_General_CP1_CI_AI " +
                $"OR u.horarioLaboral LIKE '%{query}%' " +
                $"OR r.nombre LIKE '%{query}%' COLLATE SQL_Latin1_General_CP1_CI_AI " +
                $"OR a.nombre LIKE '%{query}%' COLLATE SQL_Latin1_General_CP1_CI_AI)) as users ";

            //PAGINACION
            if (!isCount)
            {
                sql += $"WHERE users.numRow > {offset}";
            }

            return conexion.Select(sql).Tables[0];
        }

        public DataRow SelectOneData(string campo, string value)
        {
            string sql = $"SELECT u.id, p.nombre, p.apellido, p.ci, p.telefono, u.username, u.contrasena, u.horarioLaboral, u.rol_id, r.nombre rol, u.persona_id, u.estado " +
                $"FROM Usuario u, Persona p, Rol r " +
                $"WHERE (u.rol_id = r.id AND u.persona_id = p.id) " +
                $"AND ({campo} = '{value}')";

            var result = conexion.Select(sql).Tables[0];

            if (result.Rows.Count > 0) return result.Rows[0];

            return null;
        }

        public void InsertData(string nombre, string apellido, string ci, string telefono, string username, string contrasena, string horarioLaboral, int rol_id, int almacen_id, bool estado)
        {
            string sql = $"INSERT INTO Persona (nombre,apellido,telefono,ci) VALUES ('{nombre}', '{apellido}' ,'{telefono}','{ci}'); " +
                "SELECT * FROM Persona WHERE id = SCOPE_IDENTITY() ;";

            DataRow persona = conexion.Select(sql).Tables[0].Rows[0];

            if (persona == null) return;

            sql = $"INSERT INTO Usuario (username, contrasena, horarioLaboral, rol_id, almacen_id, persona_id, estado) " +
                $"VALUES ('{username}', '{contrasena}', '{horarioLaboral}', {rol_id}, {almacen_id}, {persona["id"]}, {Convert.ToInt32(estado)});";
            conexion.InsertOrUpdate(sql);
        }

        public void UpdateData(int id, string nombre, string apellido, string ci, string telefono, string username, string contrasena, string horarioLaboral, int rol_id, int almacen_id, int persona_id, bool estado, bool isPassword = false)
        {
            string sql = $"UPDATE Persona SET nombre='{nombre}', apellido='{apellido}', ci='{ci}', telefono='{telefono}' " +
                $"WHERE id={persona_id}";
            conexion.InsertOrUpdate(sql);

            sql = $"UPDATE Usuario SET  username='{username}', horarioLaboral='{horarioLaboral}', rol_id={rol_id}, almacen_id={almacen_id}, estado={Convert.ToInt32(estado)} ";

            if (isPassword) sql += $", contrasena='{contrasena}'";
            sql += $"WHERE id={id};";
            conexion.InsertOrUpdate(sql);
        }
    }
}
