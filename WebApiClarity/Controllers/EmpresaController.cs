using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ClarityDB.T4;

namespace WebApiClarity.Controllers {
    public class EmpresaController : ApiController {
        // GET: 
        [System.Web.Http.HttpGet]
        [System.Web.Http.AcceptVerbs("GET")]
        public object GET(int pageNumber, int itemsPerPage) {
            var db = new PetaPoco.Database("jlapc");
            var sql = PetaPoco.Sql.Builder
                .Append("SELECT ")
                .Append("E.*")
                .Append("FROM Empresa E");
            var items = db.Page<dynamic>(pageNumber, itemsPerPage, sql);
            JsonResult rta = new JsonResult() { Data = new { data = items }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return rta.Data;
        }
        // POST: 
        [System.Web.Http.HttpGet]
        [System.Web.Http.AcceptVerbs("POST")]
        public object POST([FromBody] Empresa item) {
            var db = new PetaPoco.Database("jlapc");
            db.Insert(item);
            JsonResult rta = new JsonResult() { Data = new { ok = true }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return rta.Data;
        }
        // PUT: 
        [System.Web.Http.HttpGet]
        [System.Web.Http.AcceptVerbs("PUT")]
        public object PUT([FromBody] Empresa item) {
            var db = new PetaPoco.Database("jlapc");
            db.Update(item);
            JsonResult rta = new JsonResult() { Data = new { ok = true }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return rta.Data;
        }
        // DELETE: 
        [System.Web.Http.HttpGet]
        [System.Web.Http.AcceptVerbs("DELETE")]
        public object PUT(int id) {
            var db = new PetaPoco.Database("jlapc");
            db.Delete("Empresa","EmpresaID",null,id);
            JsonResult rta = new JsonResult() { Data = new { ok = true }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return rta.Data;
        }

    }
}
