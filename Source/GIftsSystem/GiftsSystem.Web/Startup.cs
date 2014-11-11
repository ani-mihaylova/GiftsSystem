using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GiftsSystem.Web.Startup))]
namespace GiftsSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
