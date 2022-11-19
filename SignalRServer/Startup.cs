using Microsoft.Owin.Cors;
using Microsoft.Owin.Mapping;
using Owin;
using SignalRServer.APIs;

namespace SignalRServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.Map("/severalive", subApp => subApp.Use<ApiServerAlive>());

            app.MapSignalR();
        }
    }
}
