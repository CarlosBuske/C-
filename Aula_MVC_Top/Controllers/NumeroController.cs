using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aula_MVC_Top.Controllers
{
    public class NumeroController : Controller
    {
        
        // GET: Numero
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(int txt_num, string Resultado)
        {
            if(Resultado == "")
            {
                ViewBag.Numero = txt_num;
                if (txt_num % 2 == 0)
                {

                    ViewBag.Menssagem = "<label style=" + "color:green" + ">" + "O Numero " + txt_num + "é Par" + "<label/>";
                    
                }
                else
                {
                    ViewBag.Menssagem = "<label style=" + "color:red" + ">" + "O Numero " + txt_num + "é Impar" + "<label/>";
                }
                ViewData["Resultado"] = txt_num;
            }
            else
            {
                ViewBag.Numero = txt_num;
                int Soma = 0;
                Soma = Convert.ToInt32(Resultado) + txt_num;
                if(Soma % 2 == 0)
                {
                    ViewBag.Menssagem = "<label style=" + "color:green" + ">" + "O Numero " + Soma + "é Par" + "<label/>";
                }
                else
                {
                    ViewBag.Menssagem = "<label style=" + "color:red" + ">" + "O Numero " + Soma + "é Impar" + "<label/>";
                }
                ViewData["Resultado"] = Soma;
            }
            
            


            return View();
        }
    }
}