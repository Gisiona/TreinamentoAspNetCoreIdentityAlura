using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;
using TreinamentoAspNetCoreIdentityAlura.Models;
using TreinamentoAspNetCoreIdentityAlura.ViewModels;

namespace TreinamentoAspNetCoreIdentityAlura.Controllers
{
    public class ContaController : Controller
    {
        // GET: Conta
        public ActionResult Registrar()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Registrar(ContaViewModel conta)
        {
            if(ModelState.IsValid)
            {

                //adiciona o usuario no banco
                var _context = new IdentityDbContext<UsuarioAplicacao>("TreinamentoAspNetCoreIdentityConnection");

                var userStore = new UserStore<UsuarioAplicacao>(_context);

                var userManager = new UserManager<UsuarioAplicacao>(userStore);
            

                var novoUsuario = new UsuarioAplicacao();

                novoUsuario.Email = conta.Email;
                novoUsuario.UserName = conta.NomeUsuario;
                novoUsuario.NomeCompleto = conta.NomeCompleto;

                _context.Users.Add(novoUsuario);
                _context.SaveChanges();





                // redireciona para pagina inicial
                return RedirectToAction("index", "home");
            }
            return View();
        }
    }
}