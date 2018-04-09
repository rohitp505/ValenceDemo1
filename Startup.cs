using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ValenceDemo.Startup))]
namespace ValenceDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
