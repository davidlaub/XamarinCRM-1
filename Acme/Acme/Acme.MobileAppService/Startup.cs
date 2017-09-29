using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Acme.MobileAppService.Startup))]

namespace Acme.MobileAppService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}