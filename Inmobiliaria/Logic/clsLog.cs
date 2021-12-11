using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Logic
{
    public class Log
    {
        public string usuario { get; set; }
        public string rol { get; set; }
        public object request { get; set; }
        public string accion { get; set; }
        public string metodo { get; set; }
        public DateTime fecha { get; set; } = DateTime.Now;


    }
}
