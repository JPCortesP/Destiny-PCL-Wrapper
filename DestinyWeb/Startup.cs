using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DestinyWeb.Startup))]
namespace DestinyWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
