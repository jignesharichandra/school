using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(school18.Startup))]
namespace school18
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
