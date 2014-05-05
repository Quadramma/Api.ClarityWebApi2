using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClarityDB.T4;

namespace ClarityWebApi.Controllers
{
    public class ModuloController : Controller
    {
        //
        // GET: /Modulo/

        public JsonResult Index()
        {

            SYS_Modulo module = new SYS_Modulo() {
                Descripcion = "Modulo Ejemplo"
            };

            return Json(module, JsonRequestBehavior.AllowGet);
        }

    }
}
