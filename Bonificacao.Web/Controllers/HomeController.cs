using Bonificacao.Data;
using Bonificacao.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net.Mail;
using System.Configuration;
using System.Data.Entity.Infrastructure;

namespace Bonificacao.Web.Controllers
{
    [Authorize]
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Index(int[] clientes = null, string datainicio = null, string datafim = null)
        //{
        //    var usuario = base.GetUsuario();
        //    if (usuario == null)
        //        return HttpNotFound();

        //    IQueryable<Movimento> movimentos = Context.Movimentos
        //        .Include(m => m.Cliente)
        //        .Include(m => m.Estabelecimento)
        //        .Include(m => m.Produto)
        //        .Include(m => m.Vendedor);

        //    if (clientes != null && clientes.Any())
        //        movimentos = movimentos.Where(m => clientes.Contains(m.ClienteId));

        //    if (!string.IsNullOrEmpty(datainicio) && !string.IsNullOrEmpty(datafim))
        //    {
        //        var dataInicial = DateTimeOffset.Parse(datainicio, new CultureInfo("pt-BR"));
        //        var dtFinal = DateTimeOffset.Parse(datafim, new CultureInfo("pt-BR"));
        //        var dataFinal = new DateTimeOffset(dtFinal.Year, dtFinal.Month, dtFinal.Day, 23, 59, 59, dtFinal.Offset);
        //        movimentos = movimentos.Where(m => m.DataCriacao >= dataInicial && m.DataCriacao <= dataFinal);
        //    }

        //    IQueryable<Pessoa> opcoesClientes = Context.Pessoas.Where(e => e.Tipo == TipoPessoa.Cliente);
        //    if (usuario.Tipo == TipoPessoa.Cliente)
        //    {
        //        movimentos = movimentos.Where(e => e.ClienteId == usuario.Id);
        //        opcoesClientes = opcoesClientes.Where(e => e.Id == usuario.Id);
        //    }

        //    var movimentacoes = movimentos.OrderByDescending(e => e.DataCriacao).ToList();
        //    var opcoesCliente = opcoesClientes.OrderBy(e => e.Nome).ToList();

        //    var model = new HomeViewModel()
        //    {
        //        Movimentacoes = movimentacoes
        //    };

        //    ViewBag.OpcoesClientes = new SelectList(opcoesCliente, "Id", "Nome");

        //    return View(model);
        //}
    }
}