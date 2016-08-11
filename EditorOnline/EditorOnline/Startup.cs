using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EditorOnline.Startup))]
namespace EditorOnline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
