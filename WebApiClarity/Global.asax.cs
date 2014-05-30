using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.Routing;
using ClarityWebApi.App_Start;
using WebApiClarity.App_Start;

namespace ClarityWebApi {

    public class WebApiApplication : HttpApplication {
        protected void Application_Start() {


            //GlobalConfiguration.Configuration.MessageHandlers.Add(new TokenAuthHandler());


            //register cors handler
            //GlobalConfiguration.Configuration.MessageHandlers.Add(new CorsHandler());


            var cors = new EnableCorsAttribute("*", "*", "*");
            GlobalConfiguration.Configuration.EnableCors(cors);

           


            AreaRegistration.RegisterAllAreas();
            // RegisterGlobalFilters(GlobalFilters.Filters);
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}