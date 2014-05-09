using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ClarityDB.T4;

namespace ClarityWebApi.Controllers
{
    public class ModuloController : ApiController
    {
        //
        // GET: /Modulo/
       [System.Web.Http.HttpGet]
       [System.Web.Http.AcceptVerbs("GET")]
        public JsonResult list()
        {
            SYS_Modulo module = new SYS_Modulo() {
                Descripcion = "Modulo Ejemplo"
            };
            return new JsonResult() { Data = module, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}
