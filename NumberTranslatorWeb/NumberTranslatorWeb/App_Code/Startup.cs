using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NumberTranslatorWeb.Startup))]
namespace NumberTranslatorWeb
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
