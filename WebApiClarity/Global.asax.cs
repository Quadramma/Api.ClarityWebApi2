using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using ClarityWebApi.App_Start;

namespace ClarityWebApi
{
 
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {

            //register cors handler
            GlobalConfiguration.Configuration.MessageHandlers.Add(new CorsHandler());

            AreaRegistration.RegisterAllAreas();
           // RegisterGlobalFilters(GlobalFilters.Filters);
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}