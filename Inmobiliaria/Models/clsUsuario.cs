using Inmobiliaria.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class Usuario
    {

        public int id { get; set; }
        public string nombre { get; set; }
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        public string email { get; set; }
        public string contraseña { get; set; }
        public bool isOauth { get; set; }

        //no pertenece al modelo
        public string rolesJson { get; set; }
        public List<Rol> roles { get; set; }


    }
}
