using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAREA_4_DESARROLLO_DE_SISTEMAS.Model
{
    class Zapato
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string modelo { get; set; }
        public string talla { get; set; }
        public string color { get; set; }
        public int stock { get; set; }
        public decimal precio { get; set; }
        public string img { get; set; }
        public string usuario_id { get; set; }
        public string fecha { get; set; }
        public bool estado { get; set; }

    }
}
