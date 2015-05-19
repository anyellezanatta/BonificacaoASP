using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Bonificacao.Data;

namespace Bonificacao.Web.Controllers.Apis
{
    [AllowAnonymous]
    public class ProdutoController : ApiController
    {
        private BonificacaoContext db = new BonificacaoContext();

        // GET: api/Produto?busca=gasolina
        public IQueryable GetProduto(string busca)
        {
            IQueryable<Produto> produtos = db.Produtos;
            if (!string.IsNullOrEmpty(busca))
                produtos = produtos.Where(p => p.Nome.Contains(busca));

            return produtos.Select(p => new { p.Nome, p.Preco });
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