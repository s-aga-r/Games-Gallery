using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GamesGallery.PL.Web.Startup))]
namespace GamesGallery.PL.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
