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
    public class ProdutosController : ControllerBase
    {
        // GET: Produtos
        public ActionResult Index()
        {
            return View(Context.Produtos.ToList());
        }

        // GET: Produtos/Cadastrar
        public ActionResult Cadastrar()
        {
            return View();
        }

        // POST: Produtos/Cadastrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                Context.Produtos.Add(produto);
                Context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(produto);
        }

        // GET: Produtos/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = Context.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                Context.Entry(produto).State = EntityState.Modified;
                Context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost]
        public ActionResult Deletar(int id)
        {
            try
            {
                Produto produto = Context.Produtos.Find(id);
                Context.Produtos.Remove(produto);
                Context.SaveChanges();

                return Content("Item removido com sucesso");
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        [OverrideAuthorization]
        [AllowAnonymous]
        public ViewResult Busca()
        {
            return View();
        }
    }
}
