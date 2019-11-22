using Aula_MVC_Top.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aula_MVC_Top.Controllers
{
    public class ProfessorController : Controller
    {
        // GET: Professor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(Professor professor)
        {
            if (!ModelState.IsValid)
            {
                return View(professor);
            }
            else
            {
                List<string> minha_lista = new List<string>();
                if(Session["lista_cpf"] != null)
                {
                    minha_lista = (List<string>)Session["lista_cpf"];
                }
                minha_lista.Add(professor.Cpf);

                Session["lista_cpf"] = minha_lista;
                return View();
                //aqui poderia mandar salvar no banco 
            }
        }

        public ActionResult  validacao(string sexo)
        {

            if(sexo.ToLower() == "m" || sexo.ToUpper() == "M")
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult valida_cpf(string cpf, string email)
        {
            if (Session["lista_cpf"] != null)
            {
                List<string> minha_lista;
                minha_lista = (List<string>)Session["lista_cpf"];
                if (minha_lista.Contains(cpf))
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            
        }

        //public ActionResult validaCpf(string cpf)
        //{
        //    List<string> lista_cpf = (List<string>)Session["lista"];

        //    if(lista_cpf !=  null){
        //        foreach (var cpfExistente in lista_cpf)
        //        {
        //            if (cpf == cpfExistente)
        //            {
        //                return Json(false, JsonRequestBehavior.AllowGet);
        //            }
        //        }


        //        lista_cpf.Add(cpf);
        //        Session["lista"] = lista_cpf;
        //    }
        //    return Json(true, JsonRequestBehavior.AllowGet);

        //}
    }
}

