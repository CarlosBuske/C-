using Atividade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Atividade.Controllers
{
    public class InicialController : Controller
    {
        // GET: Inicial
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginUsuario(Usuario usuario)
        {
            Session["Tipo"] = "Usuario";
            return RedirectToAction("TelasUsuario");
        }

        public ActionResult LoginAdm()
        {
            List<Usuario> lista = new List<Usuario>();
            Usuario u = new Usuario();
            lista = u.Busca();
            ViewBag.Lista = lista;
            return View();
        }

        [HttpPost]
        public ActionResult LoginAdm(Usuario usuario)
        {
                Session["Tipo"] = "Adm";
            
                if(usuario.Login.Equals("Adm") && usuario.Senha.Equals("123") )
                {
                    return RedirectToAction("TelasAdm");
                }
                else
                {
                    return RedirectToAction("LoginUsuario");
                }
            
        }

        public ActionResult TelasAdm()
        {
            if(Session["Tipo"] != null)
            {

                return View();
            }
            return RedirectToAction("LoginUsuario");
        }

        [HttpPost]
        public ActionResult TelasAdm(Admnistrados admnistrados)
        {
            return View();
        }

        public ActionResult TelasUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TelasUsuario(Admnistrados admnistrados)
        {
            return View();
        }
        

        public ActionResult TelasCadastraNoticia()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TelasCadastraNoticia(FormCollection form, Usuario usuario)
        {
            
                Usuario u = new Usuario();
                u.CadastrarNoticia(usuario.Tipo, form["txt_noticia"]);
                return View();
            
            
        }

        public ActionResult ValidaTipo(Usuario usuario)
        {

            if (usuario.Tipo == "Esporte" )
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            if ( usuario.Tipo == "Noticia")
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            if (usuario.Tipo == "Famosos")
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            if (usuario.Tipo == "Tecnologia&Games")
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            if (usuario.Tipo == "Politica")
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            if (usuario.Tipo == "Carro")
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Sair()
        {
            Session.Remove("Tipo");
            return RedirectToAction("LoginUsuario");
        }

        [HttpPost]
        public ActionResult Sair(Usuario usuario)
        {
            Session.Remove("Tipo");
            return RedirectToAction("LoginUsuario");
        }

        public ActionResult Voltar()
        {
            return RedirectToAction("TelasAdm");
        }

        [HttpPost]
        public ActionResult Voltar(Usuario usuario)
        {
            return RedirectToAction("TelasAdm");
        }

        public ActionResult Excluir()
        {
            return RedirectToAction("TelasAdm");
        }

        [HttpPost]
        public ActionResult Excluir(Usuario usuario)
        {
            Usuario u = new Usuario();
            u.Excluir(Convert.ToInt32(usuario.cod_noticia));
            return RedirectToAction("TelasAdm");
        }

        public ActionResult CadastrarNova()
        {
            return RedirectToAction("TelasCadastraNoticia");
        }

        [HttpPost]
        public ActionResult CadastrarNova(Usuario usuario)
        {
            return RedirectToAction("TelasCadastraNoticia");
        }




    }
}