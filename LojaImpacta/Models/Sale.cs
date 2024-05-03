using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaImpacta.Models
{
    public class Sale
    {
        [Display(Name = "Cód. Venda")]
        [Key]
        public Guid SaleID { get; set; }

        [Display(Name = "Data da venda")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dd/MM/yyyy - hh:mm:ss}")]
        public DateTime SaleDate { get; set; }

        [Display(Name = "Vendedor")]
        [Required]
        public int SalesPersonID { get; set; }

        [Display(Name = "Cliente")]
        [Required]
        public int ClientID { get; set; }

        [Display(Name = "Produto")]
        [Required]
        public int ProductID { get; set; }

        [Display(Name = "Quantidade")]
        [Required]
        public int AmountBought { get; set; } = 1;

        [Display(Name = "Preço Final")]
        [DataType(DataType.Currency)]
        public decimal FinalPrice { get; set; }

    }
}
