using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BitmUniversityApp.Startup))]
namespace BitmUniversityApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
