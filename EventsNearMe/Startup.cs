using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EventsNearMe.Startup))]
namespace EventsNearMe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
