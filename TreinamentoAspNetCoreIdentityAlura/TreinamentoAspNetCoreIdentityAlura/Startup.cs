using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;
using TreinamentoAspNetCoreIdentityAlura.App_Start.Identity;
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
                    var userManager =new UserManager<UsuarioAplicacao>(userStore);

                    //adicionado validacoes do usuario atraves do Owin
                    var userValidator = new UserValidator<UsuarioAplicacao>(userManager);

                    userValidator.RequireUniqueEmail = true; //nao permite cadastro com emails duplicados

                    //Adiciona ao user manager as validacoes do usuário
                    userManager.UserValidator = userValidator;

                    //Adiciona validacao da senha
                    userManager.PasswordValidator = new ValidadorSenha()
                    {
                        TamanhoSenhaRequerido = 6,
                        ObrigatorioCaracteresEspecias = true
                    };


                    return userManager;
                });
        }
    }
}