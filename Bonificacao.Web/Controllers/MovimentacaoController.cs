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
    public class MovimentacaoController : ControllerBase
    {
        public ActionResult Index()
        {
            var movimentos = Context.Movimentos.Include(m => m.Cliente).Include(m => m.Estabelecimento).Include(m => m.Produto).Include(m => m.Vendedor);
            return View(movimentos.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(Context.Pessoas.Where(e => e.Tipo == TipoPessoa.Cliente), "Id", "Nome");
            ViewBag.EstabelecimentoId = new SelectList(Context.Estabelecimentos, "Id", "Nome");
            ViewBag.ProdutoId = new SelectList(Context.Produtos, "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TipoMovimento,ClienteId,EstabelecimentoId,VendedorId,ProdutoId,ValorBonus,SaldoBonus,ValorPago,DataHoraMovimento,DataCriacao,DataModificacao")] Movimento movimento)
        {
            if (ModelState.IsValid)
            {
                var usuario = base.GetUsuario();

                movimento.VendedorId = usuario.Id;
                movimento.EstabelecimentoId = usuario.EstabelecimentoId.Value;
                movimento.TipoMovimento = TipoMovimento.Venda;

                Context.Movimentos.Add(movimento);
                Context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(Context.Pessoas, "Id", "Nome", movimento.ClienteId);
            ViewBag.EstabelecimentoId = new SelectList(Context.Estabelecimentos, "Id", "Nome", movimento.EstabelecimentoId);
            ViewBag.ProdutoId = new SelectList(Context.Produtos, "Id", "Nome", movimento.ProdutoId);
            return View(movimento);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimento movimento = Context.Movimentos.Find(id);
            if (movimento == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(Context.Pessoas, "Id", "Nome", movimento.ClienteId);
            ViewBag.EstabelecimentoId = new SelectList(Context.Estabelecimentos, "Id", "Nome", movimento.EstabelecimentoId);
            ViewBag.ProdutoId = new SelectList(Context.Produtos, "Id", "Nome", movimento.ProdutoId);
            ViewBag.VendedorId = new SelectList(Context.Pessoas, "Id", "Nome", movimento.VendedorId);
            return View(movimento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TipoMovimento,ClienteId,EstabelecimentoId,VendedorId,ProdutoId,ValorBonus,SaldoBonus,ValorPago,DataHoraMovimento,DataCriacao,DataModificacao")] Movimento movimento)
        {
            if (ModelState.IsValid)
            {
                Context.Entry(movimento).State = EntityState.Modified;
                Context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(Context.Pessoas, "Id", "Nome", movimento.ClienteId);
            ViewBag.EstabelecimentoId = new SelectList(Context.Estabelecimentos, "Id", "Nome", movimento.EstabelecimentoId);
            ViewBag.ProdutoId = new SelectList(Context.Produtos, "Id", "Nome", movimento.ProdutoId);
            ViewBag.VendedorId = new SelectList(Context.Pessoas, "Id", "Nome", movimento.VendedorId);
            return View(movimento);
        }
    }
}
