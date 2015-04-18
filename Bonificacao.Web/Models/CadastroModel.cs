using Bonificacao.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bonificacao.Web.Models
{
    public class CadastroModel : ViewModelBase
    {
        public TipoPessoa? Tipo { get; set; }

        public int? EstabelecimentoId { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [MinLength(6, ErrorMessage = "O campo {0} deve ter pelo menos {1} caracteres")]
        public string Senha { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmação de senha")]
        [Compare("Senha", ErrorMessage = "As senhas não conferem")]
        public string ConfirmacaoSenha { get; set; }
    }
}