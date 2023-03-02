using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace CDatos
{
    public class UsuarioD
    {
        ConexionDB conexion = new ConexionDB();


        public DataTable SelectTotalData()
        {
            string query = "SELECT COUNT(*) as totalUsuarios FROM Usuario;";
            return conexion.Select(query).Tables[0];
        }
        public DataTable SelectData(int limit = 10, int offset = 0, string busqueda = "", bool isCount = false)
        {
            string header = isCount ? "COUNT(*) as totalUsuarios" : "*, r.nombre rol, a.nombre almacen";


            string query = $"SELECT {header} FROM Usuario u, Rol r, Almacen a " +
                "WHERE (u.rol_id = r.id AND u.almacen_id = a.id) " +
                $"AND (u.nombre LIKE '{busqueda}' COLLATE SQL_Latin1_General_CP1_CI_AI " +
                $"OR u.apellido LIKE '{busqueda}' COLLATE SQL_Latin1_General_CP1_CI_AI " +
                $"OR u.ci LIKE '{busqueda}' " +
                $"OR u.telefono LIKE '{busqueda}' " +
                $"OR u.username LIKE '{busqueda}' COLLATE SQL_Latin1_General_CP1_CI_AI " +
                $"OR u.horarioLaboral LIKE '{busqueda}' " +
                $"OR r.nombre LIKE '{busqueda}' COLLATE SQL_Latin1_General_CP1_CI_AI " +
                $"OR a.nombre LIKE '{busqueda}' COLLATE SQL_Latin1_General_CP1_CI_AI) ";
            //PAGINACION
            if (!isCount)
            {
                query += "ORDER BY u.id " +
                $"OFFSET {offset} ROWS FETCH FIRST {limit} ROWS ONLY";
            }
            
            return conexion.Select(query).Tables[0];
        }

        public void InsertData(string nombre, string apellido, string ci, string telefono, string username, string contrasena, string horarioLaboral, int rol_id, int almacen_id, bool estado)
        {
            string sql = $"INSERT INTO Usuario (nombre, apellido, ci, telefono, username, contrasena, horarioLaboral, rol_id, almacen_id, estado) " +
                $"VALUES ('{nombre}', '{apellido}', '{ci}', '{telefono}', '{username}', '{contrasena}', '{horarioLaboral}', {rol_id}, {almacen_id}, {Convert.ToInt32(estado)});";
            conexion.InsertOrUpdate(sql);
        }

        public void UpdateData(int id, string nombre, string apellido, string ci, string telefono, string username, string contrasena, string horarioLaboral, int rol_id, int almacen_id, bool estado)
        {
            string query = $"UPDATE Usuario SET nombre='{nombre}', apellido='{apellido}', ci='{ci}', telefono='{telefono}', username='{username}', " +
                $"contrasena='{contrasena}', horarioLaboral='{horarioLaboral}', rol_id={rol_id}, almacen_id={almacen_id}, estado={Convert.ToInt32(estado)} " +
                $"WHERE id={id};";

            conexion.InsertOrUpdate(query);
        }
    }
}
