using API.Model;
using Dapper;
using Inmobiliaria.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Inmobiliaria.Logic
{
    public class UsuarioLogic
    {

        string connection = @"Data Source=localhost;Initial Catalog=Inmobiliaria; User ID=sa;Password=admin";

        internal ResultSet<Usuario> crearUsuario(Usuario objRequest)
        {

            ResultSet<Usuario> objResult = new ResultSet<Usuario>();
            DynamicParameters parameters = new();

            parameters.Add("@opcion ", 1);
            parameters.Add("@id ", objRequest.id);
            parameters.Add("@nombre ", objRequest.nombre);
            parameters.Add("@primerApellido ", objRequest.primerApellido);
            parameters.Add("@segundoApellido ", objRequest.segundoApellido);
            parameters.Add("@email ", objRequest.email);
            parameters.Add("@contraseña ", objRequest.contraseña);
            parameters.Add("@isOauth ", objRequest.isOauth);


            using (var cnn = new SqlConnection(connection))
            {
                try
                {
                    var rowsAffected = cnn.Execute("sp_usuarios", parameters, commandType: CommandType.StoredProcedure);

                    if (rowsAffected == 0) throw new Exception("Error al insertar el usuario");

                    objResult.CodigoEstatus = (int)HttpStatusCode.OK;
                    objResult.Notificaciones = "Se ha creado el usuario con éxito";

                }
                catch (Exception ex)
                {
                    objResult.CodigoEstatus = (int)HttpStatusCode.BadRequest;
                    objResult.Notificaciones = ex.Message;
                }

            }
            return objResult;
        }

        internal ResultSet<Usuario> buscarUsuarioPorEmail(Usuario objRequest)
        {

            ResultSet<Usuario> objResult = new ResultSet<Usuario>();
            DynamicParameters parameters = new();

            parameters.Add("@opcion ", 6);
            parameters.Add("@email ", objRequest.email);
            

            using (var cnn = new SqlConnection(connection))
            {
                try
                {
                    var result = cnn.QueryFirstOrDefault<Usuario>("sp_usuarios", parameters, commandType: CommandType.StoredProcedure);

                    if (result is null) throw new Exception("Error al obtener el usuario");

                    result.roles = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rol>>(result.rolesJson);
                    result.rolesJson = null;

                    objResult.ObjData = result;
                    objResult.CodigoEstatus = (int)HttpStatusCode.OK;
                    objResult.Notificaciones = "Se ha recuperado por éxito";

                }
                catch (Exception ex)
                {
                    objResult.CodigoEstatus = (int)HttpStatusCode.BadRequest;
                    objResult.Notificaciones = ex.Message;
                }

            }
            return objResult;

        }
    }
}
