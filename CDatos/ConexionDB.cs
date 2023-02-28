using System.Data.SqlClient;
using System.Data;
using System;
using System.Windows;

namespace CDatos
{
    public class ConexionDB
    {
        private string connectionString = "Data Source=DESKTOP-HA2D645;Initial Catalog=Inventario;Integrated Security=True";
        private SqlConnection connection;

        public void Conectar()
        {
            try
            {
                connection = new SqlConnection("Data Source=DESKTOP-HA2D645;Initial Catalog=Inventario;Integrated Security=True");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error en DB: \n{e.Message}");
            }
        }

        public DataSet Select(string query)
        {
            Conectar();

            DataSet ds = new DataSet();

            try
            {
                connection.Open();

                SqlDataAdapter da = new SqlDataAdapter(query, connection);
                da.Fill(ds);

                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error en DB: \n{e.Message}");
            }


            return ds;

        }

        public void Insert(string name, string apellido, string ci, string telefono, string username, string password, string horarioLaboral, bool estado)
        {
            Conectar();
            try
            {
                string query = "INSERT INTO Usuario (nombre,apellido,ci, telefono, username,contrasena,horarioLaboral, estado) VALUES" +
                " (@name, @apellido, @ci, @telefono, @username, @password, @horarioLaboral, @estado)";




                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@apellido", apellido);
                    command.Parameters.AddWithValue("@ci", ci);
                    command.Parameters.AddWithValue("@telefono", telefono);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@horarioLaboral", horarioLaboral);
                    command.Parameters.AddWithValue("@estado", estado);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error en DB: \n{e.Message}");
            }


        }
    }
}
