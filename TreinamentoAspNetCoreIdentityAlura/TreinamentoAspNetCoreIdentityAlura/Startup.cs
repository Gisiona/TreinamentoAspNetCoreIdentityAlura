using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;
using TreinamentoAspNetCoreIdentityAlura.Models;

[assembly: OwinStartup(typeof(TreinamentoAspNetCoreIdentityAlura.Startup))]
namespace TreinamentoAspNetCoreIdentityAlura
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.CreatePerOwinContext<DbContext>(() =>
            new IdentityDbContext<UsuarioAplicacao>("TreinamentoAspNetCoreIdentityConnection"));

            builder.CreatePerOwinContext<IUserStore<UsuarioAplicacao>>(
                (opcoes, contextoOwin) =>
                {
                    var _context = contextoOwin.Get<DbContext>();
                    return new UserStore<UsuarioAplicacao>(_context);
                });

            builder.CreatePerOwinContext<UserManager<UsuarioAplicacao>>(
                (opcoes, contextoOwin) =>
                {
                    var userStore = contextoOwin.Get <IUserStore<UsuarioAplicacao>>();
                    return new UserManager<UsuarioAplicacao>(userStore);
                });
        }
    }
}