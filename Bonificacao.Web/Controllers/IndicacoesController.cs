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
    public class IndicacoesController : Controller
    {
        BonificacaoContext context = new BonificacaoContext();

        public ViewResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult Indicar()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult Indicar(IndicacaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var pessoa = context.Pessoas.FirstOrDefault(e => e.Usuario == User.Identity.Name);
                    if (pessoa.Usuario == model.Email)
                    {
                        ModelState.AddModelError("Email", "Não foi possível indicar esse e-mail");
                        return PartialView();
                    }
                    var indicacao = new Indicacao()
                    {
                        EmailDestino = model.Email,
                        PessoaId = pessoa.Id
                    };

                    context.Indicacoes.Add(indicacao);
                    context.SaveChanges();

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
            var pessoa = context.Pessoas.FirstOrDefault(e => e.Usuario == User.Identity.Name);
            if (pessoa != null)
            {
                return PartialView(pessoa.Indicacoes.Select(e => e.EmailDestino).ToList());
            }
            return PartialView();
        }
    }
}