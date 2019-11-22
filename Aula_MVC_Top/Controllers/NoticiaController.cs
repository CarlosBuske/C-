using Aula_MVC_Top.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aula_MVC_Top.Controllers
{
    public class NoticiaController : Controller
    {
        // GET: Noticia

        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Index()
        {
            IList<Noticias> lista_noticias = new List<Noticias>();
            if (Session["Noticias"] != null)
            {
                lista_noticias = (List<Noticias>)Session["Noticias"];
            }
            else
            {
                Noticias m1 = new Noticias();
                m1.Id = 1;
                m1.Id_Topico = 1;
                m1.Titulo = "Neymar";
                m1.Descricao = "Neymar faz mais que tres gols pela seleção";
                lista_noticias.Add(m1);

                Noticias m2 = new Noticias();
                m2.Id = 2;
                m2.Id_Topico = 2;
                m2.Titulo = "Gastronomia";
                m2.Descricao = "Novo Restaurante Renomado Abre em São Paulo";
                lista_noticias.Add(m2);

                Noticias m3 = new Noticias();
                m3.Id = 3;
                m3.Id_Topico = 1;
                m3.Titulo = "Esporte";
                m3.Descricao = "Atletico ganha do gremio";
                lista_noticias.Add(m3);

                Session["Noticias"] = lista_noticias;

            }
            return View(lista_noticias);
        }

        public ActionResult Edit(int Id)
        {
            List<Noticias> lista_noticias = new List<Noticias>();
            if (Session["Noticias"] != null)
            {
                lista_noticias = (List<Noticias>)Session["Noticias"];
            }

            var n1 = lista_noticias.Find(x => x.Id == Id);

            return View(n1);
        }

        [HttpPost]
        public ActionResult Edit(Noticias noticias)
        {
            List<Noticias> lista_noticias = new List<Noticias>();
            if (Session["Noticias"] != null)
            {
                lista_noticias = (List<Noticias>)Session["Noticias"];
            }
            for (int i = 0; i < lista_noticias.Count; i++)
            {
                if(lista_noticias[i].Id == noticias.Id)
                {
                    lista_noticias[i].Id_Topico = noticias.Id_Topico;
                    lista_noticias[i].Titulo = noticias.Titulo;
                    lista_noticias[i].Descricao = noticias.Descricao;
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult delete(int Id)
        {
            List<Noticias> lista_noticias = new List<Noticias>();
            if (Session["Noticias"] != null)
            {
                lista_noticias = (List<Noticias>)Session["Noticias"];
            }

            for (int i = 0; i < lista_noticias.Count; i++)
            {
                if (lista_noticias[i].Id == Id)
                {
                    lista_noticias.RemoveAt(i);
                }
            }
            Session["Noticias"] = lista_noticias;
            return RedirectToAction("Index");
        }
    }
}