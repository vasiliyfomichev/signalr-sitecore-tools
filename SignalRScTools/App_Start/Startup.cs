using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SitecoreSignalR.Tools.Startup))]
namespace SitecoreSignalR.Tools
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
