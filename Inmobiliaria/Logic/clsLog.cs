using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Logic
{
    public class Log
    {
        internal string usuario;
        internal string rol;

        public string accion { get; set; }
        public string metodo { get; set; }
        public DateTime fecha { get; set; } = DateTime.Now;


    }
}
