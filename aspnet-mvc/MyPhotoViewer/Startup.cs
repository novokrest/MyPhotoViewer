using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyPhotoViewer.Startup))]
namespace MyPhotoViewer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
