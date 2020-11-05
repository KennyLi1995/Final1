using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinalAssignment2.Startup))]
namespace FinalAssignment2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
