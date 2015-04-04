using Bonificacao.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bonificacao.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            var model = new HomeViewModel() { TipoUsuario = base.GetTipoUsuario() };
            return View(model);
        }
    }
}