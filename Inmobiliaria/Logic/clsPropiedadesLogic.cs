using API.Model;
using Dapper;
using Inmobiliaria.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Inmobiliaria.Logic
{
    public class PropiedadesLogic
    {


        string stringConnection = configuraciones.connection;

        internal ResultSet<Propiedades> crearPropiedad(Propiedades objRequest)
        {

            ResultSet<Propiedades> objResult = new ResultSet<Propiedades>();
            DynamicParameters parameters = new();
            List<Imagenes> lstImagenes = new List<Imagenes>(); 


            parameters.Add("@opcion ", 1);
            parameters.Add("@descripcion", objRequest.descripcion);
            parameters.Add("@precio", objRequest.precio);

            try
            {

                string path = configuraciones.rutas;


                parameters.Add("@tags", Newtonsoft.Json.JsonConvert.SerializeObject(objRequest.lstTag));

                using (var cnn = new SqlConnection(stringConnection))
                {
                    cnn.Open();

                    var transaction = cnn.BeginTransaction();
                    try
                    {

                        var multi = cnn.QueryMultiple("sp_propiedades", parameters, transaction, commandType: CommandType.StoredProcedure);

                        int id = multi.Read<int>().First();

                        //Insertar los documentos
                        foreach (var i in objRequest.lstImagenes)
                        {
                            Imagenes objImagen = new Imagenes();

                            
                            objImagen.ruta = path + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + id + "/";

                            Funciones.GuardarImagen(objImagen.ruta,i.nombre , Convert.FromBase64String(i.imagenString));
                            lstImagenes.Add(objImagen);

                        }
                        parameters = new DynamicParameters();

                        parameters.Add("@opcion", 1);//Insertar
                        parameters.Add("@imagenes", Newtonsoft.Json.JsonConvert.SerializeObject(lstImagenes));
                        parameters.Add("@idPropiedad", id);

                        cnn.Execute("sp_imagenes", parameters, transaction, commandType: CommandType.StoredProcedure);

                        objResult.CodigoEstatus = (int)HttpStatusCode.Created;
                        objResult.Notificaciones = "Registro creado exitosamente";

                        transaction.Commit();

                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception(ex.Message);
                    }
                }

            }catch(Exception ex)
            {


                objResult.CodigoEstatus = (int)HttpStatusCode.BadRequest;
                objResult.Notificaciones = "No se pudo crear la publicación";
                objResult.ErrorMessage = ex.Message;
            }


            return objResult;

        }

        internal ResultSet<Tag> obtenerTags()
        {
            ResultSet<Tag> objResult = new ResultSet<Tag>();
            objResult.Data = new List<Tag>();
            DynamicParameters parameters = new();

            parameters.Add("@opcion ", 5); //obtener todos los tags


            try
            {
                using (var cnn = new SqlConnection(stringConnection))
                {
                    var tags = cnn.Query<Tag>("sp_tags", parameters, commandType: CommandType.StoredProcedure).ToList();

                    if (tags.Count == 0) throw new Exception("No se encontraron registros");

                    objResult.Data = tags;
                    objResult.CodigoEstatus = (int)HttpStatusCode.OK;
                    objResult.Notificaciones = "Registro obtenidos con éxito";
                    
                }

            }
            catch (Exception ex)
            {
                objResult.CodigoEstatus = (int)HttpStatusCode.BadRequest;
                objResult.Notificaciones = "No se encontraron registros";
                objResult.ErrorMessage = ex.Message;
            }

            return objResult;

        }

        internal ResultSet<Propiedades> getPropiedadById(int id)
        {

            ResultSet<Propiedades> objResult = new ResultSet<Propiedades>();
            objResult.Data = new List<Propiedades>();
            DynamicParameters parameters = new();

            parameters.Add("@opcion ", 6); //obtener propidad por id
            parameters.Add("@id", id); 

            try
            {
                using (var cnn = new SqlConnection(stringConnection))
                {
                    var propiedad = cnn.QueryFirstOrDefault<Propiedades>("sp_propiedades", parameters, commandType: CommandType.StoredProcedure);

                    if (propiedad == null) throw new Exception("No se encontraron registros");

                    Propiedades objPropiedad = new Propiedades();
                    objPropiedad.descripcion = propiedad.descripcion;
                    objPropiedad.lstImagenes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Imagenes>>(propiedad.imagenesJson);
                    objPropiedad.lstTag = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Tag>>(propiedad.tagsJson);


                    List<Imagenes> lstImagenes = new List<Imagenes>();
                    foreach (var i in objPropiedad.lstImagenes)
                    {

                        Imagenes oImagenes = new();
                        var paths = i.ruta.Split("/");
                        oImagenes.imagenString = Convert.ToBase64String(File.ReadAllBytes(i.ruta));
                        oImagenes.nombre = paths[paths.Length - 1];

                        lstImagenes.Add(oImagenes);
                    }

                    objPropiedad.lstImagenes = lstImagenes;

                    objResult.ObjData = objPropiedad;
                    objResult.CodigoEstatus = (int)HttpStatusCode.OK;
                    objResult.Notificaciones = "Registros encontrados";

                }

            }
            catch (Exception ex)
            {
                objResult.CodigoEstatus = (int)HttpStatusCode.BadRequest;
                objResult.Notificaciones = "No se pudo crear la publicación";
                objResult.ErrorMessage = ex.Message;
            }

            return objResult;

        }

        internal ResultSet<Propiedades> listarPropiedades()
        {
            ResultSet<Propiedades> objResult = new ResultSet<Propiedades>();
            objResult.Data = new List<Propiedades>();
            DynamicParameters parameters = new();

            parameters.Add("@opcion ", 5); //obtener todas las propiedades

            try
            {
                using (var cnn = new SqlConnection(stringConnection))
                {
                    var result = cnn.Query<Propiedades>("sp_propiedades", parameters, commandType: CommandType.StoredProcedure).ToList();

                    if (result.Count == 0) throw new Exception("No se encontraron registros");

                    foreach(var propiedad in result)
                    {
                        Propiedades objPropiedad = new Propiedades();
                        objPropiedad.descripcion = propiedad.descripcion;
                        objPropiedad.lstImagenes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Imagenes>>(propiedad.imagenesJson);

                        List<Imagenes> lstImagenes = new List<Imagenes>();
                        foreach(var i in objPropiedad.lstImagenes)
                        {

                            Imagenes oImagenes = new();
                            var paths = i.ruta.Split("/");
                            oImagenes.imagenString = Convert.ToBase64String(File.ReadAllBytes(i.ruta));
                            oImagenes.nombre = paths[paths.Length - 1];

                            lstImagenes.Add(oImagenes);
                        }
                        objPropiedad.lstImagenes = lstImagenes;


                        objResult.Data.Add(objPropiedad);
                    }

                    objResult.CodigoEstatus = (int)HttpStatusCode.OK;
                    objResult.Notificaciones = "Registros encontrados";
                }

            }
            catch (Exception ex)
            {
                objResult.CodigoEstatus = (int)HttpStatusCode.BadRequest;
                objResult.Notificaciones = "No se pudo crear la publicación";
                objResult.ErrorMessage = ex.Message;
            }


            return objResult;

        }
    }
}
