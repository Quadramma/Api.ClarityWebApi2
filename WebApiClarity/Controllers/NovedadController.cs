using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using ClarityDB.T4;

namespace WebApiClarity.Controllers
{

    public class NovedadController : ApiController
    {

        //CUSTOM GET: 
        [System.Web.Http.HttpGet]
        [System.Web.Http.AcceptVerbs("GET")]
        public object GETTOPONE() {
            var db = new PetaPoco.Database("jlapc");
            var sql = PetaPoco.Sql.Builder.Append("SELECT ").Append(" E.* ")
                .Append(" FROM Novedad E")
                .Append(" Order By E.Fecha DESC");
                ;
            var items = db.Page<dynamic>(1, 1, sql);
            JsonResult rta = new JsonResult() { Data = new { data = items }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return rta.Data;
        }


        // GET: 
        [System.Web.Http.HttpGet]
        [System.Web.Http.AcceptVerbs("GET")]
        public object GET(int pageNumber, int itemsPerPage, int ContactoID) {
            var db = new PetaPoco.Database("jlapc");
            var sql = PetaPoco.Sql.Builder
                .Append("SELECT ")
                .Append("E.*")
                .Append("FROM Novedad E")
                .Append("WHERE E.ContactoID = @0", ContactoID)
                .Append(" Order By E.Fecha DESC");
            var items = db.Page<dynamic>(pageNumber, itemsPerPage, sql);
            JsonResult rta = new JsonResult() { Data = new { data = items }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return rta.Data;
        }

        // GET: 
        [System.Web.Http.HttpGet]
        [System.Web.Http.AcceptVerbs("GET")]
        public object GET(int id) {
            var db = new PetaPoco.Database("jlapc");
            var sql = PetaPoco.Sql.Builder
                .Append("SELECT N.*")
                .Append("FROM Novedad N")
                .Append("WHERE N.NovedadID=@0", id);
            var items = db.Query<dynamic>(sql);
            return (new JsonResult() { Data = new { data = items }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;
        }

        // POST:  CREATE Y UPDATE
        [System.Web.Http.HttpPost]
        [System.Web.Http.AcceptVerbs("POST")]
        public object POST([FromBody] Novedad item) {
            try {
                var db = new PetaPoco.Database("jlapc");
                if (item.NovedadID == 0) {
                    db.Insert(item);
                } else {
                    db.Update(item);
                }
                return (new JsonResult() { Data = new { ok = true, error = "" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;
            } catch (Exception e) {
                return (new JsonResult() { Data = new { ok = false, error = e.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;
            }
        }
     
        // DELETE: 
        [System.Web.Http.HttpGet]
        [System.Web.Http.AcceptVerbs("DELETE")]
        public object DELETE(int id) {
            var db = new PetaPoco.Database("jlapc");
            db.Delete("Novedad", "NovedadID", null, id);
            JsonResult rta = new JsonResult() { Data = new { ok = true }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return rta.Data;
        }

    }
}
