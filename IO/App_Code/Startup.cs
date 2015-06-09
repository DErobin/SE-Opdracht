using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IO.Startup))]
namespace IO
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
