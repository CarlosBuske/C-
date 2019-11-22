using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Teste_Final.Models;

namespace Teste_Final.Controllers
{
    public class AgendaController : Controller
    {
        // GET: Agenda

        EstudoProva1Entities2 db = new EstudoProva1Entities2();
        public ActionResult Inicial()
        {
            
            return View();
        }

        public JsonResult CadastrarAgenda(FormCollection form)
        {

            DateTime data = DateTime.ParseExact(form["txtData"], "yyyy-MM-dd", CultureInfo.InvariantCulture);
            Agenda a = new Agenda();

            a.Data = data;
            a.Anotacao = form["txtAnotacao"];

            db.Agenda.Add(a);
            db.SaveChanges();

            return Json(new
            {
            }, JsonRequestBehavior.AllowGet);
        }


       

        public JsonResult Traz_Dados_Agenda(int id)
        {
            var a = db.Agenda.FirstOrDefault(x => x.Id_Agenda == id);
            int auxId = a.Id_Agenda;
            string auxData= TransformaData(a.Data.ToString());
            string auxAnotacao = a.Anotacao;

            return Json(new
            {
                auxId,
                auxData,
                auxAnotacao
            }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Remover(int id)
        {
            var a = db.Agenda.FirstOrDefault(x => x.Id_Agenda == id);
            db.Entry(a).State = EntityState.Deleted;
            db.SaveChanges();

            return Json(new
            {
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Alterar_Agenda(FormCollection form)
        {
            DateTime data = DateTime.ParseExact(form["txtData"], "yyyy-MM-dd", CultureInfo.InvariantCulture);
            Agenda a = new Agenda();
            a = db.Agenda.Find(Convert.ToInt32(form["txtId"]));

            a.Data = data;
            a.Anotacao = form["txtAnotacao"]; 

            db.SaveChanges();
            return Json(new
            {

            }, JsonRequestBehavior.AllowGet);
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
            IList<Agenda> Lista = new List<Agenda>();
            Lista = db.Agenda.OrderBy(x => x.Data).ToList();

            StringBuilder str = new StringBuilder();

            str.Append("<table  width=\"100%\" border=\"1\">");
            str.Append("<tr><th>Data</th><th>Anotação</th><th>Editar</th><th>Excluir</th></tr>");
            foreach (var item in Lista)
            {
                str.Append("<tr><td>" + item.Data.Value.ToShortDateString() + "</td><td>" + item.Anotacao + "</td><td> <input type=button id=btn_Alt_Agenda name=btn_Alt_Agenda onclick=Traz_Dados(" + item.Id_Agenda + ") value = Alterar /> </td><td> <input type=button id=btn_Rem_Agenda name=btn_Rem_Agenda onclick=Delete(" + item.Id_Agenda + ") value = Excluir /> </td></tr>");
            }
            str.Append("</table>");

            return str.ToString();
        }

        public JsonResult Lista_Tudo()
        {

            var lista = db.Agenda.OrderBy(x => x.Data);
            IList<Agenda> Lista = new List<Agenda>();

            //Lista_Alunos = (IList<Alunos>) lista;
            foreach (var item in lista)
            {

                Agenda a = new Agenda();
                a.Id_Agenda = item.Id_Agenda;
                a.Data = Convert.ToDateTime(item.Data);
                a.Anotacao = item.Anotacao;

                Lista.Add(a);
            }



            return Json(new
            {
                Lista
            }, JsonRequestBehavior.AllowGet);
        }

        public string TransformaData (string data)
        {
            string resultado = "";
            string yyyy = "";
            string MM = "";
            string dd = "";
            for (int i = 0; i < data.Length; i++)
            {
                if (i == 0 || i== 1)
                {
                    dd += data[i];
                }
                if (i == 3 || i == 4)
                {
                    MM += data[i];
                }
                if (i >= 6 && i<= 9)
                {
                    yyyy += data[i];
                }
            }
            resultado = yyyy + "-" + MM + "-"+ dd;

            return resultado;
        }
    }
}