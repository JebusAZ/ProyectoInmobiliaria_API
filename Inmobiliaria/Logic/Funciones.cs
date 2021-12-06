using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inmobiliaria.Logic
{
    public static class Funciones
    {

        public static void GuardarImagen(string ruta, byte[] archivo)
        {
            
            // crea archivo o sobreescribe si existe.
            using (FileStream fs = File.Create(ruta))
            {
                // Agrega la información al archivo
                fs.Write(archivo, 0, archivo.Length);
            }

        }

    }
}
