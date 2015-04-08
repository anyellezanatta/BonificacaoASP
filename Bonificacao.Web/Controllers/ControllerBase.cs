using Bonificacao.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bonificacao.Web.Controllers
{
    public class ControllerBase : Controller
    {
        protected readonly BonificacaoContext Context = new BonificacaoContext();

        protected Pessoa GetUsuario()
        {
            var usuario = Context.Pessoas.FirstOrDefault(e => e.Usuario == User.Identity.Name);
            return usuario;
        }

        protected TipoPessoa GetTipoUsuario()
        {
            return GetUsuario().Tipo;
        }
    }
}