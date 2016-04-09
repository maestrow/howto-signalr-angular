using Microsoft.Owin;
using Owin;
using POC.WebBgTask;

[assembly: OwinStartup(typeof(Startup))]
namespace POC.WebBgTask
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.MapSignalR();

        }
    }
}
