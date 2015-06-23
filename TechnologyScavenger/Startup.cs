using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TechnologyScavenger.Startup))]
namespace TechnologyScavenger
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
