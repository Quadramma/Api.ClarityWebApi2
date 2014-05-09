using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ClarityDB.T4;

namespace WebApiClarity.Controllers
{
    public class ContactoController : ApiController
    {

        private object getAll(PetaPoco.Database db) {
            var sql = PetaPoco.Sql.Builder
                .Append("SELECT")
                .Append("Emp.RazonSocial") //,T.Descripcion as TemaDescripcion
                //.Append(",GU.Descripcion as GrupoUsuarioDescripcion, GU.GrupoID, G.Descripcion as GrupoDescripcion")
                .Append("FROM Contacto C")
                .Append("LEFT JOIN Tema T on T.TemaID = C.TemaID")
                .Append("LEFT JOIN GrupoUsuario GU on GU.GrupoUsuarioID = C.GrupoUsuarioID")
                .Append("LEFT JOIN Grupo G on G.GrupoID = GU.GrupoID")
                .Append("LEFT JOIN Empresa Emp on Emp.EmpresaID = C.EmpresaID");
            var items = db.Query<Contacto>(sql);
            return items;
        }


        // GET: 
        [System.Web.Http.HttpGet]
        [System.Web.Http.AcceptVerbs("GET")]
        public object GET() {
            var db = new PetaPoco.Database("jlapc");
            var items = getAll(db);
            JsonResult rta = new JsonResult() { Data = items, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return rta.Data;
        }

        // GET: 
        [System.Web.Http.HttpGet]
        [System.Web.Http.AcceptVerbs("GET")]
        public object GET(int id) {
            var db = new PetaPoco.Database("jlapc");
            var sql = PetaPoco.Sql.Builder
                .Append("SELECT T.*,G.Descripcion as GrupoDescripcion")
                .Append("FROM Tema T")
                .Append("INNER JOIN Grupo G on G.GrupoID = T.GrupoID")
                .Append("WHERE T.TemaID=@0", id);
            var items = db.Query<Tema>(sql);
            return (new JsonResult() { Data = items, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;
        }

        // PUT:  UPDATE
        [System.Web.Http.HttpPut]
        [System.Web.Http.AcceptVerbs("PUT")]
        public object PUT([FromBody] Tema tema) {
            try {
                var db = new PetaPoco.Database("jlapc");
                var sql = PetaPoco.Sql.Builder
                    .Append("UPDATE Tema")
                    .Append(" SET Descripcion= @0", tema.Descripcion)
                    .Append(" , EsFijo= @0", tema.EsFijo)
                    .Append(" , GrupoID= @0", tema.GrupoID)
                    .Append("WHERE TemaID = @0", tema.TemaID);
                db.Execute(sql);
                return (new JsonResult() { Data = new { ok = true, items = getAll(db), sql = sql }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;

            } catch (Exception e) {
                return (new JsonResult() { Data = new { ok = false, items = new Tema[] { }, error = e.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;
            }
        }

        // POST:  CREATE
        [System.Web.Http.HttpPost]
        [System.Web.Http.AcceptVerbs("POST")]
        public object POST([FromBody] Tema tema) {
            try {
                var db = new PetaPoco.Database("jlapc");
                var sql = PetaPoco.Sql.Builder
                    .Append(" INSERT INTO Tema (Descripcion,EsFijo,GrupoID)")
                    .Append(" VALUES(@0,@1,@2)", tema.Descripcion, tema.EsFijo, tema.GrupoID);
                db.Execute(sql);
                return (new JsonResult() { Data = new { ok = true, items = getAll(db), error = "", sql = sql }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;

            } catch (Exception e) {
                return (new JsonResult() { Data = new { ok = false, items = new Tema[] { }, error = e.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;
            }
        }

        // DELETE:  DELETE
        [System.Web.Http.HttpDelete]
        [System.Web.Http.AcceptVerbs("DELETE")]
        public object DELETE(int id) {
            try {
                var db = new PetaPoco.Database("jlapc");
                var sql = PetaPoco.Sql.Builder
                    .Append("DELETE FROM Tema")
                    .Append("WHERE TemaID= @0", id);
                db.Execute(sql);
                return (new JsonResult() { Data = new { ok = true, items = getAll(db), error = "", sql = sql }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;

            } catch (Exception e) {
                return (new JsonResult() { Data = new { ok = false, items = new Tema[] { }, error = e.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;
            }
        }
    }
}
