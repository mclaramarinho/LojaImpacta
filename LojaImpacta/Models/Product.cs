using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaImpacta.Models
{
    public class Product
    {
        [Display(Name = "Cód. Produto")]
        [Key]
        public int ProductID { get; set; }

        [Display(Name = "Nome do produto")]
        [StringLength(maximumLength: 30)]
        [Required]
        public string ProductName { get; set; }

        [Display(Name = "URL da Imagem")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; } = "https://th.bing.com/th/id/R.51879f9aeaaf6060aa42a64df71696f1?rik=h8Ox9c2rUwGi%2fg&pid=ImgRaw&r=0";

        [Display(Name = "Preço")]
        [DataType(DataType.Currency)]
        [Required]
        public decimal Price { get; set; }

        [Display(Name = "Descrição do produto")]
        [StringLength(maximumLength: 250)]
        public string? Description { get; set; }

        [Display(Name = "Estoque disponível")]
        [Required]
        public int AmountAvailabel { get; set; } = 0;

    }
}
