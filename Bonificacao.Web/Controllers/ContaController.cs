using Bonificacao.Data;
using Bonificacao.Web.Models;
using Bonificacao.Data.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Diagnostics;
using System.Net;

namespace Bonificacao.Web.Controllers
{
    [AllowAnonymous]
    public class ContaController : ControllerBase
    {
        public ActionResult Usuarios()
        {
            var pessoaLogada = GetUsuario();

            var usuarios = Context.Pessoas.Where(p => p.Usuario != pessoaLogada.Usuario).Select(p =>
                new UsuarioModel()
                {
                    Id = p.Id,
                    Email = p.Usuario,
                    Nome = p.Nome,
                    Tipo = p.Tipo.ToString(),
                    Estabelecimento = p.Estabelecimento != null ? p.Estabelecimento.Nome : null
                });

            return View(usuarios);
        }

        public ActionResult DeletarUsuario(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = Context.Pessoas.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }

            return View(pessoa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletarUsuario(int id)
        {
            Pessoa pessoa = Context.Pessoas.Find(id);
            Context.Pessoas.Remove(pessoa);
            Context.SaveChanges();
            return RedirectToAction("Usuarios");
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var senhaEncriptada = SHA256Generator.GetHash(loginModel.Senha);
                    var usuario = Context.Pessoas.FirstOrDefault(p => p.Usuario == loginModel.Usuario && p.Senha == senhaEncriptada);
                    if (usuario != null)
                    {
                        FormsAuthentication.SetAuthCookie(loginModel.Usuario, loginModel.Lembrar);
                        Session["Tipo"] = usuario.Tipo;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Usuário ou senha inválidos");

                        return View(loginModel);
                    }

                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    return View();
                }
            }
            return View(loginModel);
        }

        public ActionResult Cadastro(string email = null)
        {
            ViewBag.Estabelecimentos = new SelectList(Context.Estabelecimentos.ToList(), "Id", "Nome");

            return View(new CadastroModel() { Email = email, TipoUsuario = GetTipoUsuario() });
        }

        [HttpPost]
        public ActionResult Cadastro(CadastroModel loginModel)
        {
            ViewBag.Estabelecimentos = new SelectList(Context.Estabelecimentos.ToList(), "Id", "Nome");
            loginModel.TipoUsuario = GetTipoUsuario();

            if (loginModel.Tipo == TipoPessoa.Vendedor && loginModel.EstabelecimentoId.GetValueOrDefault(0) == 0)
                ModelState.AddModelError("EstabelecimentoId", "O campo Estabelecimento é obrigatório");

            if (ModelState.IsValid)
            {
                try
                {
                    var pessoa = new Pessoa()
                    {
                        //Se tipo for diferente de null, então recebe tipo, se não tipo é cliente
                        Tipo = loginModel.Tipo.HasValue ? loginModel.Tipo.Value : TipoPessoa.Cliente,
                        Nome = loginModel.Nome,
                        Senha = SHA256Generator.GetHash(loginModel.Senha),
                        Usuario = loginModel.Email,
                        EstabelecimentoId = loginModel.EstabelecimentoId
                    };
                    Context.Pessoas.Add(pessoa);
                    var result = Context.SaveChanges() > 0;

                    if (!User.Identity.IsAuthenticated)
                    {
                        FormsAuthentication.SetAuthCookie(loginModel.Email, false);
                        Session["Tipo"] = pessoa.Tipo;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                        return RedirectToAction("Usuarios");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "E-mail já cadastrado");
                    loginModel.Email = "";
                    return View(loginModel);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Não foi possível salvar o cadastro");
                    return View(loginModel);
                }
            }
            return View(loginModel);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["Tipo"] = null;
            return RedirectToAction("Login");
        }
    }
}
