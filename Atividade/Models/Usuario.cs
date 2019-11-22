using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Atividade.Models
{
    public class Usuario
    {

        public string TipoUsuario { get; set; }

        public string Login { get; set; }
        
        public string Senha { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public string cod_noticia { get; set; }

        [StringLength(300, MinimumLength = 30, ErrorMessage = "A Noticia Tem que ter mais de 30 caracteres e menos de 300")]
        public string Noticia { get; set; }


        [StringLength(30, ErrorMessage = "O Tipo nao pode ter mais que 30 caracteres")]
        [System.Web.Mvc.Remote("ValidaTipo", "Inicial", ErrorMessage = "Tipo de noticia invalido")]
        public string Tipo { get; set; }


        public void CadastrarNoticia(string Tipo, string Noticia)
        {
            
            SqlConnection conec = new SqlConnection(@"Data Source=DESKTOP-TLG3QB4\SQLEXPRESS;Initial Catalog=c#MVC;Integrated Security=True");
            SqlCommand comando = new SqlCommand("Sp_cadastrar_Noticia", conec);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("@Noticia", SqlDbType.VarChar).Value = Noticia;
            comando.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = Tipo;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            conec.Open();
            comando.ExecuteNonQuery();
            conec.Close();
        }
        public void Excluir(int cod)
        {

            SqlConnection conec = new SqlConnection(@"Data Source=DESKTOP-TLG3QB4\SQLEXPRESS;Initial Catalog=c#MVC;Integrated Security=True");
            SqlCommand comando = new SqlCommand("Sp_Excluir", conec);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("@cod", SqlDbType.Int).Value = cod;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            conec.Open();
            comando.ExecuteNonQuery();
            conec.Close();
        }

        public List<Usuario> Busca()
        {
            var listaNoticias = new List<Usuario>();
            SqlConnection conec = new SqlConnection(@"Data Source=DESKTOP-TLG3QB4\SQLEXPRESS;Initial Catalog=c#MVC;Integrated Security=True");
            SqlCommand comando = new SqlCommand("Sp_Listar", conec);
            conec.Open();
            var leitor = comando.ExecuteReader();
            while (leitor.Read())
            {
                Usuario u = new Usuario();
                u.cod_noticia = leitor["cod_noticia"].ToString();
                u.Noticia = leitor["noticia"].ToString();
                u.Tipo = leitor["Tipo"].ToString();
                listaNoticias.Add(u);
            }
            conec.Close();
            return listaNoticias;


        }
    }
}