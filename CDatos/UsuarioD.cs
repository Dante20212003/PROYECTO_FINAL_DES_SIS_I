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
            string header = "";

            if (isCount)
            {
                header += "SELECT COUNT(*) as totalUsuarios FROM Usuario u, Rol r, Almacen a ";
            }
            else
            {
                header += $"SELECT TOP {limit} * FROM (SELECT ROW_NUMBER() OVER (ORDER BY u.id DESC) AS numRow, " +
                        "u.id, u.nombre, u.apellido, u.ci, u.telefono, u.username, u.contrasena, u.horarioLaboral, u.rol_id, r.nombre rol, u.almacen_id, a.nombre almacen, u.estado, u.fecha " +
                        "FROM Usuario u, Rol r, Almacen a ";
            }


            string sql = header +
                "WHERE (u.rol_id = r.id AND u.almacen_id = a.id) " +
                $"AND (u.nombre LIKE '%{query}%' COLLATE SQL_Latin1_General_CP1_CI_AI " +
                $"OR u.apellido LIKE '%{query}%' COLLATE SQL_Latin1_General_CP1_CI_AI " +
                $"OR u.ci LIKE '%{query}%'" +
                $"OR u.telefono LIKE '%{query}%' " +
                $"OR u.username LIKE '%{query}%' COLLATE SQL_Latin1_General_CP1_CI_AI " +
                $"OR u.horarioLaboral LIKE '%{query}%' " +
                $"OR r.nombre LIKE '%{query}%' COLLATE SQL_Latin1_General_CP1_CI_AI " +
                $"OR a.nombre LIKE '%{query}%' COLLATE SQL_Latin1_General_CP1_CI_AI) ";

            //PAGINACION
            if (!isCount)
            {
                sql += $") as users WHERE users.numRow > {offset}";
            }

            return conexion.Select(sql).Tables[0];
        }

        public DataRow SelectOneData(string campo, string value)
        {
            string sql = $"SELECT u.id, u.nombre, u.apellido, u.ci, u.telefono, u.username, u.contrasena, u.horarioLaboral, u.rol_id, r.nombre rol, u.almacen_id, a.nombre almacen, u.estado " +
                $"FROM Usuario u, Rol r, Almacen a " +
                $"WHERE (u.rol_id = r.id AND u.almacen_id = a.id) " +
                $"AND ({campo} = '{value}')";

            var result = conexion.Select(sql).Tables[0];

            if (result.Rows.Count > 0) return result.Rows[0];

            return null;
        }

        public void InsertData(string nombre, string apellido, string ci, string telefono, string username, string contrasena, string horarioLaboral, int rol_id, int almacen_id, bool estado)
        {
            string sql = $"INSERT INTO Usuario (nombre, apellido, ci, telefono, username, contrasena, horarioLaboral, rol_id, almacen_id, estado) " +
                $"VALUES ('{nombre}', '{apellido}', '{ci}', '{telefono}', '{username}', '{contrasena}', '{horarioLaboral}', {rol_id}, {almacen_id}, {Convert.ToInt32(estado)});";
            conexion.InsertOrUpdate(sql);
        }

        public void UpdateData(int id, string nombre, string apellido, string ci, string telefono, string username, string contrasena, string horarioLaboral, int rol_id, int almacen_id, bool estado, bool isPassword = false)
        {
            string sql = $"UPDATE Usuario SET nombre='{nombre}', apellido='{apellido}', ci='{ci}', telefono='{telefono}', username='{username}', " +
                $"horarioLaboral='{horarioLaboral}', rol_id={rol_id}, almacen_id={almacen_id}, estado={Convert.ToInt32(estado)} ";

            if (isPassword) sql += $", contrasena='{contrasena}'";

            sql += $"WHERE id={id};";

            conexion.InsertOrUpdate(sql);
        }
    }
}
