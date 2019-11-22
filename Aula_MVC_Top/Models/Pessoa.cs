using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aula_MVC_Top.Models
{
    public class Pessoa
    {
        public int Id_Pessoa { get; set; }
        public string Nome { get; set; }
        public int? Idade { get; set; }

        public string Sexo { get; set; }
        public int? Peso { get; set; }

    }
}