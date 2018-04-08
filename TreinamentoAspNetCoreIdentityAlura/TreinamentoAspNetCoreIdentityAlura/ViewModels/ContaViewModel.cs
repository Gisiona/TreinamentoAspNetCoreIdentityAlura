using System.ComponentModel.DataAnnotations;

namespace TreinamentoAspNetCoreIdentityAlura.ViewModels
{
    public class ContaViewModel
    {
        [Required]
        [Display(Name = "Nome do usuário")]
        public string NomeUsuario { get; set; }

        [Required]
        [Display(Name ="Nome completo do usuário")]
        public string NomeCompleto { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

    }
}