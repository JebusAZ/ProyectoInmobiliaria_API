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

        public static void GuardarImagen(string ruta, string nombre, byte[] archivo)
        {

            //Verificar si la ruta existe, si no, crearla
            if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);

            // crea archivo o sobreescribe si existe.
            using (FileStream fs = File.Create(ruta + nombre ))
            {
                // Agrega la información al archivo
                fs.Write(archivo, 0, archivo.Length);
            }

        }

        internal static void Log(Log log)
        {
            try
            {

                MongoLogic objMongoLogic = new MongoLogic();

                objMongoLogic.Insert(log);
            }
            catch
            {

            }

        }
    }
}
