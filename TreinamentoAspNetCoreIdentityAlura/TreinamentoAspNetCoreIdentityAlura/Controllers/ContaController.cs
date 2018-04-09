using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TreinamentoAspNetCoreIdentityAlura.Models;
using TreinamentoAspNetCoreIdentityAlura.ViewModels;

namespace TreinamentoAspNetCoreIdentityAlura.Controllers
{
    public class ContaController : Controller
    {


        private UserManager<UsuarioAplicacao> _userManager;

        public UserManager<UsuarioAplicacao> UserManager
        {
            get
            {
                if(_userManager == null)
                {
                    var contextoOwin = HttpContext.GetOwinContext();
                    _userManager = contextoOwin.GetUserManager<UserManager<UsuarioAplicacao>>();
                }
                return _userManager;
            }
            set
            {
                _userManager = value;
            }
        }

        // GET: Conta
        public ActionResult Registrar()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> Registrar(ContaViewModel conta)
        {
            if(ModelState.IsValid)
            {
                //adiciona o usuario no banco
                //var _context = new IdentityDbContext<UsuarioAplicacao>("TreinamentoAspNetCoreIdentityConnection");
                //var userStore = new UserStore<UsuarioAplicacao>(_context);
                //var userManager = new UserManager<UsuarioAplicacao>(userStore);
            

                var novoUsuario = new UsuarioAplicacao();

                novoUsuario.Email = conta.Email;
                novoUsuario.UserName = conta.NomeUsuario;
                novoUsuario.NomeCompleto = conta.NomeCompleto;

                var usuarioBanco = UserManager.FindByEmail(conta.Email);

                if(usuarioBanco != null)
                {
                    return RedirectToAction("index", "home");
                }
                    

                var resultado = await UserManager.CreateAsync(novoUsuario, conta.Senha);
                if(resultado.Succeeded)
                {
                    // redireciona para pagina inicial
                    return RedirectToAction("index", "home");
                }

                //_context.Users.Add(novoUsuario);
                //_context.SaveChanges();

                
            }
            return View();
        }
    }
}