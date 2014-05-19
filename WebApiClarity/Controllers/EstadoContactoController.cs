using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ClarityDB.T4;

namespace WebApiClarity.Controllers
{
    public class EstadoContactoController : ApiController
    {


        // GET: 
        [System.Web.Http.HttpGet]
        [System.Web.Http.AcceptVerbs("GET")]
        public object GET(int pageNumber, int itemsPerPage) {
            var db = new PetaPoco.Database("jlapc");
            var sql = PetaPoco.Sql.Builder
                .Append("SELECT EC.*")
                .Append("FROM EstadoContacto EC");
            var items = db.Page<dynamic>(pageNumber, itemsPerPage, sql);
            JsonResult rta = new JsonResult() { Data = new { data = items }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return rta.Data;
        }

        // GET: 
        [System.Web.Http.HttpGet]
        [System.Web.Http.AcceptVerbs("GET")]
        public object get(int id) {
            var db = new PetaPoco.Database("jlapc");
            var sql = PetaPoco.Sql.Builder
                .Append("SELECT EC.*")
                .Append("FROM EstadoContacto EC")
                .Append("WHERE EC.EstadoContactoID=@0", id);
            var items = db.Query<EstadoContacto>(sql);
            return (new JsonResult() { Data = items, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;
        }

        // PUT:  UPDATE
        [System.Web.Http.HttpPut]
        [System.Web.Http.AcceptVerbs("PUT")]
        public object update([FromBody] EstadoContacto item) {
            try {
                var db = new PetaPoco.Database("jlapc");
                var sql = PetaPoco.Sql.Builder
                    .Append("UPDATE EstadoContacto")
                    .Append(" SET Descripcion= @0", item.Descripcion)
                    .Append("WHERE EstadoContactoID = @0", item.EstadoContactoID);
                db.Execute(sql);
                return (new JsonResult() { Data = new { ok = true, sql = sql }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;

            } catch (Exception e) {
                return (new JsonResult() { Data = new { ok = false, error = e.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;
            }
        }

        // POST:  CREATE
        [System.Web.Http.HttpPost]
        [System.Web.Http.AcceptVerbs("POST")]
        public object Post([FromBody] EstadoContacto item) {
            try {
                var db = new PetaPoco.Database("jlapc");
                var sql = PetaPoco.Sql.Builder
                    .Append(" INSERT INTO EstadoContacto (Descripcion)")
                    .Append(" VALUES(@0,@1,@2)", item.Descripcion);
                db.Execute(sql);
                return (new JsonResult() { Data = new { ok = true, error = "", sql = sql }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;

            } catch (Exception e) {
                return (new JsonResult() { Data = new { ok = false, error = e.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;
            }
        }

        // DELETE:  DELETE
        [System.Web.Http.HttpDelete]
        [System.Web.Http.AcceptVerbs("DELETE")]
        public object delete(int id) {
            try {
                var db = new PetaPoco.Database("jlapc");
                var sql = PetaPoco.Sql.Builder
                    .Append("DELETE FROM EstadoContacto")
                    .Append("WHERE EstadoContactoID= @0", id);
                db.Execute(sql);
                return (new JsonResult() { Data = new { ok = true, error = "", sql = sql }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;

            } catch (Exception e) {
                return (new JsonResult() { Data = new { ok = false, error = e.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;
            }
        }

    }
}
