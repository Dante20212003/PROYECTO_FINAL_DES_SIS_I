using System.Data.SqlClient;
using System.Data;
using System;
using System.Windows;

namespace CDatos
{
    public class ConexionDB
    {
        private SqlConnection connection;
        private SqlDataAdapter da;
        private SqlCommand comm;

        public void Conectar()
        {
            try
            {
                connection = new SqlConnection("Data Source=DESKTOP-HA2D645;Initial Catalog=Tarea4;Integrated Security=True");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error en DB (Conectar): \n{e.Message}");
            }
        }

        public DataSet Select(string query)
        {
            Conectar();

            DataSet ds = new DataSet();

            try
            {
                connection.Open();

                da = new SqlDataAdapter(query, connection);
                da.Fill(ds);

                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error en DB (Select): \n{e.Message}");
            }


            return ds;

        }

        public void InsertOrUpdate(string query)
        {
            try
            {
                Conectar();

                comm = new SqlCommand(query, connection);

                connection.Open();
                comm.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error en DB (InsertOrUpdate): \n{e.Message}");
            }
        }

        public SqlDataReader SelectOne(string query)
        {

            Conectar();

            SqlDataReader result = null;


            try
            {
                connection.Open();
                comm = new SqlCommand(query, connection);

                result = comm.ExecuteReader();
                result.Read();

                MessageBox.Show(result["stock"].ToString());
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error en DB (SelectOne): \n{e.Message}");
            }

            return result;

        }
    }
}
