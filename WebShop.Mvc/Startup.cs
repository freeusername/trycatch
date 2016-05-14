using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebShop.Mvc.Startup))]
namespace WebShop.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
