using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MasspackWebApi.Startup))]
namespace MasspackWebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
