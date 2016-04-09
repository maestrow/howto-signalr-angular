using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using SignalRAngular.Json;

namespace SignalRAngular
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalHost.DependencyResolver.Register(typeof(JsonSerializer), getSerializer);
        }

        private JsonSerializer getSerializer()
        {
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new SignalRContractResolver();
            return JsonSerializer.Create(settings);            
        }
    }
}
