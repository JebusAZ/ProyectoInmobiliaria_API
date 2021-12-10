using API.Model;
using Inmobiliaria.Logic;
using Inmobiliaria.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Controllers
{
    [EnableCors("MyAllowSpecificOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class PropiedadesController : ControllerBase
    {
        [HttpPost]
        [Route("agregar")]
       public IActionResult agregarPropiedad(Propiedades objRequest)
       {
            PropiedadesLogic objUsuarioLogic = new PropiedadesLogic();
            ResultSet<Propiedades> objPropiedad = new ResultSet<Propiedades>();

            objPropiedad = objUsuarioLogic.crearPropiedad(objRequest);

            return StatusCode(objPropiedad.CodigoEstatus, objPropiedad);
       }

        [HttpGet]
        public IActionResult listarPropiedades()
        {
            PropiedadesLogic objUsuarioLogic = new PropiedadesLogic();
            ResultSet<Propiedades> objPropiedad = new ResultSet<Propiedades>();

            objPropiedad = objUsuarioLogic.listarPropiedades();

            return StatusCode(objPropiedad.CodigoEstatus, objPropiedad);
        }

        [HttpGet]
        [Route("{id}")]    

        public IActionResult getPropiedadById(int id)
        {
            PropiedadesLogic objUsuarioLogic = new PropiedadesLogic();
            ResultSet<Propiedades> objPropiedad = new ResultSet<Propiedades>();

            objPropiedad = objUsuarioLogic.getPropiedadById(id);

            return StatusCode(objPropiedad.CodigoEstatus, objPropiedad);
        }


    }

}
