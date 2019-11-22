using Aula_MVC_Top.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aula_MVC_Top.Controllers
{
    public class MateriaController : Controller
    {
        // GET: Materia

        FACEAR_2019Entities2 db = new FACEAR_2019Entities2();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Outros_Comandos_Jquery()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.Alunos = new SelectList(db.Alunos.ToList(), "Id_Aluno", "Nome", 2);
            return View();
        }

        public ActionResult Edit()
        {

            var Materia = db.Materia.FirstOrDefault();
            ViewBag.Alunos = new SelectList(db.Alunos.ToList(), "Id_Aluno", "Nome", 2);
            return View(Materia);
        }

        public JsonResult Retorna_Hora()
        {
            DateTime hora_atual = DateTime.Now;
            string Nome_Autor = "Professor";

            return Json(new
            {
                hora = hora_atual.ToString("HH:mm:ss"),
                Nome_Autor

            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Retorna_Qtd_Letras(string nome, string sobrenome)
        {
            int qtd = nome.Length;
            qtd += sobrenome.Length;
            return Json(new
            {
                qtd

            }, JsonRequestBehavior.AllowGet);
        }

        
    }
}