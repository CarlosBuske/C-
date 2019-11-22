using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Aula_MVC_Top.Models
{
    public class Vendedor
    {

        //Data Source=DESKTOP-TLG3QB4\SQLEXPRESS;Initial Catalog=c#MVC;Integrated Security=True
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public int cod { get; set; }

        public double salario_fixo { get; set; }

        public string faixa_comissao{ get; set; }
        public char sexo { get; set; }

        public int ativo { get; set; }


        public int Logar()
        {
            int id_aux;
            SqlConnection conec = new SqlConnection(@"Data Source=DESKTOP-TLG3QB4\SQLEXPRESS;Initial Catalog=c#MVC;Integrated Security=True");
            SqlCommand comando = new SqlCommand("Sp_Login", conec);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("@cod", SqlDbType.VarChar).Value = this.cod;
            comando.Parameters.Add("@Senha", SqlDbType.VarChar).Value = this.Senha;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            conec.Open();
            id_aux = Convert.ToInt32(comando.ExecuteScalar());
            conec.Close();
            return id_aux;
        }


    }
}