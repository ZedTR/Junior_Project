using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Wcf.AES.Jobs.Client.Startup))]
namespace Wcf.AES.Jobs.Client
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
