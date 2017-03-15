using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Withings.SampleSite.Startup))]
namespace Withings.SampleSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
