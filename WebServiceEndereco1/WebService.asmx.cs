using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Net.Http;
using System.Threading.Tasks;


namespace WebServiceEndereco1
{
    /// <summary>
    /// Descrição resumida de WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Olá, Mundo";
        }

        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BuscarCep(string cep)
        {
            string resultadoJson;
            using(HttpClient client = new HttpClient())
            {
                string url = "https://viacep.com.br/ws/"+cep+"/json/";
                var response = client.GetAsync(url).Result;
                using(HttpContent content = response.Content)
                {
                    Task<string> resultado = content.ReadAsStringAsync();
                    string Jreturn = resultado.Result;
                    resultadoJson = Jreturn;
                }
            }

            HttpContext.Current.Response.Write("[" + resultadoJson + "]");
        }
    }
}
