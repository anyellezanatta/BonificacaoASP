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
    public class EstabelecimentosController : ControllerBase
    {
        // GET: Estabelecimentos
        public ActionResult Index()
        {
            var estabelecimentos = Context.Estabelecimentos.Include(e => e.GrupoEstabelecimento);
            return View(estabelecimentos.ToList());
        }

        public ActionResult Cadastrar()
        {
            ViewBag.GrupoEstabelecimentoId = new SelectList(Context.GruposEstabelecimento, "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Estabelecimento estabelecimento)
        {
            if (ModelState.IsValid)
            {
                Context.Estabelecimentos.Add(estabelecimento);
                Context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GrupoEstabelecimentoId = new SelectList(Context.GruposEstabelecimento, "Id", "Nome", estabelecimento.GrupoEstabelecimentoId);
            return View(estabelecimento);
        }

        // GET: Estabelecimentos/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estabelecimento estabelecimento = Context.Estabelecimentos.Find(id);
            if (estabelecimento == null)
            {
                return HttpNotFound();
            }
            ViewBag.GrupoEstabelecimentoId = new SelectList(Context.GruposEstabelecimento, "Id", "Nome", estabelecimento.GrupoEstabelecimentoId);
            return View(estabelecimento);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Nome,GrupoEstabelecimentoId,DataCriacao,DataModificacao")] Estabelecimento estabelecimento)
        {
            if (ModelState.IsValid)
            {
                Context.Entry(estabelecimento).State = EntityState.Modified;
                Context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GrupoEstabelecimentoId = new SelectList(Context.GruposEstabelecimento, "Id", "Nome", estabelecimento.GrupoEstabelecimentoId);
            return View(estabelecimento);
        }

        // POST: Estabelecimentos/Delete/5
        [HttpPost]
        public ActionResult Deletar(int id)
        {
            try
            {
                Estabelecimento estabelecimento = Context.Estabelecimentos.Find(id);
                Context.Estabelecimentos.Remove(estabelecimento);
                Context.SaveChanges();

                return Content("Item removido com sucesso");
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}
