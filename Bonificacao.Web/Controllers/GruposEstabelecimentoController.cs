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
    public class GruposEstabelecimentoController : Controller
    {
        private BonificacaoContext db = new BonificacaoContext();

        // GET: GruposEstabelecimento
        public ActionResult Index()
        {
            return View(db.GruposEstabelecimento.ToList());
        }

        // GET: GruposEstabelecimento/Cadastrar
        public ActionResult Cadastrar()
        {
            return View();
        }

        // POST: GruposEstabelecimento/Cadastrar        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar([Bind(Include = "Id,Nome,DataCriacao,DataModificacao")] GrupoEstabelecimento grupoEstabelecimento)
        {
            if (ModelState.IsValid)
            {
                db.GruposEstabelecimento.Add(grupoEstabelecimento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(grupoEstabelecimento);
        }

        // GET: GruposEstabelecimento/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrupoEstabelecimento grupoEstabelecimento = db.GruposEstabelecimento.Find(id);
            if (grupoEstabelecimento == null)
            {
                return HttpNotFound();
            }
            return View(grupoEstabelecimento);
        }

        // POST: GruposEstabelecimento/Editar/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Nome,DataCriacao,DataModificacao")] GrupoEstabelecimento grupoEstabelecimento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grupoEstabelecimento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grupoEstabelecimento);
        }

        // GET: GruposEstabelecimento/Deletar/5
        public ActionResult Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrupoEstabelecimento grupoEstabelecimento = db.GruposEstabelecimento.Find(id);
            if (grupoEstabelecimento == null)
            {
                return HttpNotFound();
            }
            return View(grupoEstabelecimento);
        }

        // POST: GruposEstabelecimento/Deletar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deletar(int id)
        {
            GrupoEstabelecimento grupoEstabelecimento = db.GruposEstabelecimento.Find(id);
            db.GruposEstabelecimento.Remove(grupoEstabelecimento);
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
