using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Brilliance.Startup))]
namespace Brilliance
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}