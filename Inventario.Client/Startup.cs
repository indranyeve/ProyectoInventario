using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Inventario.Client.Startup))]
namespace Inventario.Client
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
