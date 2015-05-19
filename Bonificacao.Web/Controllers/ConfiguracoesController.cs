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
        private BonificacaoContext db = new BonificacaoContext();

    
        // GET: Configuracoes/Editar1-
        public ActionResult Editar()
        {
            Configuracao configuracao = db.Configuracoes.FirstOrDefault();
            if (configuracao == null)
            {
                return HttpNotFound();
            }
            return View(configuracao);
        }



        // POST: Configuracoes/Editar2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,BonusPorLitro,NivelBonificacao,DataCriacao,DataModificacao")] Configuracao configuracao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(configuracao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
                // Direciona pro index do controller home
            }
            return View(configuracao);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
