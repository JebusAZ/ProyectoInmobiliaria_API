﻿using API.Model;
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
    public class UsuariosController : ControllerBase
    {

        [Route("crearUsuario")]
        [HttpPost]
        public IActionResult crearUsuario(Usuario objRequest)
        {
            UsuarioLogic objUsuarioLogic = new UsuarioLogic();
            ResultSet<Usuario> objUsuario = new ResultSet<Usuario>();

            objUsuario = objUsuarioLogic.crearUsuario(objRequest);

            return StatusCode(objUsuario.CodigoEstatus, objUsuario);
        }

        [Route("buscarUsuarioByEmail")]
        [HttpPost]
        public IActionResult buscarUsuarioPorEmail(Usuario objRequest)
        {
            UsuarioLogic objUsuarioLogic = new UsuarioLogic();
            ResultSet<Usuario> objUsuario = new ResultSet<Usuario>();

            objUsuario = objUsuarioLogic.buscarUsuarioPorEmail(objRequest);

            return StatusCode(objUsuario.CodigoEstatus, objUsuario);
        }



    }
}
