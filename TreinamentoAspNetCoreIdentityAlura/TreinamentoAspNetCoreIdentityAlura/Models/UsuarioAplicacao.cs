using Microsoft.AspNet.Identity.EntityFramework;

namespace TreinamentoAspNetCoreIdentityAlura.Models
{
    public class UsuarioAplicacao : IdentityUser
    {
        public string NomeCompleto { get; set; }

        public UsuarioAplicacao()
        {
        }
    }
}