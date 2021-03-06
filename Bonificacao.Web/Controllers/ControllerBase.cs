﻿using Bonificacao.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bonificacao.Web.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected readonly BonificacaoContext Context = new BonificacaoContext();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.TipoUsuario = GetTipoUsuario();
            base.OnActionExecuting(filterContext);
        }

        protected Pessoa GetUsuario()
        {
            var usuario = Context.Pessoas.FirstOrDefault(e => e.Usuario == User.Identity.Name);
            return usuario;
        }

        protected TipoPessoa GetTipoUsuario()
        {
            var usuario = GetUsuario();
            return usuario != null ? usuario.Tipo : TipoPessoa.Cliente;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}