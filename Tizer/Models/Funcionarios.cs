//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tizer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Funcionarios
    {
        public int Id_Funcionario { get; set; }
        public Nullable<int> Id_Empresa { get; set; }
        public string NomeEmpresa { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public Nullable<decimal> Salario { get; set; }
        public string DataAdmissao { get; set; }
        public string DataDemissao { get; set; }
        public string Status { get; set; }
    
        public virtual Empresa Empresa { get; set; }
    }
}