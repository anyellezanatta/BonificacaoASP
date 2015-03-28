using Bonificacao.Data;
using Bonificacao.Web.Models;
using System;
using System.Collections.Generic;
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
                    var usuario = db.Pessoas.FirstOrDefault(p => p.Usuario == loginModel.Usuario && p.Senha == loginModel.Senha);
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

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
