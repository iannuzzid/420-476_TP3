using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_420_476_Devoir3_Iannuzzi_David.Startup))]
namespace _420_476_Devoir3_Iannuzzi_David
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
