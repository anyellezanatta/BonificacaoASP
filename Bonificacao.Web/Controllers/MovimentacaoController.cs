using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bonificacao.Data;
using System.Globalization;
using Bonificacao.Web.Models;

namespace Bonificacao.Web.Controllers
{
    [Authorize]
    public class MovimentacaoController : ControllerBase
    {
        [ChildActionOnly]
        public ActionResult BuscaMovimentacao()
        {
            IQueryable<Pessoa> opcoesClientes = Context.Pessoas.Where(e => e.Tipo == TipoPessoa.Cliente);
            var usuario = base.GetUsuario();
            if (usuario.Tipo == TipoPessoa.Cliente)
                opcoesClientes = opcoesClientes.Where(e => e.Id == usuario.Id);
            var clientesList = opcoesClientes.OrderBy(e => e.Nome).ToList();

            return PartialView("BuscaMovimentacao", 
                new BuscaMovimentacaoViewModel() 
                { 
                    DataInicio = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy"),
                    DataFim = DateTime.Now.ToString("dd/MM/yyyy"),
                    OpcoesClientes = new SelectList(clientesList, "Id", "Nome")
                });
        }

        public ActionResult Lista(int[] clientes = null, string datainicio = null, string datafim = null)
        {
            var usuario = base.GetUsuario();
            if (usuario == null)
                return HttpNotFound();

            IQueryable<Movimento> movimentos = Context.Movimentos
                .Include(m => m.Cliente)
                .Include(m => m.Estabelecimento)
                .Include(m => m.Produto)
                .Include(m => m.Vendedor);

            if (clientes != null && clientes.Any())
                movimentos = movimentos.Where(m => clientes.Contains(m.ClienteId));

            if (!string.IsNullOrEmpty(datainicio) && !string.IsNullOrEmpty(datafim))
            {
                var dataInicial = DateTimeOffset.Parse(datainicio, new CultureInfo("pt-BR"));
                var dtFinal = DateTimeOffset.Parse(datafim, new CultureInfo("pt-BR"));
                var dataFinal = new DateTimeOffset(dtFinal.Year, dtFinal.Month, dtFinal.Day, 23, 59, 59, dtFinal.Offset);
                movimentos = movimentos.Where(m => m.DataCriacao >= dataInicial && m.DataCriacao <= dataFinal);
            }

            IQueryable<Pessoa> opcoesClientes = Context.Pessoas.Where(e => e.Tipo == TipoPessoa.Cliente);
            if (usuario.Tipo == TipoPessoa.Cliente)
            {
                movimentos = movimentos.Where(e => e.ClienteId == usuario.Id);
                opcoesClientes = opcoesClientes.Where(e => e.Id == usuario.Id);
            }

            var movimentacoes = movimentos.OrderByDescending(e => e.DataCriacao).ToList();

            return PartialView("Lista", movimentacoes);
        }

        public ActionResult Cadastrar()
        {
            ViewBag.ClienteId = new SelectList(Context.Pessoas.Where(e => e.Tipo == TipoPessoa.Cliente), "Id", "Nome");
            ViewBag.EstabelecimentoId = new SelectList(Context.Estabelecimentos, "Id", "Nome");
            ViewBag.ProdutoId = new SelectList(Context.Produtos, "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Movimento movimento)
        {
            if (ModelState.IsValid)
            {
                var usuario = base.GetUsuario();
                var produto = Context.Produtos.FirstOrDefault(p => p.Id == movimento.ProdutoId);
                if (usuario == null || produto == null)
                    return HttpNotFound();

                movimento.VendedorId = usuario.Id;
                movimento.EstabelecimentoId = usuario.EstabelecimentoId.Value;

                movimento.TipoMovimento = TipoMovimento.Venda;
                movimento.ValorBonus = 0;
                movimento.SaldoBonus = 0;

                decimal valorSaldoBonusCliente = 0;
                var ultimaMovimentacaoCliente = Context.Movimentos
                    .Where(e => e.ClienteId == movimento.ClienteId)
                    .OrderByDescending(e => e.DataCriacao)
                    .FirstOrDefault();

                if (ultimaMovimentacaoCliente != null)
                    valorSaldoBonusCliente = ultimaMovimentacaoCliente.SaldoBonus;

                var valorTotal = movimento.Quantidade * produto.Preco;
                movimento.ValorTotal = valorTotal;
                if (valorSaldoBonusCliente > valorTotal)
                {
                    movimento.SaldoBonus = valorSaldoBonusCliente - valorTotal;
                    movimento.ValorPago = 0;
                }
                else if (valorSaldoBonusCliente <= valorTotal)
                {
                    movimento.SaldoBonus = 0;
                    movimento.ValorPago = valorTotal - valorSaldoBonusCliente;
                }
                Context.Movimentos.Add(movimento);

                var configuracao = Context.Configuracoes.SingleOrDefault();
                if (configuracao == null)
                    return HttpNotFound();

                var nivelBonificacao = configuracao.NivelBonificacao;
                var bonusPorLitro = configuracao.BonusPorLitro;

                //Bonificação
                var cliente = Context.Pessoas.FirstOrDefault(e => e.Id == movimento.ClienteId);
                decimal valorBonus = bonusPorLitro * movimento.Quantidade;

                var pessoas = GetPessoasIndicacao(cliente.Usuario, movimento.EstabelecimentoId, nivelBonificacao).ToList().Distinct();
                foreach (var pessoa in pessoas)
                {
                    decimal saldoBonus = 0;
                    var ultimaMovimentacao = Context.Movimentos
                        .Where(e => e.ClienteId == pessoa.Id)
                        .OrderByDescending(e => e.DataCriacao)
                        .FirstOrDefault();
                    if (ultimaMovimentacao != null)
                        saldoBonus = ultimaMovimentacao.SaldoBonus;

                    var m = new Movimento()
                    {
                        ClienteId = pessoa.Id,
                        VendedorId = movimento.VendedorId,
                        EstabelecimentoId = movimento.EstabelecimentoId,
                        ProdutoId = movimento.ProdutoId,
                        TipoMovimento = TipoMovimento.RecebimentoBonus,
                        Quantidade = 0,
                        ValorPago = 0,
                        ValorTotal = 0,
                        ValorBonus = valorBonus,
                        SaldoBonus = saldoBonus + valorBonus
                    };
                    Context.Movimentos.Add(m);
                }

                Context.SaveChanges();
                return RedirectToAction("Cadastrar");
            }

            ViewBag.ClienteId = new SelectList(Context.Pessoas.Where(e => e.Tipo == TipoPessoa.Cliente), "Id", "Nome");
            ViewBag.EstabelecimentoId = new SelectList(Context.Estabelecimentos, "Id", "Nome");
            ViewBag.ProdutoId = new SelectList(Context.Produtos, "Id", "Nome");

            return View(movimento);
        }

        public IEnumerable<Pessoa> GetPessoasIndicacao(string emailPessoaIndicada, int estabelecimentoId, int nivel)
        {
            if (nivel > 0)
            {
                var indicacoes = Context.Indicacoes.Where(e => e.EmailDestino == emailPessoaIndicada).Select(e => e.PessoaId).ToList(); //Traz ids de pessoas que indicaram o cliente (por e-mail)
                var pessoas = Context.Pessoas.Where(e => indicacoes.Contains(e.Id)).ToList(); //Traz pessoas que indicaram o e-mail do cliente
                nivel--;

                foreach (var pessoa in pessoas)
                {
                    yield return pessoa;

                    var pessoas2 = GetPessoasIndicacao(pessoa.Usuario, estabelecimentoId, nivel).ToList();
                    foreach (var pessoa2 in pessoas2)
                    {
                        yield return pessoa2;
                    }
                }
            }
        }

        [ChildActionOnly]
        public ActionResult MeuSaldo()
        {
            decimal saldo = 0;

            var pessoa = GetUsuario();
            if (pessoa != null)
                saldo = pessoa.Movimentos.OrderByDescending(e => e.DataCriacao).Select(e => e.SaldoBonus).FirstOrDefault();

            return PartialView(saldo);
        }
    }
}
