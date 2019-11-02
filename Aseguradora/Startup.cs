using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Aseguradora.Startup))]
namespace Aseguradora
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
