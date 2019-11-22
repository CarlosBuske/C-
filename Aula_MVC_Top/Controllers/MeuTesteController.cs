using Aula_MVC_Top.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aula_MVC_Top.Controllers
{
    public class MeuTesteController : Controller
    {

        FACEAR_2019Entities2 db = new FACEAR_2019Entities2();
        // GET: MeuTeste
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Adicionar_Aluno()
        {
            return Json(new
            {
                
            }, JsonRequestBehavior.AllowGet);
        }
    }
}