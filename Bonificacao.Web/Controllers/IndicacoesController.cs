using Bonificacao.Data;
using Bonificacao.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Bonificacao.Web.Controllers
{
    public class IndicacoesController : ControllerBase
    {
        public ViewResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult Indicar()
        {
            ViewBag.Estabelecimentos = Context.Estabelecimentos
                .ToList()
                .OrderBy(e => e.Nome)
                .Select(e => new SelectListItem()
                {
                    Text = e.Nome,
                    Value = e.Id.ToString()
                });
            return PartialView();
        }

        [ChildActionOnly]
        [HttpPost]
        public PartialViewResult Indicar(IndicacaoModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Estabelecimentos = Context.Estabelecimentos
                    .ToList()
                    .OrderBy(e => e.Nome)
                    .Select(e => new SelectListItem()
                    {
                        Text = e.Nome,
                        Value = e.Id.ToString()
                    });

                try
                {
                    var pessoa = Context.Pessoas.FirstOrDefault(e => e.Usuario == User.Identity.Name);
                    if (pessoa.Usuario == model.Email)
                    {
                        ModelState.AddModelError("Email", "Não foi possível indicar esse e-mail");
                        return PartialView();
                    }
                    var indicacao = new Indicacao()
                    {
                        EmailDestino = model.Email,
                        PessoaId = pessoa.Id,
                        EstabelecimentoId = model.EstabelecimentoSelecionado.Value
                    };

                    Context.Indicacoes.Add(indicacao);
                    Context.SaveChanges();

                    var usuarioEmail = ConfigurationManager.AppSettings["SendgridAccount"];
                    var senhaEmail = ConfigurationManager.AppSettings["SendgridKey"];

                    var email = new SendGrid.SendGridMessage(
                        new MailAddress("anyelle.ad@gmail.com"),
                        new MailAddress[] { new MailAddress(model.Email) },
                        "Indicação de posto",
                        "<p>Você foi indicado para abastecer no posto, clique <a href=\"" + Url.Action("Cadastro", "Conta", new { email = model.Email }, Request.Url.Scheme) + "\">aqui</a> para se cadastrar</p>",
                        "Você foi indicado para abastecer no posto");
                    var client = new SendGrid.Web(new System.Net.NetworkCredential(usuarioEmail, senhaEmail));
                    client.Deliver(email);
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("Email", "Não foi possível indicar esse e-mail");
                }
                catch (Exception)
                {
                }
            }

            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult MinhasIndicacoes()
        {
            var pessoa = Context.Pessoas.FirstOrDefault(e => e.Usuario == User.Identity.Name);
            if (pessoa != null)
            {
                return PartialView(pessoa.Indicacoes.Select(e => new MinhaIndicacaoModel { Email = e.EmailDestino, Estabelecimento = e.Estabelecimento.Nome }).ToList());
            }
            return PartialView();
        }
    }
}