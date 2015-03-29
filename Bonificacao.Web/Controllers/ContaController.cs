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

namespace Bonificacao.Web.Controllers
{
    [AllowAnonymous]
    public class ContaController : Controller
    {
        BonificacaoContext db = new BonificacaoContext();

        // GET: Conta/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Conta/Login
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var senhaEncriptada = SHA256Generator.GetHash(loginModel.Senha);
                    var usuario = db.Pessoas.FirstOrDefault(p => p.Usuario == loginModel.Usuario && p.Senha == senhaEncriptada);
                    if (usuario != null)
                        FormsAuthentication.SetAuthCookie(loginModel.Usuario, loginModel.Lembrar);
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

        // GET: Conta/Cadastro
        public ActionResult Cadastro()
        {
            var email = User.Identity.Name;
            var usuario = db.Pessoas.FirstOrDefault(e => e.Usuario == email);
            ViewBag.Administrador = (usuario != null && usuario.Tipo == TipoPessoa.Administrador);

            return View();
        }

        // POST: Conta/Cadastro
        [HttpPost]
        public ActionResult Cadastro(CadastroModel loginModel)
        {
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
                        Usuario = loginModel.Email
                    };
                    db.Pessoas.Add(pessoa);
                    var result = db.SaveChanges() > 0;

                    FormsAuthentication.SignOut();
                    FormsAuthentication.SetAuthCookie(loginModel.Email, false);

                    return RedirectToAction("Index", "Home");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "E-mail já cadastrado");
                    loginModel.Email = "";
                    return View(loginModel);
                }
                catch (Exception)
                {
                    return View();
                }
            }
            return View(loginModel);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        protected override void Dispose(bool disposing)
        {
            if (db != null)
            {
                db.Dispose();
                db = null;
            }
            base.Dispose(disposing);
        }
    }
}
