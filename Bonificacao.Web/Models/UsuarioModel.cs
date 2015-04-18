using Bonificacao.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bonificacao.Web.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Tipo { get; set; }
        public string Estabelecimento { get; set; }
    }
}