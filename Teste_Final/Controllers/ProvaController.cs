using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Teste_Final.Models;

namespace Teste_Final.Controllers
{
    public class ProvaController : Controller
    {
        // GET: Prova
        AgoraVaiEntities db = new AgoraVaiEntities();
        public ActionResult List_Pessoas()
        {
            return View();
        }

        public JsonResult TrazLista()
        {
            return Json(new
            {
                Tabela = criaTabela()
            }, JsonRequestBehavior.AllowGet);
        }

        public string criaTabela()
        {
            IList<Pessoa> Lista = new List<Pessoa>();
            Lista = db.Pessoa.OrderBy(x => x.Nome).ToList();

            StringBuilder str = new StringBuilder();

            str.Append("<table  width=\"100%\" border=\"1\">");
            str.Append("<tr><th>Id</th><th>Nome</th><th>Add Nota</th></tr>");
            foreach (var item in Lista)
            {
                str.Append("<tr><td>" + item.Id_Pessoa + "</td><td>" + item.Nome + "</td><td> <input type=button id=btn_Add_Nota name=btn_Add_Nota onclick=Add_Nota(" + item.Id_Pessoa + ") value = Adicionar Nota /> </td></tr>");
            }
            str.Append("</table>");

            return str.ToString();
        }

        public JsonResult SetIdSessionCliente(int id)
        {
            Session["Cliente"] = id;
            return Json(new
            {
            }, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Cad_Pessoa()
        {
            return View();
        }

        public JsonResult CadastrarPessoa(FormCollection form)
        {



            Pessoa p = new Pessoa();
            p.Nome = form["txtNomePessoa"];

            db.Pessoa.Add(p);
            db.SaveChanges();

            return Json(new
            {
            }, JsonRequestBehavior.AllowGet);
        }

        //---------------------metodos da pessoa--------------------\\

        public ActionResult Cad_Produto()
        {
            return View();
        }
        
        public JsonResult TrazListaProduto()
        {
            return Json(new
            {
                Tabela = criaTabelaProduto()
            }, JsonRequestBehavior.AllowGet);
        }
        
        public string criaTabelaProduto()
        {
            IList<Produto> Lista = new List<Produto>();
            Lista = db.Produto.OrderBy(x => x.Descricao).ToList();

            StringBuilder str = new StringBuilder();

            str.Append("<table  width=\"100%\" border=\"1\">");
            str.Append("<tr><th>Id</th><th>Descrição</th><th>Valor</th></tr>");
            foreach (var item in Lista)
            {
                str.Append("<tr><td>" + item.Id_Produto + "</td><td>" + item.Descricao + "</td><td>" + item.valor + "</td></tr>");
            }
            str.Append("</table>");

            return str.ToString();
        }

        public JsonResult CadastrarProduto(FormCollection form)
        {



            Produto p = new Produto();
            p.Descricao = form["txtNomeProduto"];
            p.valor = Convert.ToInt32(form["txtValor"]);

            db.Produto.Add(p);
            db.SaveChanges();

            return Json(new
            {
            }, JsonRequestBehavior.AllowGet);
        }

        //---------------------metodos do produto--------------------\\


        public ActionResult Nota()
        {
            return View();
        }

        public JsonResult PopulaCamposNota()
        {
            int auxid = (int)Session["Cliente"];
            Pessoa n = new Pessoa();
            n = db.Pessoa.Find(auxid);

            string auxNome = n.Nome;
            return Json(new
            {
                auxNome,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TrazProdutos()
        {
            return Json(new
            {
                Produtos = criaTabelaProdutos()
            }, JsonRequestBehavior.AllowGet);
        }

        public string criaTabelaProdutos()
        {
            IList<Produto> Lista = new List<Produto>();
            Lista = db.Produto.OrderBy(x => x.Descricao).ToList();

            StringBuilder str = new StringBuilder();

            str.Append("<select id=\"Produtos\" name=\"Produtos\" onchange=\"PopulaCamposProdutos()\">");
            str.Append("<option value =\"0\" > Todos </option>");
            for (int i = 0; i < Lista.Count; i++)
            {
                str.Append("<option value=" + Lista[i].Id_Produto + ">" + Lista[i].Descricao + "</option>");

            }
            str.Append("</select>");
            str.Append("<label for=\"Descricao\" id=\"lblDescricao\"> Descrição: </label>");
            str.Append("<input type=\"text\" id=\"txtDescricao\" name=\"txtDescricao\" readonly />");


            str.Append("<label for=\"ValorU\" id=\"ValorU\"> Valor Unitario: </label>");
            str.Append("<input type=\"text\" id=\"txtValorU\" name=\"txtValorU\" onchange=\"Soma()\" />");

            str.Append("<label for=\"Quantidade\" id=\"Quantidade\"> Quantidade: </label>");
            str.Append("<input type=\"text\" id=\"txtQuantidade\" name=\"txtQuantidade\" onchange=\"Soma()\" />");

            str.Append("<label for=\"ValorT\" id=\"ValorT\"> Valor Total: </label>");
            str.Append("<input type=\"text\" id=\"txtValorPT\" name=\"txtValorPT\" readonly />");

            str.Append("<input type=\"button\" id=\"btnAddItem\" name=\"btnAddItem\" value=\"Adicionar\" onclick=\"AdicionarItemNota($('#Produtos').val())\" />");


            return str.ToString();
        }

        public JsonResult AdicionarItemNota(int id, string Nome, int ValorU, int Quantidade, int ValorT)
        {
            int auxValor = 0;
            IList<Itens_Nota> lista = new List<Itens_Nota>();

            if (!(Session["ItensNota"] == null))
            {
                lista = (List<Itens_Nota>)Session["ItensNota"];
            }

            Itens_Nota i = new Itens_Nota();
            i.Id_Produto = id;
            i.Nome = Nome;
            i.ValorUnitario = ValorU;
            i.Quantidade = Quantidade;
            i.ValorTotar = ValorT;

            lista.Add(i);
            StringBuilder str = new StringBuilder();

            str.Append("<table class=\"table table-bordered table-striped\">");
            str.Append("<tr><td>Cod</td><td>Nome</td><td>Valor Unitario</td><td>Quantidade</td><td>Valor Total</td></tr>");
            foreach (var item in lista)
            {
                str.Append("<tr><td>" + item.Id_Produto + "</td><td>" + item.Nome + "</td><td>" + item.ValorUnitario + "</td><td>" + item.Quantidade + "</td><td>" + item.ValorTotar + "</td></tr>");
                auxValor += (int)item.ValorTotar;
            }

            str.Append("</table>");


            Session["ItensNota"] = lista;
            return Json(new
            {
                TblItens = str.ToString(),
                auxValor
            }, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult PopulaCamposProdutos(int id)
        {
            Produto p = new Produto();
            p = db.Produto.Find(id);

            string auxDescricao = p.Descricao;
            int auxValorU = (int)p.valor;
            return Json(new
            {
                auxDescricao,
                auxValorU
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Salvar(FormCollection form)
        {
            int id = (int)Session["Cliente"];
            DateTime data = DateTime.Now;
            Nota n = new Nota();
            n.Data = data.ToString("yyyy-MM-dd");
            n.valorT = Convert.ToInt32(form["txtValorT"]);
            n.Nome_Cliente = form["txtCliente"];
            n.Id_Pessoa = id;

            IList<Itens_Nota> lista = new List<Itens_Nota>();
            if (Session["ItensNota"] != null)
            {
                lista = (List<Itens_Nota>)Session["ItensNota"];
            }
            n.Itens_Nota = lista;
            db.Nota.Add(n);
            db.SaveChanges();
            Session.Remove("ItensNota");




            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        //---------------------metodos da tela Nota--------------------\\
        
        public ActionResult Lista_Nota()
        {
            return View();
        }

        public JsonResult TrazListaNotas()
        {
            return Json(new
            {
                TabelaNota = criaTabelaNotas()
            }, JsonRequestBehavior.AllowGet);
        }

        public string criaTabelaNotas()
        {
            IList<Nota> Lista = new List<Nota>();
            Lista = db.Nota.OrderBy(x => x.Nome_Cliente).ToList();

            StringBuilder str = new StringBuilder();

            str.Append("<table  width=\"100%\" border=\"1\">");
            str.Append("<tr><th>Id</th><th>Data</th><th>Valor Total</th><th>Nome Cliente</th><th>Detalhes</th></tr>");
            foreach (var item in Lista)
            {
                str.Append("<tr><td>" + item.Id_Nota + "</td><td>" + item.Data + "</td><td>" + item.valorT + "</td><td>" + item.Nome_Cliente + "</td><td> <input type=button id=btn_Detalhes name=btn_Detalhes onclick=detalhes(" + item.Id_Nota + ") value = Detalhes /> </td></tr>");
            }
            str.Append("</table>");

            return str.ToString();
        }
        
        public JsonResult SetIdSessionNota(int id)
        {
            Session["Nota"] = id;
            return Json(new
            {
            }, JsonRequestBehavior.AllowGet);
        }

        //---------------------metodos da tela Lista de Notas--------------------\\
        
        public ActionResult DetalhesNota()
        {
            return View();
        }

        public JsonResult PopulaNota()
        {
            int auxid = (int)Session["Nota"];
            Nota n = new Nota();
            n = db.Nota.Find(auxid);

            string auxNome = n.Nome_Cliente;
            int auxValorT = (int)n.valorT;
            string Data = n.Data;
            return Json(new
            {
                auxNome,
                auxValorT,
                Data
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Cria_Lista_Nota()
        {
            return Json(new
            {
                TabelaNota = criaTabelaItensNaNota()
            }, JsonRequestBehavior.AllowGet);
        }

        public string criaTabelaItensNaNota()
        {
            int auxId = (int)Session["Nota"];
            IList<Itens_Nota> Lista = new List<Itens_Nota>();
            Lista = db.Itens_Nota.Where(x => x.Id_Nota == auxId).ToList();

            StringBuilder str = new StringBuilder();

            str.Append("<table  width=\"100%\" border=\"1\">");
            str.Append("<tr><th>Id</th><th>Nome</th><th>Valor Unitario </th><th>Quantidade</th><th>Valor Unitario Total</th></tr>");
            foreach (var item in Lista)
            {
                str.Append("<tr><td>" + item.Id_ItensNota + "</td><td>" + item.Nome + "</td><td>" + item.ValorUnitario + "</td><td>" + item.Quantidade + "</td><td>" + item.ValorTotar + "</td></tr>");
            }
            str.Append("</table>");

            return str.ToString();
        }

        //---------------------metodos da tela Detalhes de Notas--------------------\\
    }
}