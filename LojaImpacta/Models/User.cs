using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;

namespace LojaImpacta.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Display(Name = "Nome")]
        [StringLength(maximumLength:80)]
        [Required]
        public string Name { get; set; }


        [StringLength(maximumLength: 11, MinimumLength = 11)]
        [Required]
        public string CPF { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare(nameof(Password), ErrorMessage = "As senhas inseridas não são iguais.")]
        [Required]
        public string ConfirmPswd { get; set; }


        [Display(Name = "Nível de Acesso")] 
        [Required]
        public int AccessLevel { get; set; }

        [Display(Name = "Conta Ativa?")]
        public Boolean AccountActive { get; set; } = true;

        //public ICollection<Sale> Sales { get; set;} = new List<Sale>();
    }
}
