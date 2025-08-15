using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace tcc_demo.Models
{
    public class Customer
    {
        public string idCustomer { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o nome do cliente")]
        [MaxLength(80, ErrorMessage = "O nome deve conter no maximo 255 caracteres")]
        public string nameCustomer { get; set; }

        [Display(Name = "Número do CPF")]
        [Required(ErrorMessage = "Digite o CPF do cliente")]
        [MaxLength(11, ErrorMessage = "O CPF deve conter no maximo 11 caracteres")]
        [MinLength(11, ErrorMessage = "O CPF deve conter no minimo 11 caracters")]
        //[Remote("SelectCPFDoCliente", "Cliente", ErrorMessage = "O CPF já está cadastrado")]
        public string cpfCustomer { get; set; }

        [Display(Name = "Email do Cliente")]
        [Required(ErrorMessage = "Informe o  email do cliente")]
        [MaxLength(50, ErrorMessage = "o Email deve conter no maximo 15 caracteres")]
        //[Remote("SelectEmailDoCliente", "Cliente", ErrorMessage = "O email já está cadastrado")]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\-][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "Digite um Email válido")]
        public string emailCustomer { get; set; }


        [MaxLength(9, ErrorMessage = "a senha deve conter no maximo 8 caracteres")]
        public string passwordCustomer { get; set; }


        [Display(Name = "Número do Telefone")]
        [Required(ErrorMessage = "Digite o número de telefone")]
        [MaxLength(11, ErrorMessage = "O telefone deve conter 11 caracteres")]
        [MinLength(11, ErrorMessage = "O telefone deve conter 11 caracteres")]
        [RegularExpression(@"^[0-9]+${11,11}", ErrorMessage = "Somente números")]
        public string phoneCustomer { get; set; }
    }
}