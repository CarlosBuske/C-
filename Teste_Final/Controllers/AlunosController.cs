using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Teste_Final.Models;

namespace Teste_Final.Controllers
{
    public class AlunosController : Controller
    {
        FACEAR_2019Entities db = new FACEAR_2019Entities();
        // GET: Alunos
        public ActionResult Inicial()
        {
            return View();
        }

        public ActionResult Add_novo_aluno()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add_novo_aluno(Alunos alunos)
        {
            //if (!ModelState.IsValid)
            //{
            //    ModelState.AddModelError("", "Tem Algo Errado");
            //    return View();
            //}
            //else
            //{
                alunos.Excluido = false;
                db.Alunos.Add(alunos);
                db.SaveChanges();
                return RedirectToAction("Inicial");
                
            //}

                
        }

        public ActionResult Editar(int Id_Aluno)
        {
            
            return View();
        }

        public JsonResult Lista_Tudo()
        {
            var lista = db.Alunos.ToList().OrderBy(x => x.Nome);
            StringBuilder str = new StringBuilder();
            IList<Alunos> Lista_Alunos = new List<Alunos>();

            str.Append("<table width=\"100%\" border=\"1\">");
            str.Append("<tr><th>Id</th><th>Nome</th><th>Sexo</th><th>Editar</th></tr>");
            foreach (var item in lista)
            {
                if (item.Ativo != false)
                {
                    Alunos a = new Alunos();
                    a.Nome = item.Nome;
                    a.Sexo = item.Sexo;
                    Lista_Alunos.Add(a);

                    str.Append("<tr><td>" + item.Id_Aluno + "</td><td>" + item.Nome + "</td><td>" + item.Sexo + "</td><td> <a href=/Alunos/Editar?Id_Aluno="+item.Id_Aluno+">Editar</a> </td></tr>");
                }
            }
            return Json(new
            {
                tbl_alunos = str.ToString(),
                Lista_Alunos
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Lista_Tudo2()
        {
            var lista = db.Alunos.ToList().OrderBy(x => x.Nome);
            IList<Alunos> Lista_Alunos = new List<Alunos>();

            //Lista_Alunos = (IList<Alunos>) lista;
            foreach (var item in lista)
            {
                Alunos a = new Alunos();
                a.Nome = item.Nome;
                a.Sexo = item.Sexo;
                a.Id_Aluno = item.Id_Aluno;
                a.Ativo = item.Ativo;
                Lista_Alunos.Add(a);
            }

            return Json(new
            {
                Lista_Alunos
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Pesquisa_expecifica(string txt_pesquisa)
        {
            var lista = db.Alunos.Where(x => x.Nome.Contains(txt_pesquisa));
            StringBuilder str = new StringBuilder();
            IList<Alunos> Lista_Alunos = new List<Alunos>();

            str.Append("<table width=\"100%\" border=\"1\">");
            str.Append("<tr><th>Id</th><th>Nome</th><th>Sexo</th><th>Editar</th></tr>");
            foreach (var item in lista)
            {
                if (item.Ativo != false)
                {
                    Alunos a = new Alunos();
                    a.Nome = item.Nome;
                    a.Sexo = item.Sexo;
                    Lista_Alunos.Add(a);

                    str.Append("<tr><td>" + item.Id_Aluno + "</td><td>" + item.Nome + "</td><td>" + item.Sexo + "</td><td> @Html.ActionLink(\"Editar\", \"Editar\", new { id = "+item.Id_Aluno+" }) </ td ></tr>");
                    
                }
            }
            return Json(new
            {
                tbl_alunos = str.ToString()
            }, JsonRequestBehavior.AllowGet);
        }
    }
}