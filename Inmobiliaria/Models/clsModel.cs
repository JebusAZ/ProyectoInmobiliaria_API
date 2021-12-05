using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model
{
    public class Paged<Model>
    {
        public int? TotalRows { get; set; }
        public int? TotalPages { get; set; }
        public List<Model> ListResult { get; set; }
    }

    public class ResultSet<Model>
    {
        public int? id { get; set; }
        public int? RowsAffected { get; set; }
        public string Estatus { get; set; } // OK | ERR
        public int CodigoEstatus { get; set; } // CODIGO DE RESPUESTA HTTP
        public string NombreArchivo { get; set; }
        public string Notificaciones { get; set; }
        public List<Model> Data { get; set; }
        public Model ObjData { get; set; }  ///-----ELIMINAR
        public Paged<Model> PagedData { get; set; }
        public string ErrorMessage { get; set; }
        public string token { get; set; }
    }

    public class Result<Model>
    {
        public ResultSet<Model> OK(List<Model> model, Paged<Model> paged = null)
        {
            ResultSet<Model> res = new ResultSet<Model>();
            Paged<Model> pag = new Paged<Model>();

            if (paged != null)
            {
                pag.TotalRows = paged.TotalRows;
                pag.TotalPages = paged.TotalPages;
                res.PagedData = pag;
            }

            res.Estatus = "OK";
            res.Notificaciones = "Registros Encontrados";
            res.Data = model;
            res.ErrorMessage = null;
            res.CodigoEstatus = 200;


            return res;
        }

        public ResultSet<Model> Created(bool status, List<Model> model = null) // bool status???
        {
            ResultSet<Model> res = new ResultSet<Model>();
            if (model != null)
            {
                res.Data = model;
            }

            res.Estatus = "OK";
            res.Notificaciones = "Registro guardado correctamente";
            res.ErrorMessage = null;
            res.CodigoEstatus = 200;

            return res;
        }
        public ResultSet<Model> Error(string msg)
        {
            ResultSet<Model> res = new ResultSet<Model>();
            res.Estatus = "FAILED";
            res.Notificaciones = msg;
            res.Data = null;
            res.PagedData = null;
            res.CodigoEstatus = 400;

            return res;
        }

        public ResultSet<Model> Disabled(bool status, List<Model> model = null) // bool status???
        {
            ResultSet<Model> res = new ResultSet<Model>();
            if (model != null)
            {
                res.Data = model;
            }

            res.Estatus = "OK";
            res.Notificaciones = "Registro desactivado correctamente";
            res.ErrorMessage = null;
            res.CodigoEstatus = 200;

            return res;
        }

        public ResultSet<Model> Updated(bool status, List<Model> model = null) // bool status???
        {
            ResultSet<Model> res = new ResultSet<Model>();
            if (model != null)
            {
                res.Data = model;
            }

            res.Estatus = "OK";
            res.Notificaciones = "Registro actualizado correctamente";
            res.ErrorMessage = null;
            res.CodigoEstatus = 200;

            return res;
        }

        public ResultSet<Model> Errortoken(string msg)
        {
            ResultSet<Model> res = new ResultSet<Model>();
            res.Estatus = "FAILED";
            res.Notificaciones = msg;
            ///res.Data = null;
            ///res.PagedData = null;
            res.CodigoEstatus = 400;

            return res;
        }

        public ResultSet<Model> Delete()
        {
            ResultSet<Model> res = new ResultSet<Model>();
            res.Estatus = "OK";
            res.Notificaciones = "Registro eliminado correctamente";
            res.ErrorMessage = null;
            res.CodigoEstatus = 200;

            return res;
        }
    }
}
