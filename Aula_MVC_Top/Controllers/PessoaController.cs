using Aula_MVC_Top.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aula_MVC_Top.Controllers
{
    public class PessoaController : Controller
    {
        // GET: Pessoa

        public ActionResult Details()
        {
            Pessoa p1 = new Pessoa();
            p1.Id_Pessoa = 3;
            p1.Nome = "Hellen";
            p1.Idade = 56;

            return View(p1);
        }
        public ActionResult Index()
        {
            ViewData["Id_Pessoa"] = 1;
            ViewData["Nome"] = "Carlos";
            ViewData["Idade"] = 21;

            Pessoa p1 = new Pessoa();
            p1.Id_Pessoa = 2;
            p1.Nome = "Marcão";
            p1.Idade = 55;

            ViewBag.Id = p1.Id_Pessoa;
            ViewBag.Nome = p1.Nome;
            ViewBag.idade = p1.Idade;

            return View();
        }
    }
}