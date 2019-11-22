using Aula_MVC_Top.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aula_MVC_Top.Controllers
{
    public class ProdutoController : Controller
    {
        FACEAR_2019Entities2 db = new FACEAR_2019Entities2();
        // GET: Produto
        public ActionResult Index()
        {
            var Lista = db.Produto.ToList();
            return View(Lista);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Produto produto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Tem Alho Errado");
                return View(produto);
            }
            else
            {
                try
                {
                    db.Produto.Add(produto);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message.ToString());
                    return View(produto);
                }
            }
            
            
        }

        public ActionResult Edit(int id)
        {
            var P = db.Produto.Find(id);
            return View(P);
        }

        [HttpPost]
        public ActionResult Edit(Produto produto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(produto).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("", ex.Message.ToString());
                    return View(produto);
                }
                
            }
            else
            {
                return View(produto);
            }
        }
        public ActionResult Delete(int id)
        {
            var P = db.Produto.Find(id);
            return View(P);
        }

        [HttpPost]
        public ActionResult Delete(Produto produto)
        {
            try
            {
                var P = db.Produto.Find(produto.Id_produtto);
                db.Produto.Remove(P);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message.ToString());
                return View(produto);
            }
        }


    }
}