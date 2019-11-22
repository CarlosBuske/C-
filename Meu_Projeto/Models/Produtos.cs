using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Meu_Projeto.Models
{
    public class Produtos
    {
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "O Sobrenome não pode ter mais que 10 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "O Sobrenome não pode ter mais que 10 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public int Valor { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "O Sobrenome não pode ter mais que 10 caracteres")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "O Sobrenome não pode ter mais que 10 caracteres")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "O Sobrenome não pode ter mais que 10 caracteres")]
        public string Modelo { get; set; }

    }
}