using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ThemeParkRater.WebMVC.Startup))]
namespace ThemeParkRater.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
