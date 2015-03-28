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
        // GET: Conta
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        // POST: Conta/Login
        [HttpPost]
        public ActionResult Login(string usuario, string senha, bool? lembrar)
        {
            try
            {              
                FormsAuthentication.SetAuthCookie(usuario, false);
                return RedirectToAction("Index", "Home");       
        
            }
            catch
            {
                return View();
            }
        }        
    }
}
