using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Meu_Projeto.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            if (Request.Cookies["cookieLogin"] != null)
            {
                ViewBag.Login = Request.Cookies["cookieLogin"]["Login"];
                ViewBag.Senha = Request.Cookies["cookieLogin"]["Senha"];
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(string txtLogin, string txtSenha, bool cbxLembrar)
        {
            if (string.IsNullOrEmpty(txtLogin))
            {
                ModelState.AddModelError("", "O Campo é obrigatorio");
            }
            if (string.IsNullOrEmpty(txtSenha))
            {
                ModelState.AddModelError("", "O Campo é obrigatorio");
            }

            if(txtLogin.Equals("Admin") && txtSenha.Equals("123"))
            {

                Session["Logado"] = "Admin";
                if (cbxLembrar == true)
                {
                    HttpCookie cookie = new HttpCookie("cookieLogin");
                    cookie.Values.Add("Login", "Admin");
                    cookie.Values.Add("Senha", "123");
                    cookie.Expires = DateTime.Now.AddDays(5);
                    Response.AppendCookie(cookie);
                }else if(cbxLembrar == false)
                {
                    HttpCookie cookie = new HttpCookie("cookieLogin");
                    cookie.Values.Add("Login", "");
                    cookie.Values.Add("Senha", "");
                    cookie.Expires = DateTime.Now.AddDays(5);
                    Response.AppendCookie(cookie);
                }
                return RedirectToAction("Index", "Loja");

            }else if (txtLogin.Equals("Usuario") && txtSenha.Equals("123"))
            {
                Session["Logado"] = "Usuario";
                if (cbxLembrar == true)
                {
                    HttpCookie cookie = new HttpCookie("cookieLogin");
                    cookie.Values.Add("Login", "Usuario");
                    cookie.Values.Add("Senha", "123");
                    cookie.Expires = DateTime.Now.AddDays(5);
                    Response.AppendCookie(cookie);
                }
                else if (cbxLembrar == false)
                {
                    HttpCookie cookie = new HttpCookie("cookieLogin");
                    cookie.Values.Add("Login", "");
                    cookie.Values.Add("Senha", "");
                    cookie.Expires = DateTime.Now.AddDays(5);
                    Response.AppendCookie(cookie);
                }
                return RedirectToAction("Index", "Loja");
            }
            else
            {
                ModelState.AddModelError("", "Login ou senha inválidos");
                Session.RemoveAll();
                return View();
            }
        }
    }
}