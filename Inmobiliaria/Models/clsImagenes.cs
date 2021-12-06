using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class Imagenes
    {
        public int? id { get; set; }
        public string ruta { get; set; }
        public string imagenString { get; set; }
        public string nombre { get; set; }
        public byte[] imagenBinario { get; set; }

    }
}
