using Meu_Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Meu_Projeto.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProdutosUser()
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

        public ActionResult Meu_Carrinho()
        {
            int aux = 0;
            if (Session["Logado"] != null)
            {
                IList<Produtos> lista_MP = new List<Produtos>();
                if (Session["Meus_Produtos"] != null)
                {
                    lista_MP = (List<Produtos>)Session["Meus_Produtos"];
                }
                for (int i = 0; i < lista_MP.Count; i++)
                {
                    aux = aux + lista_MP[i].Valor;
                    
                }
                ViewBag.Total = aux;
                return View(lista_MP);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult AdicionarCarrinho(int id)
        {
            if (Session["Logado"] != null)
            {
                List<Produtos> lista_MP = new List<Produtos>();
                if (Session["Meus_Produtos"] != null)
                {
                    lista_MP = (List<Produtos>)Session["Meus_Produtos"];
                }
                List<Produtos> lista = new List<Produtos>();
                if (Session["Produtos"] != null)
                {
                    lista = (List<Produtos>)Session["Produtos"];
                }
                for (int i = 0; i < lista_MP.Count; i++)
                {
                    if (lista_MP[i].Id == id)
                    {
                        ModelState.AddModelError("", "Produto ja adicionado");
                        return RedirectToAction("ProdutosUser");
                    }
                }
                for (int i = 0; i < lista.Count; i++)
                {
                    if(lista[i].Id == id)
                    {
                        Produtos p = new Produtos();
                        p.Id = lista[i].Id;
                        p.Nome = lista[i].Nome;
                        p.Descricao = lista[i].Descricao;
                        p.Valor = lista[i].Valor;
                        p.Tipo = lista[i].Tipo;
                        p.Marca = lista[i].Marca;
                        p.Modelo = lista[i].Modelo;
                        lista_MP.Add(p);
                        Session["Meus_Produtos"] = lista_MP;
                        return RedirectToAction("ProdutosUser");
                    }
                }
               
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        public ActionResult RemoverCarrinho(int id)
        {
            if (Session["Logado"] != null)
            {
                List<Produtos> lista_MP = new List<Produtos>();
                if (Session["Meus_Produtos"] != null)
                {
                    lista_MP = (List<Produtos>)Session["Meus_Produtos"];
                }
                
                for (int i = 0; i < lista_MP.Count; i++)
                {
                    if (lista_MP[i].Id == id)
                    {
                        lista_MP.RemoveAt(i);
                        Session["Meus_Produtos"] = lista_MP;
                        return RedirectToAction("Meu_Carrinho");
                    }
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        public ActionResult FinalizarCompra()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FinalizarCompra(FormCollection form)
        {
            Session.Remove("Meus_Produtos");
            return RedirectToAction("Index", "Loja");
        }

        public ActionResult ListaCelular()
        {
            if (Session["Logado"] != null)
            {
                IList<Produtos> lista = new List<Produtos>();
                IList<Produtos> listaCelulares = new List<Produtos>();
                if (Session["Produtos"] != null)
                {
                    lista = (List<Produtos>)Session["Produtos"];
                }
                for (int i = 0; i < lista.Count; i++)
                {
                    if (lista[i].Tipo.Equals("Celular"))
                    {
                        Produtos p = new Produtos();
                        p.Id = lista[i].Id;
                        p.Nome = lista[i].Nome;
                        p.Descricao = lista[i].Descricao;
                        p.Valor = lista[i].Valor;
                        p.Tipo = lista[i].Tipo;
                        p.Marca = lista[i].Marca;
                        p.Modelo = lista[i].Modelo;
                        listaCelulares.Add(p);
                    }
                }

                return View(listaCelulares);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult ListaTablet()
        {
            if (Session["Logado"] != null)
            {
                IList<Produtos> lista = new List<Produtos>();
                IList<Produtos> listaTablet = new List<Produtos>();
                if (Session["Produtos"] != null)
                {
                    lista = (List<Produtos>)Session["Produtos"];
                }
                for (int i = 0; i < lista.Count; i++)
                {
                    if (lista[i].Tipo.Equals("Tablet"))
                    {
                        Produtos p = new Produtos();
                        p.Id = lista[i].Id;
                        p.Nome = lista[i].Nome;
                        p.Descricao = lista[i].Descricao;
                        p.Valor = lista[i].Valor;
                        p.Tipo = lista[i].Tipo;
                        p.Marca = lista[i].Marca;
                        p.Modelo = lista[i].Modelo;
                        listaTablet.Add(p);
                    }
                }

                return View(listaTablet);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult ListaNotebook()
        {
            if (Session["Logado"] != null)
            {
                IList<Produtos> lista = new List<Produtos>();
                IList<Produtos> listaNotebook = new List<Produtos>();
                if (Session["Produtos"] != null)
                {
                    lista = (List<Produtos>)Session["Produtos"];
                }
                for (int i = 0; i < lista.Count; i++)
                {
                    if (lista[i].Tipo.Equals("Notebook"))
                    {
                        Produtos p = new Produtos();
                        p.Id = lista[i].Id;
                        p.Nome = lista[i].Nome;
                        p.Descricao = lista[i].Descricao;
                        p.Valor = lista[i].Valor;
                        p.Tipo = lista[i].Tipo;
                        p.Marca = lista[i].Marca;
                        p.Modelo = lista[i].Modelo;
                        listaNotebook.Add(p);
                    }
                }

                return View(listaNotebook);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult ListaCarregadores()
        {
            if (Session["Logado"] != null)
            {
                IList<Produtos> lista = new List<Produtos>();
                IList<Produtos> listaCarregadores = new List<Produtos>();
                if (Session["Produtos"] != null)
                {
                    lista = (List<Produtos>)Session["Produtos"];
                }
                for (int i = 0; i < lista.Count; i++)
                {
                    if (lista[i].Tipo.Equals("Carregadores"))
                    {
                        Produtos p = new Produtos();
                        p.Id = lista[i].Id;
                        p.Nome = lista[i].Nome;
                        p.Descricao = lista[i].Descricao;
                        p.Valor = lista[i].Valor;
                        p.Tipo = lista[i].Tipo;
                        p.Marca = lista[i].Marca;
                        p.Modelo = lista[i].Modelo;
                        listaCarregadores.Add(p);
                    }
                }

                return View(listaCarregadores);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult Detalhes(int id)
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
    }
}