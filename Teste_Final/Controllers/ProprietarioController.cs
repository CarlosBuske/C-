using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Teste_Final.Models;

namespace Teste_Final.Controllers
{
    public class ProprietarioController : Controller
    {
        // GET: Proprietario
        EstudoProva1Entities db = new EstudoProva1Entities();
        public ActionResult Index()
        {
           
            return View();
        }

        public JsonResult Lista_Tudo_Proprietario(FormCollection form)
        {

            bool ativo;
            if (form["cbxAtivoProprietario"] != null)
            {
                ativo = true;
            }
            else
            {
                ativo = false;
            }




            string nome = form["txtNomeProprietario"];
            string cpf = form["txtCpfProprietario"];
            string sexo = form["selectSexoProprietario"];


            //var lista = db.Proprietario.ToList().OrderBy(x => x.Nome);
            IList<Proprietario> Lista_Proprietario = new List<Proprietario>();
            if (sexo == "T")
            {
                Lista_Proprietario = db.Proprietario.Where(x => x.Nome.Contains(nome) && x.Cpf.Contains(cpf) && (x.Ativo == ativo)).ToList();
            }
            else
            {
                Lista_Proprietario = db.Proprietario.Where(x => x.Nome.Contains(nome) && x.Cpf.Contains(cpf) && x.Sexo.Contains(sexo) && (x.Ativo == ativo)).ToList();
            }


            IList<Proprietario> Lista_Proprietario_Aux = new List<Proprietario>();

            //Lista_Alunos = (IList<Alunos>) lista;
            foreach (var item in Lista_Proprietario)
            {
                Proprietario p = new Proprietario();
                p.Id_Proprietario = item.Id_Proprietario;
                p.Nome = item.Nome;
                p.Cpf = item.Cpf;
                p.Sexo = item.Sexo;
                p.Ativo = item.Ativo;

                Lista_Proprietario_Aux.Add(p);
            }

            return Json(new
            {
                Lista_Proprietario_Aux
            }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Create()
        {

            return View();
        }

        public JsonResult CadastrarProprietario(FormCollection form)
        {
            bool ativo;
            if (form["cbxAtivoProprietario"] != null)
            {
                ativo = true;
            }
            else
            {
                ativo = false;
            }


            Proprietario p = new Proprietario();
            p.Nome = form["txtNomeProprietario"];
            p.Cpf = form["txtCpfProprietario"];
            p.Sexo = form["selectSexoProprietario"];
            p.Ativo = ativo;

            db.Proprietario.Add(p);
            db.SaveChanges();

            return Json(new
            {
            }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Edit()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Index", "Proprietario");
            }
            return View();

        }

        
        public JsonResult SetSession(int id)
        {
            Session["Id"] = id;
            return Json(new
            {}, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult Lista_Veiculos()
        {
            int id = (int)Session["Id"];
            var lista = db.Carro.Where(x => x.Id_Proprietario == id);
            IList<Carro> Lista_Carro = new List<Carro>();

            //Lista_Alunos = (IList<Alunos>) lista;
            foreach (var item in lista)
            {
                Carro c = new Carro();
                c.Id_Carro = item.Id_Carro;
                c.Id_Proprietario = item.Id_Proprietario;
                c.Modelo = item.Modelo;
                c.Montadora = item.Montadora;
                c.Placa = item.Placa;
                c.Ano = item.Ano;
                c.Cor = item.Cor;


                Lista_Carro.Add(c);
            }

            return Json(new
            {
                Lista_Carro
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AdicionarCarroTemp(FormCollection form)
        {
            IList<Carro> listaDeCarro = new List<Carro>();
            Random rand = new Random();
            if (!(Session["Carros"] == null))
            {
                listaDeCarro = (List<Carro>)Session["Carros"];
            }
            int idAux = (int)Session["Id"];
            Carro c = new Carro();
            c.Id_Carro = rand.Next();
            c.Montadora = form["txtMontadora"];
            c.Modelo = form["txtModelo"];
            c.Ano = form["txtAno"];
            c.Cor = form["txtCor"];
            c.Placa = form["txtPlaca"];
            c.Id_Proprietario = (int)Session["Id"];
            listaDeCarro.Add(c);
            StringBuilder str = new StringBuilder();

            str.Append("<table class=\"table table-bordered table-striped\">");
            str.Append("<tr><td>Montador</td><td>Modelo</td><td>Ano</td><td>Cor</td><td>Placa</td><td>Editar</td><td>Excluir</td></tr>");

            foreach (var item in listaDeCarro)
            {
                str.Append("<tr><td>" + item.Montadora + "</td><td>" + item.Modelo + "</td><td>" + item.Ano + "</td><td>" + item.Cor + "</td><td>" + item.Placa + "</td><td><input type=\"button\" id=\"btnEditCarTemp\" value=\"Editar\" onclick=\"SetCamposCarTemp(" + item.Id_Carro + ")\"/></td><td><input type=\"button\" id=\"btnDeleteCarTemp\" value=\"Deletar\" onclick=\"DeleteCarTemp(" + item.Id_Carro + ")\"/></td></tr>");
            }

            str.Append("</table>");
            Session["Carros"] = listaDeCarro;
            return Json(new
            {
                ListaCarro = str.ToString()
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditCarTemp(int id, string Montadora, string Modelo, string Ano, string Cor)
        {
            IList<Carro> listaDeCarro = new List<Carro>();
            if (!(Session["Carros"] == null))
            {
                listaDeCarro = (List<Carro>)Session["Carros"];
            }

            for (int i = 0; i < listaDeCarro.Count; i++)
            {
                if (listaDeCarro[i].Id_Carro == id)
                {
                    listaDeCarro[i].Montadora = Montadora;
                    listaDeCarro[i].Modelo = Modelo;
                    listaDeCarro[i].Ano = Ano;
                    listaDeCarro[i].Cor = Cor;
                    listaDeCarro[i].Id_Proprietario = (int)Session["id"];
                }
            }
            
            StringBuilder str = new StringBuilder();

            str.Append("<table class=\"table table-bordered table-striped\">");
            str.Append("<tr><td>Montador</td><td>Modelo</td><td>Ano</td><td>Cor</td><td>Placa</td><td>Editar</td><td>Excluir</td></tr>");

            foreach (var item in listaDeCarro)
            {
                str.Append("<tr><td>" + item.Montadora + "</td><td>" + item.Modelo + "</td><td>" + item.Ano + "</td><td>" + item.Cor + "</td><td>" + item.Placa + "</td><td><input type=\"button\" id=\"btnEditCarTemp\" value=\"Editar\" onclick=\"SetCamposCarTemp(" + item.Id_Carro + ")\"/></td><td><input type=\"button\" id=\"btnDeleteCarTemp\" value=\"Deletar\" onclick=\"DeleteCarTemp(" + item.Id_Carro + ")\"/></td></tr>");
            }

            str.Append("</table>");
            Session["Carros"] = listaDeCarro;
            return Json(new
            {
                ListaCarro = str.ToString()
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetCamposCarTemp(int id)
        {
            int auxIdCarro = 0;
            string auxMontadora = "";
            string auxModelo ="";
            string auxAno= "";
            string auxCor ="";
            string auxPlaca = "";
;            IList<Carro> listaDeCarro = new List<Carro>();
            if (!(Session["Carros"] == null))
            {
                listaDeCarro = (List<Carro>)Session["Carros"];
            }

            for (int i = 0; i < listaDeCarro.Count; i++)
            {
                if (listaDeCarro[i].Id_Carro == id)
                {
                     auxIdCarro = listaDeCarro[i].Id_Carro;
                     auxMontadora = listaDeCarro[i].Montadora;
                     auxModelo = listaDeCarro[i].Modelo;
                     auxAno =  listaDeCarro[i].Ano;
                     auxCor = listaDeCarro[i].Cor;
                     auxPlaca = listaDeCarro[i].Placa;
                }
            }


            return Json(new
            {
                auxMontadora, auxModelo, auxAno, auxCor, auxPlaca, auxIdCarro
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteCarTemp(int id)
        {
            IList<Carro> listaDeCarro = new List<Carro>();
            if (!(Session["Carros"] == null))
            {
                listaDeCarro = (List<Carro>)Session["Carros"];
            }

            for (int i = 0; i < listaDeCarro.Count; i++)
            {
                if (listaDeCarro[i].Id_Carro == id)
                {
                    listaDeCarro.RemoveAt(i);
                }
            }
            StringBuilder str = new StringBuilder();

            str.Append("<table class=\"table table-bordered table-striped\">");
            str.Append("<tr><td>Montador</td><td>Modelo</td><td>Ano</td><td>Cor</td><td>Placa</td><td>Editar</td><td>Excluir</td></tr>");

            foreach (var item in listaDeCarro)
            {
                str.Append("<tr><td>" + item.Montadora + "</td><td>" + item.Modelo + "</td><td>" + item.Ano + "</td><td>" + item.Cor + "</td><td>" + item.Placa + "</td><td><input type=\"button\" id=\"btnEditCarTemp\" value=\"Editar\" onclick=\"SetCamposCarTemp(" + item.Id_Carro + ")\"/></td><td><input type=\"button\" id=\"btnDeleteCarTemp\" value=\"Deletar\" onclick=\"DeleteCarTemp(" + item.Id_Carro + ")\"/></td></tr>");
            }

            str.Append("</table>");
            Session["Carros"] = listaDeCarro;

            return Json(new
            {
                ListaCarro = str.ToString()
            }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Remover_Veiculos(int id)
        {
            var c = db.Carro.FirstOrDefault(x => x.Id_Carro == id);
            db.Entry(c).State = EntityState.Deleted;
            db.SaveChanges();

            return Json(new
            {
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Traz_Dados_Veiculo(int id)
        {
            var c = db.Carro.FirstOrDefault(x => x.Id_Carro == id);
            int auxId = c.Id_Carro;
            string auxMontadora = c.Montadora;
            string auxModelo = c.Modelo;
            string auxPlaca = c.Placa;
            string auxCor = c.Cor;
            string auxAno = c.Ano;

            return Json(new
            {
                auxId, auxMontadora, auxModelo, auxPlaca, auxCor, auxAno
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Alterar_Veiculo(int id, string Montadora, string Modelo, string Placa, string Cor, string Ano)
        {

            Carro c = new Carro();
            c = db.Carro.Find(id);
            c.Montadora = Montadora;
            c.Modelo = Modelo;
            c.Placa = Placa;
            c.Cor = Cor;
            c.Ano = Ano;
            db.SaveChanges();
            return Json(new
            {
                
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Popula_Campos(int id)
        {


            var P = db.Proprietario.FirstOrDefault(x => x.Id_Proprietario == id); ;
            string auxNome = P.Nome;
            string auxCpf = P.Cpf;
            string auxSexo = P.Sexo;
            bool auxAtivo = Convert.ToBoolean(P.Ativo);
            

            return Json(new
            {
                auxNome,
                auxCpf,
                auxSexo,
                auxAtivo
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            Proprietario p = new Proprietario();
            p = db.Proprietario.Find(id);
            p.Ativo = false;
            db.SaveChanges();
            return Json(new
            {
                
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AtualizarProprietario(FormCollection form)
        {
            int id = (int)Session["Id"];
            Proprietario p = new Proprietario();
            p = db.Proprietario.Find(id);
            bool ativo;
            if (form["cbxAtivoProprietario"] != null)
                ativo = true;
            else
                ativo = false;

            p.Nome = form["txtNomeProprietario"];
            p.Cpf = form["txtCpfProprietario"];
            p.Sexo = form["selectSexoProprietario"];
            p.Ativo = ativo;

            if (Session["Carros"] != null)
            {
                IList<Carro> listaC = new List<Carro>();
                listaC = (List<Carro>)Session["Carros"];
                p.Carro = listaC;
                db.SaveChanges();
                Session.Remove("Carros");
            }
            else
            {
                db.SaveChanges();
            }



            
            return Json(new{}, JsonRequestBehavior.AllowGet);
        }



    }
}