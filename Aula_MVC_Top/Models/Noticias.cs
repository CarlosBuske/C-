using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aula_MVC_Top.Models
{
    public class Noticias
    {
        public int Id { get; set; }

        public int Id_Topico { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }
    }
}