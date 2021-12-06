using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class Propiedades
    {
        public int? id { get; set; }
        public string descripcion { get; set; }
        public int precio { get; set; }
        public List<Tag> lstTag { get; set; }
        public string tagsJson { get; set; }
        public List<Imagenes> lstImagenes { get; set; }
        public string imagenesJson { get; set; }

    }

    
    

}
