using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace WebApiClarity.Controllers
{
    public class EmpresaController : ApiController
    {
        //
        // GET: /Empresa/

        public ActionResult Index()
        {
            return View();
        }

    }
}
