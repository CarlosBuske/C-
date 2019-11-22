using Aula_MVC_Top.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Aula_MVC_Top.Controllers
{
    public class AlunosController : Controller
    {
        // GET: Alunos
        FACEAR_2019Entities2 db = new FACEAR_2019Entities2();
        public ActionResult Index()
        {
            // Busca Tudo
            var Lista = db.Alunos.ToList();
            //var Lista = db.Alunos.FirstOrDefault();

            //Pega a Quantidade que eu quero
            //var Lista = db.Alunos.Take(2);

            //Ordena Tudo pelo oq voce que
            //var Lista = db.Alunos.OrderBy(x => x.Nome);

            //Pega um dado que contenha oq eu passo
            //var Lista = db.Alunos.Where(x => x.Nome.Contains("x"));

            //var Lista = db.Alunos.Where(x => x.Nome.StartsWith("y") && x.Sexo == "F").Take(5).OrderBy(x => x.Nome);

            //int qtd = db.Alunos.Where(x=> x.Nome.Contains("a")).Count();
            //ViewBag.qtd = qtd;
            //int soma_id = db.Alunos.Sum(x => x.Id_Aluno);
            //ViewBag.soma = soma_id;
            return View(Lista);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            string aux = (string)form["txtNome"];
            string Sexo = (string)form["txtSexo"];
            var Lista = db.Alunos.Where(x => x.Nome.Contains(aux) && x.Sexo.Contains(Sexo));
            return View(Lista);
        }

        public ActionResult Create()
        {
            ViewBag.Materia = new SelectList(db.Materia.ToList(), "Id_Materia", "Descricao", "");
            return View();
            
        }
        [HttpPost]
        public ActionResult Create(Alunos alunos, FormCollection form)
        {
            db.Alunos.Add(alunos);
            db.SaveChanges();
            return View();

        }

        public ActionResult Lista_Alunos()
        {
            var Lista = db.Alunos.ToList();
            return View(Lista);
        }

        public JsonResult Retorna_Lista(string nome, string sexo)
        {
            
            var lista = db.Alunos.Where(x => x.Nome.Contains(nome) && x.Sexo.Contains(sexo));
            StringBuilder str = new StringBuilder();
            IList<Alunos> listaa = new List<Alunos>();

            str.Append("<table width=\"100%\" border=\"1\">");
            foreach (var item in lista)
            {
                
                str.Append("<tr><td>" + item.Nome + "</td><td>" + item.Sexo + "</td></tr>");
            }
            str.Append("</table>");
            return Json(new
            {
                lista_alunos = str.ToString()
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Adicionar()
        {
            
            return View();

        }
        [HttpPost]
        public ActionResult Adicionar(FormCollection form)
        {
            Alunos a = new Alunos();
            a.Nome = form["txt_nome"];
            a.Sexo = form["txt_sexo"];

            IList<Nota> lista_nota = new List<Nota>();
            if (Session["Notas"] != null)
            {
                lista_nota = (List<Nota>)Session["Notas"];
            }


            a.Nota = lista_nota;
            db.Alunos.Add(a);
            db.SaveChanges();
            Session.Remove("Notas");
            return View();

        }

        public JsonResult Alterar_Nota(int id_nota, string materia, int nota1, int nota2, int nota3, int nota4)
        {
            IList<Nota> lista_nota = new List<Nota>();
            StringBuilder str = new StringBuilder();
            lista_nota = (List<Nota>)Session["Notas"];
            for (int i = 0; i < lista_nota.Count; i++)
            {
                if (lista_nota[i].Id_Nota == id_nota)
                {
                    lista_nota[i].Materia = materia;
                    lista_nota[i].Nota_I = nota1;
                    lista_nota[i].Nota_II = nota2;
                    lista_nota[i].Nota_III = nota3;
                    lista_nota[i].Nota_IV = nota4;

                    Session["Notas"] = lista_nota;
                }
            }

            return Json(new
            {
                
                lista = lista_nota
            }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Adicionar_Nota( string materia, int nota1, int nota2, int nota3, int nota4)
        {
           
            IList<Nota> lista_nota = new List<Nota>();
            StringBuilder str = new StringBuilder();
            if (Session["Notas"] != null){
                lista_nota = (List<Nota>)Session["Notas"];
            }
                Nota n = new Nota();
                n.Id_Nota = lista_nota.Count + 1;
                n.Materia = materia;
                n.Nota_I = nota1;
                n.Nota_II = nota2;
                n.Nota_III = nota3;
                n.Nota_IV = nota4;
                lista_nota.Add(n);
                Session["Notas"] = lista_nota;

            str.Append("<table width=\"100%\" border=\"1\">");
            str.Append("<tr><th>Materia</th><th>Nota I</th><th>Nota II</th><th>Nota III</th><th>Nota IV</th><th>Editar</th><th>Excluir</th></tr>");
            foreach (var item in lista_nota)
            {

                str.Append("<tr><td>" + item.Materia + "</td><td>" + item.Nota_I + "</td><td>" + item.Nota_II + "</td><td>" + item.Nota_III + "</td><td>" + item.Nota_IV + "</td></tr>" );
            }
            str.Append("</table>");

            return Json(new
            {
                lista_teste = str.ToString(),
                lista = lista_nota
            }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Delete(int id)
        {

            IList<Nota> lista_nota = new List<Nota>();
            StringBuilder str = new StringBuilder();
            lista_nota = (List<Nota>)Session["Notas"];
            for (int i = 0; i < lista_nota.Count; i++)
            {
                if (lista_nota[i].Id_Nota == id)
                {
                    lista_nota.Remove(lista_nota[i]);

                    Session["Notas"] = lista_nota;
                }
            }

            return Json(new
            {

                lista = lista_nota
            }, JsonRequestBehavior.AllowGet);
        }


    }
}

////ver se já existe 
//            if (db.Alunos.Any(x=> x.Nome == "Fabiano"))
//            {
//                var A = db.Alunos.FirstOrDefault(x => x.Nome == "Fabiano");
//A.Nome = "Fabiano Nezello";

//                var N = A.Nota.First();
//N.Materia = "PHP";
//                N.Nota_I = 10.0m;
//                db.Entry(A).State = EntityState.Modified;
//                db.SaveChanges();
//            }
//            else
//            {
//                Alunos a = new Alunos();
//a.Nome = "Fabiano";
//                a.Sexo = "M";


//                Nota N = new Nota();
//N.Nota_I = 5;
//                N.Nota_II = 7.1m;
//                N.Nota_III = 9.5m;

//                N.Nota_IV = 4.5m;
//                N.Materia = "POO";


//                a.Nota.Add(N);

//                Nota N_ = new Nota();
//N_.Nota_I = 5.6m;
//                N_.Nota_II = 8.8m;
//                N_.Nota_III = 5.6m;

//                N_.Nota_IV = 4.5m;
//                N_.Materia = "JAVA";


//                a.Nota.Add(N_);

//                db.Alunos.Add(a);
//                db.SaveChanges();

//                return View();
//            }