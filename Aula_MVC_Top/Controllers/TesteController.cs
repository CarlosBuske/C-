using Aula_MVC_Top.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aula_MVC_Top.Controllers
{
    public class TesteController : Controller
    {
        // GET: Teste
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                int aux = 0;
                
                try
                {
                    aux = vendedor.Logar();
                }
                catch (Exception)
                {
                    
                }
                if (aux != 0)
                {
                    Session["usuario"] = aux;
                    return RedirectToAction("Home");
                }
                else
                {
                    return View(vendedor);
                }
            }
        }

        public ActionResult Home()
        {
            if(Session["usuario"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }

        public ActionResult Cadastrar()
        {
            
                return View();
           
        }
        [HttpPost]
        public ActionResult Cadastrar(Vendedor vendedor)
        {

            return View();

        }

        public ActionResult Alterar()
        {
            if (Session["usuario"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult Alterar(Vendedor vendedor)
        {
            return View();
        }

       
        public ActionResult Sair()
        {
            Session.Remove("usuario");
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Sair(Vendedor vendedor)
        {
            Session.Remove("usuario");
            return RedirectToAction("Login");
        }

        public ActionResult Listar()
        {
            if (Session["usuario"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


    }
}