using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SWApp.Startup))]
namespace SWApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);            
        }
    }
}
