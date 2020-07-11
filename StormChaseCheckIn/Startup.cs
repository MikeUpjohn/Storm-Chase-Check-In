using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StormChaseCheckIn.Startup))]
namespace StormChaseCheckIn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
