using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PacienteAsistente.Startup))]
namespace PacienteAsistente
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
