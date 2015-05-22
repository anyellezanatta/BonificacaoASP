using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bonificacao.Data;

namespace Bonificacao.Web.Controllers
{
    [Authorize]
    public class ConfiguracoesController : ControllerBase
    {
        public ActionResult Index()
        {
            Configuracao configuracao = Context.Configuracoes.FirstOrDefault();
            if (configuracao == null)
            {
                return HttpNotFound();
            }
            return View(configuracao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Configuracao configuracao)
        {
            if (ModelState.IsValid)
            {
                Context.Entry(configuracao).State = EntityState.Modified;
                Context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(configuracao);
        }
    }
}
