using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CDatos
{
    public class ZapatoD
    {
        ConexionDB conexion = new ConexionDB();

        public DataTable SelectData(int limit = 10, int offset = 0, string query = "", bool isCount = false)
        {
            string header = "";

            if (isCount)
            {
                header += "SELECT COUNT(*) as totalZapatos FROM Zapato z, Usuario u ";
            }
            else
            {
                header += $"SELECT TOP {limit} * FROM (SELECT ROW_NUMBER() OVER (ORDER BY u.id DESC) AS numRow, " +
                        "z.id, z.codigo, z.nombre, z.modelo, z.talla, z.color, z.stock, z.precio, z.img, z.usuario_id, u.nombre usuario, z.estado, z.fecha " +
                        "FROM Zapato z, Usuario u ";
            }


            string sql = header +
                "WHERE (z.usuario_id = u.id) " +
                $"AND (z.nombre LIKE '%{query}%' COLLATE SQL_Latin1_General_CP1_CI_AI " +
                $"OR z.modelo LIKE '%{query}%' COLLATE SQL_Latin1_General_CP1_CI_AI " +
                $"OR z.talla LIKE '%{query}%' " +
                $"OR z.color LIKE '%{query}%' " +
                $"OR z.stock LIKE '%{query}%' " +
                $"OR z.precio LIKE '%{query}%') ";

            //PAGINACION
            if (!isCount)
            {
                sql += $") as productos WHERE productos.numRow > {offset}";
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

        public void InsertData(string codigo, string nombre, string modelo, string talla, string color, int stock, decimal precio, string img, int usuario_id, bool estado)
        {
            string newPrecio = precio.ToString(new CultureInfo("en-US"));

            string sql = $"INSERT INTO Zapato VALUES('{codigo}','{nombre}', '{modelo}','{talla}','{color}', {stock}, {newPrecio}, '{img}', {usuario_id}, CURRENT_TIMESTAMP, {Convert.ToInt32(estado)})";
            conexion.InsertOrUpdate(sql);
        }

        public void UpdateData(int id, string codigo, string nombre, string modelo, string talla, string color, int stock, decimal precio, string img, int usuario_id, bool estado)
        {
            string newPrecio = precio.ToString(new CultureInfo("en-US"));

            string sql = $"UPDATE Zapato SET codigo='{codigo}', nombre='{nombre}', modelo='{modelo}', talla='{talla}', color='{color}', " +
                $"stock='{stock}', precio={newPrecio}, img='{img}', usuario_id={usuario_id}, estado={Convert.ToInt32(estado)} " +
                $"WHERE id={id};";
            MessageBox.Show(sql);
            conexion.InsertOrUpdate(sql);
        }
    }
}
