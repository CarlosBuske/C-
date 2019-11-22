using Aula_MVC_Top.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aula_MVC_Top.Controllers
{
    public class AlunoController : Controller
    {
        // GET: Aluno
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Create()
        {
            Aluno a1 = new Aluno();
            return View(a1);
        }

        //[HttpPost]
        //public ActionResult() { }
        [HttpPost]
        public ActionResult Create(int txt_id, string txt_nome, int txt_idade, string lista_alunos)
        {



            
            if (txt_idade < 18)
            {
                ViewBag.Erro = "Idade menor que 18";
                ViewBag.Id = txt_idade;
                ViewBag.Nome = txt_idade;
                ViewBag.idade = txt_idade;
            }
            else
            {
                Aluno a1 = new Aluno();
                a1.Id_Pessoa = txt_id;
                a1.Nome = txt_nome;
                a1.Idade = txt_idade;
                lista_alunos += "<br/>" + a1.Id_Pessoa+ " "+ a1.Nome + " "+ a1.Idade;
                ViewData["lista_alunos"] = lista_alunos;
                ViewBag.Id = "";
                ViewBag.Nome = "";
                ViewBag.idade = "";

            }
            


            return View();
        }

        
        public ActionResult NewAluno()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewAluno(FormCollection form)
        {
            Pessoa p1 = new Pessoa();
            p1.Nome = form["txt_nome"];
            p1.Idade = Convert.ToInt32(form["txt_idade"]);
            return View();
        }




        public ActionResult Novo_Aluno()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Novo_Aluno(Aluno p1, FormCollection form)
        {
            if (string.IsNullOrEmpty(p1.Nome))
            {
                ModelState.AddModelError("", "O campo não pode ser nulo");
            }

            if (!p1.Idade.HasValue)
            {
                ModelState.AddModelError("", "O campo idade não pode ser nulo");
            }

            if (string.IsNullOrEmpty(p1.Sexo) || p1.Sexo != "M" && p1.Sexo != "F")
            {
                ModelState.AddModelError("Sexo", "Valor do campo Sexo Invalido");
            }

            if (!p1.Peso.HasValue || p1.Peso < 0 || p1.Peso >300)
            {
                ModelState.AddModelError("Peso", "Valor do peso invalido");
            }
            return View();
        }



    }
}