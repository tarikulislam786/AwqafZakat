using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AwqafZakat.Startup))]
namespace AwqafZakat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
