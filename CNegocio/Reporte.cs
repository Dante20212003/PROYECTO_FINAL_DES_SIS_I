using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDatos;

namespace CNegocio
{
    public class Reporte
    {
        private int id;
        private string observaciones;
        private int usuario_id;
        private string usuario;
        private string fecha;

        ReporteD reporteD = new ReporteD();

        public int Id { get => id; set => id = value; }
        public string Observaciones { get => observaciones; set => observaciones = value; }
        public int Usuario_id { get => usuario_id; set => usuario_id = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public string Fecha { get => fecha; set => fecha = value; }

        public List<Reporte> GetReportes()
        {
            List<Reporte> listaReportes = new List<Reporte>();

            foreach (DataRow R in reporteD.SelectData().Rows)
            {
                Reporte reporte = new Reporte
                {
                    Id = int.Parse(R["id"].ToString()),
                    Observaciones = R["observaciones"].ToString(),
                    Usuario_id = int.Parse(R["usuario_id"].ToString()),
                    Usuario = R["usuario"].ToString(),
                    Fecha = Convert.ToDateTime(R["fecha"].ToString()).ToString("dddd dd MMMM 'de' yyyy hh:mm", CultureInfo.CreateSpecificCulture("es-ES")),
                };

                listaReportes.Add(reporte);
            }

            return listaReportes;
        }

        public void CrearReporte()
        {
            reporteD.InsertData(Observaciones, Usuario_id);
        }
    }
}
