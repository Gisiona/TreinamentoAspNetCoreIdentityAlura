using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TreinamentoAspNetCoreIdentityAlura.App_Start.Identity
{
    public class ValidadorSenha : IIdentityValidator<string>
    {
        public  int TamanhoSenhaRequerido { get; set; }
        public  bool ObrigatorioCaracteresEspecias { get; set; }
        

        public async Task<IdentityResult> ValidateAsync(string item)
        {
            var erros = new List<string>();

            if(ObrigatorioCaracteresEspecias && !VerificaCaracteresEspeciais(item))
            {
                erros.Add($"A senha precisa conter caracteres especiais.");
            }

            if(!VerificaTamanhoSenhaRequerido(item))
            {
                erros.Add($"A senha precisa conter no mínimo {TamanhoSenhaRequerido} caracteres.");
            }

            // se a variavel erro existir algum registro retorna um array de erros
            if(erros.Any())
            {
                return IdentityResult.Failed(erros.ToArray());
            }
            else
            {
                return IdentityResult.Success;
            }
        }


        private bool VerificaTamanhoSenhaRequerido(string senha ) => senha?.Length >= TamanhoSenhaRequerido;

        private bool VerificaCaracteresEspeciais(string senha) => Regex.IsMatch(senha, @"[!#$%&'()*+,-./:;?@[\\\]_`{|}~]");

        

    }
}