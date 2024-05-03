using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaImpacta.Models
{
    public class AccessLevel
    {
        [Key]
        public int AccessLevelID { get; set; }

        [Display(Name = "Nível de Acesso")]
        [Required]
        public string LevelName { get; set; }

        //[InverseProperty("UserID")]
        //public ICollection<User> Users { get; set; }
    }
}
