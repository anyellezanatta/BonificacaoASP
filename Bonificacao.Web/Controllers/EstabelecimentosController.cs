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
    public class EstabelecimentosController : Controller
    {
        private BonificacaoContext db = new BonificacaoContext();

        // GET: Estabelecimentos
        public ActionResult Index()
        {
            var estabelecimentos = db.Estabelecimentos.Include(e => e.GrupoEstabelecimento);
            return View(estabelecimentos.ToList());
        }
        
        public ActionResult Cadastrar()
        {
            ViewBag.GrupoEstabelecimentoId = new SelectList(db.GruposEstabelecimento, "Id", "Nome");
            return View();
        }

        // POST: Estabelecimentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar([Bind(Include = "Id,Nome,GrupoEstabelecimentoId,DataCriacao,DataModificacao")] Estabelecimento estabelecimento)
        {
            if (ModelState.IsValid)
            {
                db.Estabelecimentos.Add(estabelecimento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GrupoEstabelecimentoId = new SelectList(db.GruposEstabelecimento, "Id", "Nome", estabelecimento.GrupoEstabelecimentoId);
            return View(estabelecimento);
        }

        // GET: Estabelecimentos/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estabelecimento estabelecimento = db.Estabelecimentos.Find(id);
            if (estabelecimento == null)
            {
                return HttpNotFound();
            }
            ViewBag.GrupoEstabelecimentoId = new SelectList(db.GruposEstabelecimento, "Id", "Nome", estabelecimento.GrupoEstabelecimentoId);
            return View(estabelecimento);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Nome,GrupoEstabelecimentoId,DataCriacao,DataModificacao")] Estabelecimento estabelecimento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estabelecimento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GrupoEstabelecimentoId = new SelectList(db.GruposEstabelecimento, "Id", "Nome", estabelecimento.GrupoEstabelecimentoId);
            return View(estabelecimento);
        }

        // GET: Estabelecimentos/Delete/5
        public ActionResult Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estabelecimento estabelecimento = db.Estabelecimentos.Find(id);
            if (estabelecimento == null)
            {
                return HttpNotFound();
            }
            return View(estabelecimento);
        }

        // POST: Estabelecimentos/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estabelecimento estabelecimento = db.Estabelecimentos.Find(id);
            db.Estabelecimentos.Remove(estabelecimento);
            db.SaveChanges();
            return RedirectToAction("Index");
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
