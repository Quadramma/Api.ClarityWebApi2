using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ClarityDB.T4;

namespace WebApiClarity.Controllers
{
    public class SubEstadoContactoController : ApiController
    {

        // GET: 
        [System.Web.Http.HttpGet]
        [System.Web.Http.AcceptVerbs("GET")]
        public object getAll() {
            var db = new PetaPoco.Database("jlapc");
            var sql = PetaPoco.Sql.Builder
                .Append("SELECT SEC.*")
                .Append("FROM SubEstadoContacto SEC");
            var items = db.Query<SubEstadoContacto>(sql);
            JsonResult rta = new JsonResult() { Data = items, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return rta.Data;
        }

        // GET: 
        [System.Web.Http.HttpGet]
        [System.Web.Http.AcceptVerbs("GET")]
        public object get(int id) {
            var db = new PetaPoco.Database("jlapc");
            var sql = PetaPoco.Sql.Builder
                .Append("SELECT SEC.*")
                .Append("FROM SubEstadoContacto SEC")
                .Append("WHERE EC.SubEstadoContactoID=@0", id);
            var items = db.Query<SubEstadoContacto>(sql);
            return (new JsonResult() { Data = items, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;
        }

        // PUT:  UPDATE
        [System.Web.Http.HttpPut]
        [System.Web.Http.AcceptVerbs("PUT")]
        public object update([FromBody] SubEstadoContacto item) {
            try {
                var db = new PetaPoco.Database("jlapc");
                var sql = PetaPoco.Sql.Builder
                    .Append("UPDATE SubEstadoContacto")
                    .Append(" SET EstadoContactoID= @0", item.EstadoContactoID)
                    .Append(" SET Descripcion= @0", item.Descripcion)
                    .Append(" SET TipoCuentaID= @0", item.TipoCuentaID)
                    .Append(" SET GrupoID= @0", item.GrupoID)
                    .Append("WHERE SubEstadoContactoID = @0", item.EstadoContactoID);
                db.Execute(sql);
                return (new JsonResult() { Data = new { ok = true, items = getAll(), sql = sql }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;

            } catch (Exception e) {
                return (new JsonResult() { Data = new { ok = false, items = new SubEstadoContacto[] { }, error = e.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;
            }
        }

        // POST:  CREATE
        [System.Web.Http.HttpPost]
        [System.Web.Http.AcceptVerbs("POST")]
        public object Post([FromBody] SubEstadoContacto item) {
            try {
                var db = new PetaPoco.Database("jlapc");
                var sql = PetaPoco.Sql.Builder
                    .Append(" INSERT INTO SubEstadoContacto (EstadoContactoID,Descripcion,TipoCuentaID,GrupoID)")
                    .Append(" VALUES(@0,@1,@2)",item.EstadoContactoID, item.Descripcion, item.TipoCuentaID, item.GrupoID);
                db.Execute(sql);
                return (new JsonResult() { Data = new { ok = true, items = getAll(), error = "", sql = sql }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;

            } catch (Exception e) {
                return (new JsonResult() { Data = new { ok = false, items = new EstadoContacto[] { }, error = e.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;
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
                return (new JsonResult() { Data = new { ok = true, items = getAll(), error = "", sql = sql }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;

            } catch (Exception e) {
                return (new JsonResult() { Data = new { ok = false, items = new EstadoContacto[] { }, error = e.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;
            }
        }
    }
}
