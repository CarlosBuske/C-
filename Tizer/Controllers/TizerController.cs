using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Tizer.Models;

namespace Tizer.Controllers
{
    public class TizerController : Controller
    {
        TizerTIEntities T = new TizerTIEntities();
        // GET: Tizer
        //----------------------Metodos Empresa-----------------------------------------------

        public ActionResult Index()
        {
            Session["Empresa"] = null;
            Session["Funcionario"] = null;
            Session["IdEmpresaFuncionario"] = null;
            return View();
        }

        public ActionResult Cad_Empresa()
        {
            return View();
        }

        public JsonResult CadastrarEmpresa(FormCollection form)
        {
            
            Empresa e = new Empresa();
            e.RazaoSocial = form["txtRazaoSocial"];
            e.Cnpj = form["txtCNPJ"];
            e.Cidade = form["txtCidade"];
            e.UF = form["txtUF"];
            T.Empresa.Add(e);
            T.SaveChanges();

            return Json(new
            {
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListaEmpresas()
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
            IList<Empresa> Lista = new List<Empresa>();
            Lista = T.Empresa.OrderBy(x => x.RazaoSocial).ToList();

            StringBuilder str = new StringBuilder();

            str.Append("<table class=\"table table-bordered table-striped\">");
            str.Append("<tr><th>Razão Social</th><th>CNPJ</th><th>Cidade</th><th>UF</th><th>Editar</th></tr>");
            foreach (var item in Lista)
            {
                str.Append("<tr><td>" + item.RazaoSocial + "</td><td>" + item.Cnpj + "</td><td>" + item.Cidade + "</td><td>" + item.UF + "</td><td> <input type=button id=btn_Alterar name=btn_Alterar onclick=Alterar(" + item.Id_Empresa + ") value = Alterar /> </td></tr>");
            }
            str.Append("</table>");

            return str.ToString();
        }

        public ActionResult AlterarEmpresa()
        {
            if (Session["Empresa"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        
        public JsonResult SetIdSessionEmpresa(int id)
        {
            Session["Empresa"] = id;
            return Json(new
            {
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PopulaCamposEmpresa()
        {
            int auxid = (int)Session["Empresa"];
            Empresa e = new Empresa();
            e = T.Empresa.Find(auxid);

            string auxRazaoSocial = e.RazaoSocial;
            string auxCNPJ = e.Cnpj;
            string auxCidade = e.Cidade;
            string auxUF = e.UF;

            return Json(new
            {
                auxRazaoSocial,
                auxCNPJ,
                auxCidade,
                auxUF
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AtualizarEmpresa(FormCollection form)
        {
            int auxid = (int)Session["Empresa"];
            Empresa e = new Empresa();
            e = T.Empresa.Find(auxid);
            e.RazaoSocial = form["txtRazaoSocial"];
            e.Cnpj = form["txtCNPJ"];
            e.Cidade = form["txtCidade"];
            e.UF = form["txtUF"];
            

            T.SaveChanges();
            Session["Empresa"] = null;
            return Json(new
            {

            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidaCnpj(string cnpj)
        {
            bool auxV;
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                auxV = false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            auxV = cnpj.EndsWith(digito);
            return Json(new
            {
                auxV
            }, JsonRequestBehavior.AllowGet);
        }


        //----------------------Metodos Funcionario-----------------------------------------------

        public ActionResult Cad_Funcionario()
        {
            return View();
        }

        public ActionResult AlterarFuncionario()
        {
            if (Session["Funcionario"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public JsonResult SetIdSession(int id)
        {
            Session["Funcionario"] = id;
            Funcionarios f = new Funcionarios();
            f = T.Funcionarios.Find(id);
            Session["IdEmpresaFuncionario"] = f.Id_Empresa;
            return Json(new
            {
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PopulaCamposFuncionario()
        {
            int auxid = (int)Session["Funcionario"];
            Funcionarios f = new Funcionarios();
            f = T.Funcionarios.Find(auxid);

            string auxNome = f.Nome;
            string auxCpf = f.CPF;
            string auxTelefone = f.Telefone;
            decimal auxSalario = Convert.ToDecimal(f.Salario);
            int auxCbx = (int)f.Id_Empresa;
            string dateAux = f.DataAdmissao;
            string auxStatus = f.Status;
            string dateAuxDemissao = f.DataDemissao;
           
            return Json(new
            {
                auxNome,
                auxCpf,
                auxTelefone,
                auxSalario,
                auxCbx,
                dateAux,
                auxStatus,
                dateAuxDemissao
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListaFuncionarios()
        {
            return View();
        }

        public JsonResult TrazListaFuncionarios()
        {
            return Json(new
            {
                Tabela = criaTabelaFuncionarios()
            }, JsonRequestBehavior.AllowGet);
        }

        public string criaTabelaFuncionarios()
        {
            IList<Funcionarios> Lista = new List<Funcionarios>();
            Lista = T.Funcionarios.OrderBy(x => x.Nome).ToList();

            StringBuilder str = new StringBuilder();

            str.Append("<table class=\"table table-bordered table-striped\">");
            str.Append("<tr><th>Nome </th><th>CPF</th><th>Telefone</th><th>Salario</th><th>Nome Empresa</th><th>Status</th><th>Editar</th></tr>");
            foreach (var item in Lista)
            {
                str.Append("<tr><td>" + item.Nome + "</td><td>" + item.CPF + "</td><td>" + item.Telefone + "</td><td>" + item.Salario + "</td><td>" + item.NomeEmpresa + "</td><td>" + item.Status + "</td><td> <input type=button id=btn_Alterar name=btn_Alterar onclick=Alterar(" + item.Id_Funcionario + ") value = Alterar Funcionario /> </td></tr>");
            }
            str.Append("</table>");

            return str.ToString();
        }

        public JsonResult Pesquisa_Funcionario(string txt_pesquisa)
        {
            var lista = T.Funcionarios.ToList();
            StringBuilder str = new StringBuilder();

            str.Append("<table class=\"table table-bordered table-striped\">");
            str.Append("<tr><th>Nome </th><th>CPF</th><th>Telefone</th><th>Salario</th><th>Nome Empresa</th><th>Status</th><th>Editar</th></tr>");
            foreach (var item in lista)
            {
                if (item.NomeEmpresa.Contains(txt_pesquisa) || item.Status.Contains(txt_pesquisa))
                {

                    str.Append("<tr><td>" + item.Nome + "</td><td>" + item.CPF + "</td><td>" + item.Telefone + "</td><td>" + item.Salario + "</td><td>" + item.NomeEmpresa + "</td><td>" + item.Status + "</td><td> <input type=button id=btn_Alterar name=btn_Alterar onclick=Alterar(" + item.Id_Funcionario + ") value = Alterar Funcionario /> </td></tr>");

                }
            }
            return Json(new
            {
                PesquisaEspecifica = str.ToString()
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CadastrarFuncionario(string nome, string Cpf, int Id_Empresa, string Telefone, decimal Salario, string DataAdmissao)
        {
            var Lista = T.Empresa.ToList();

            Funcionarios f = new Funcionarios();
            foreach (var item in Lista)
            {
                if (item.Id_Empresa == Id_Empresa)
                {
                    f.NomeEmpresa = item.RazaoSocial;
                }
            }
            f.Nome = nome;
            f.CPF = Cpf;
            f.Id_Empresa = Id_Empresa;
            f.Telefone = Telefone;
            f.Salario = Salario;
            f.DataAdmissao = DataAdmissao;
            f.Status = "Trabalhando";
            
            T.Funcionarios.Add(f);
            T.SaveChanges();

            return Json(new
            {
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Alterar_Funcionario(string nome, string Telefone, decimal Salario, int Empresa, string DataDemissaoNova, string DataAdmissaoNova)
        {
            int auxid = (int)Session["Funcionario"];
            Funcionarios f = new Funcionarios();
            f = T.Funcionarios.Find(auxid);
            f.Nome = nome;
            f.Telefone = Telefone;
            f.Salario = Convert.ToDecimal(Salario);
            f.Id_Empresa = Empresa;
            var Lista = T.Empresa.ToList();
            foreach (var item in Lista)
            {
                if (item.Id_Empresa == Empresa)
                {
                    f.NomeEmpresa = item.RazaoSocial;
                }
            }
            if (String.IsNullOrEmpty(DataAdmissaoNova))
            {

            }
            else
            {
                f.DataAdmissao = DataAdmissaoNova;
                f.Status = "Trabalhando";
            }
            if (String.IsNullOrEmpty(DataDemissaoNova))
            {

            }
            else
            {
                f.DataDemissao = DataDemissaoNova;
                f.Status = "Demitido";
            }

            T.SaveChanges();
            Session["Funcionario"] = null;
            return Json(new
            {

            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CompararId(int Empresa)
        {
            bool aux;
            int auxid = (int)Session["Funcionario"];
            Funcionarios f = new Funcionarios();
            f = T.Funcionarios.Find(auxid);
            string auxStatus = f.Status;
            if (Empresa == (int)Session["IdEmpresaFuncionario"])
            {
                aux = false;
            }
            else
            {
                aux = true;
            }
            return Json(new
            {
                aux,
                auxStatus
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TrazCbx()
        {
            return Json(new
            {
                Cbx = criaCbx()
            }, JsonRequestBehavior.AllowGet);
        }

        public string criaCbx()
        {
            IList<Empresa> Lista = new List<Empresa>();
            Lista = T.Empresa.OrderBy(x => x.RazaoSocial).ToList();

            StringBuilder str = new StringBuilder();
            
            foreach (var item in Lista)
            {
                str.Append("<option  value = "+item.Id_Empresa+" > "+item.RazaoSocial+" </ option >");
            }

            return str.ToString();
        }

        public JsonResult ValidaCpf(string cpf)
        {
            bool auxV;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                auxV = false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            auxV = cpf.EndsWith(digito);
            return Json(new
            {
                auxV
            }, JsonRequestBehavior.AllowGet);
        }

    }
}