using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProgrammingPractice.UI.MVC.Startup))]
namespace ProgrammingPractice.UI.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
