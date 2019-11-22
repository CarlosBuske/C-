using Meu_Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Meu_Projeto.Controllers
{
    public class LojaController : Controller
    {
        // GET: Loja
        public ActionResult Index()
        {
            if (Session["Produtos"]!= null )
            {

            }
            else {
                IList<Produtos> lista = new List<Produtos>();
                Produtos p1 = new Produtos();
                p1.Id = 1;
                p1.Nome = "Notebook Gamer";
                p1.Descricao = "4gb Ram";
                p1.Valor = 400;
                p1.Tipo = "Notebook";
                p1.Marca = "Dell";
                p1.Modelo = "Slin";
                lista.Add(p1);

                Produtos p2 = new Produtos();
                p2.Id = 2;
                p2.Nome = "IPad";
                p2.Descricao = "Ipad Apple";
                p2.Valor = 250;
                p2.Tipo = "Tablet";
                p2.Marca = "Apple";
                p2.Modelo = "IPad Air";
                lista.Add(p2);

                Produtos p3 = new Produtos();
                p3.Id = 3;
                p3.Nome = "Xiaomi MiA3";
                p3.Descricao = "4gb ram 64gb memoria";
                p3.Valor = 20;
                p3.Tipo = "Celular";
                p3.Marca = "Xiaomi";
                p3.Modelo = "MiA3";
                lista.Add(p3);

                Produtos p4 = new Produtos();
                p4.Id = 4;
                p4.Nome = "Carregador Samsumg";
                p4.Descricao = "Carregador not Samsumg";
                p4.Valor = 50;
                p4.Tipo = "Carregadores";
                p4.Marca = "Samsumg Max";
                p4.Modelo = "Carregador 30w";
                lista.Add(p4);

                Session["Produtos"] = lista;

            }
            

            if (Session["Logado"] != null)
            {
                //ViewBag.Teste = Session["Logado"];
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {

            
            return RedirectToAction("Login", "Login");
        }

        public ActionResult CadastrarProduto()
        {
            if (Session["Logado"] != null)
            {
                //ViewBag.Teste = Session["Logado"];
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
        }
        [HttpPost]
        public ActionResult CadastrarProduto(Produtos produtos)
        {
            if (ModelState.IsValid)
            {
                int aux = 0;
                List<Produtos> lista = new List<Produtos>();
                if (Session["Produtos"] != null)
                {
                    lista = (List<Produtos>)Session["Produtos"];
                }
                for (int i = 0; i < lista.Count; i++)
                {
                    if (lista[i].Id == produtos.Id)
                    {
                        ModelState.AddModelError("", "Id Já existente");
                        return View();
                    }
                }
                if (aux == 0)
                {

                    Produtos p1 = new Produtos();
                    p1.Id = produtos.Id;
                    p1.Nome = produtos.Nome;
                    p1.Descricao = produtos.Descricao;
                    p1.Valor = produtos.Valor;
                    p1.Tipo = produtos.Tipo;
                    p1.Marca = produtos.Marca;
                    p1.Modelo = produtos.Modelo;
                    lista.Add(p1);

                    Session["Produtos"] = lista;
                    return View();

                }
                else
                {
                    return View();
                }
            }
            return View();



        }

        public ActionResult Listar()
        {
            if (Session["Logado"] != null)
            {
                IList<Produtos> lista = new List<Produtos>();
                if (Session["Produtos"] != null)
                {
                    lista = (List<Produtos>)Session["Produtos"];
                }

                return View(lista);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
        }

        public ActionResult Editar(int id)
        {
            if (Session["Logado"] != null)
            {
                //ViewBag.Teste = Session["Logado"];
                    List<Produtos> lista = new List<Produtos>();
                    if (Session["Produtos"] != null)
                    {
                    lista = (List<Produtos>)Session["Produtos"];
                    }
                    var p = lista.Find(x => x.Id == id);
                    return View(p);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
        }
        [HttpPost]
        public ActionResult Editar(Produtos produtos)
        {
            IList<Produtos> lista = new List<Produtos>();
            if (Session["Produtos"] != null)
            {
                lista = (List<Produtos>)Session["Produtos"];
            }
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Id == produtos.Id)
                {
                    lista[i].Nome = produtos.Nome;
                    lista[i].Descricao = produtos.Descricao;
                    lista[i].Valor = produtos.Valor;
                    lista[i].Tipo = produtos.Tipo;
                    lista[i].Marca = produtos.Marca;
                    lista[i].Modelo = produtos.Modelo;
                }
            }
            Session["Produtos"] = lista;
            Response.Write("<script>alert('Atualizado');</script>");
            return RedirectToAction("Listar");

            
        }

        public ActionResult Delete(int id)
        {
            IList<Produtos> lista = new List<Produtos>();
            if (Session["Produtos"] != null)
            {
                lista = (List<Produtos>)Session["Produtos"];
            }
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Id == id)
                {
                    lista.RemoveAt(i);
                    
                }
            }
            Session["Produtos"] = lista;
            return RedirectToAction("Listar");
        }

    }
}