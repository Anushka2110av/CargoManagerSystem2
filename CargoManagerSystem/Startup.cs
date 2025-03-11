using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CargoManagerSystem.Startup))]
namespace CargoManagerSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
