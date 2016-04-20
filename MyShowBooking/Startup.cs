using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyShowBooking.Startup))]
namespace MyShowBooking
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
