﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bonificacao.Data;

namespace Bonificacao.Web.Controllers
{
    [Authorize]
    public class GruposEstabelecimentoController : ControllerBase
    {
        // GET: GruposEstabelecimento
        public ActionResult Index()
        {
            return View(Context.GruposEstabelecimento.ToList());
        }

        // GET: GruposEstabelecimento/Cadastrar
        public ActionResult Cadastrar()
        {
            return View();
        }

        // POST: GruposEstabelecimento/Cadastrar        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar([Bind(Include = "Id,Nome,DataCriacao,DataModificacao")] GrupoEstabelecimento grupoEstabelecimento)
        {
            if (ModelState.IsValid)
            {
                Context.GruposEstabelecimento.Add(grupoEstabelecimento);
                Context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(grupoEstabelecimento);
        }

        // GET: GruposEstabelecimento/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrupoEstabelecimento grupoEstabelecimento = Context.GruposEstabelecimento.Find(id);
            if (grupoEstabelecimento == null)
            {
                return HttpNotFound();
            }
            return View(grupoEstabelecimento);
        }

        // POST: GruposEstabelecimento/Editar/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Nome,DataCriacao,DataModificacao")] GrupoEstabelecimento grupoEstabelecimento)
        {
            if (ModelState.IsValid)
            {
                Context.Entry(grupoEstabelecimento).State = EntityState.Modified;
                Context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grupoEstabelecimento);
        }

        // POST: GruposEstabelecimento/Deletar/5
        [HttpPost]
        public ActionResult Deletar(int id)
        {
            try
            {
                GrupoEstabelecimento grupoEstabelecimento = Context.GruposEstabelecimento.Find(id);
                Context.GruposEstabelecimento.Remove(grupoEstabelecimento);
                Context.SaveChanges();

                return Content("Item removido com sucesso");
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}
