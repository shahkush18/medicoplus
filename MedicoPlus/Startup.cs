using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MedicoPlus.Startup))]
namespace MedicoPlus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
