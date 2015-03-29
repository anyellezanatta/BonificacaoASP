
using System.ComponentModel.DataAnnotations;
namespace Bonificacao.Web.Models
{
    public class LoginModel
    {
        [Required]
        public string Usuario { get; set; }
        [Required]
        public string Senha { get; set; }
        public bool Lembrar { get; set; }
    }
}