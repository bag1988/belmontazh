using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(belmontazh.Startup))]
namespace belmontazh
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
