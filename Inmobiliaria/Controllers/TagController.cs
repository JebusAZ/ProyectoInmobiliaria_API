using API.Model;
using Inmobiliaria.Logic;
using Inmobiliaria.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace Inmobiliaria.Controllers
{

    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        [Route("obtenerTags")]
        [HttpGet]
        public IActionResult getTags()
        {
            PropiedadesLogic objTagLogic = new PropiedadesLogic();
            ResultSet<Tag> objTag = new ResultSet<Tag>();

            objTag = objTagLogic.obtenerTags();

            return StatusCode(objTag.CodigoEstatus, objTag);
        }

    }
}
