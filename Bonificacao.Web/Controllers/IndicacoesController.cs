using Bonificacao.Data;
using Bonificacao.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bonificacao.Web.Controllers
{
    [Authorize]
    public class IndicacoesController : ControllerBase
    {
        public PartialViewResult MinhasIndicacoes()
        {
            var pessoa = GetUsuario();
            if (pessoa != null)
            {
                var indicacoes = pessoa.Indicacoes
                    .OrderByDescending(e => e.DataCriacao)
                    .Select(e =>
                    new MinhaIndicacaoModel
                    {
                        Email = e.EmailDestino,
                        Estabelecimento = e.Estabelecimento.Nome,
                        Data = e.DataCriacao.ToString("dd/MM/yyyy")
                    }).ToList();

                return PartialView(indicacoes);
            }
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult Indicar()
        {
            var model = new IndicacaoModel();

            ViewBag.Estabelecimentos = Context.Estabelecimentos
                .ToList()
                .OrderBy(e => e.Nome)
                .Select(e => new SelectListItem()
                {
                    Text = e.Nome,
                    Value = e.Id.ToString()
                });

            return PartialView(model);
        }

        [HttpPost]
        public PartialViewResult Indicar(IndicacaoModel model)
        {
            ViewBag.Estabelecimentos = Context.Estabelecimentos
                    .ToList()
                    .OrderBy(e => e.Nome)
                    .Select(e => new SelectListItem()
                    {
                        Text = e.Nome,
                        Value = e.Id.ToString()
                    });

            if (ModelState.IsValid)
            {
                try
                {
                    var pessoa = GetUsuario();
                    var usuario = Context.Pessoas.FirstOrDefault(e => e.Usuario == model.Email);
                    if (pessoa.Usuario == model.Email || usuario != null)
                    {
                        ModelState.AddModelError("Email", "Não foi possível indicar esse e-mail");
                        return PartialView(model);
                    }
                    var indicacao = new Indicacao()
                    {
                        EmailDestino = model.Email,
                        PessoaId = pessoa.Id,
                        EstabelecimentoId = model.EstabelecimentoSelecionado.Value
                    };

                    Context.Indicacoes.Add(indicacao);
                    Context.SaveChanges();

                    var estabelecimento = Context.Estabelecimentos.Find(model.EstabelecimentoSelecionado.Value);

                    var usuarioEmail = ConfigurationManager.AppSettings["SendgridAccount"];
                    var senhaEmail = ConfigurationManager.AppSettings["SendgridKey"];

                    var email = new SendGrid.SendGridMessage(
                        new MailAddress("anyelle.ad@gmail.com"),
                        new MailAddress[] { new MailAddress(model.Email) },
                        "Indicação de posto",
                        "<p>Você foi indicado para abastecer no posto " + estabelecimento.Nome + ", clique <a href=\"" + Url.Action("Cadastro", "Conta", new { email = model.Email }, Request.Url.Scheme) + "\">aqui</a> para se cadastrar</p>",
                        "Você foi indicado para abastecer no posto " + estabelecimento.Nome);
                    var client = new SendGrid.Web(new System.Net.NetworkCredential(usuarioEmail, senhaEmail));
                    client.Deliver(email);
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("Email", "Não foi possível indicar esse e-mail");
                    return PartialView(model);
                }
                catch (Exception)
                {
                }
            }

            return PartialView();
        }


    }
}