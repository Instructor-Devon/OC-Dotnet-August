using System.ComponentModel.DataAnnotations;

namespace JustProducts.Models
{
    public class Product
    {
        [Key]
        public int ProductId {get;set;}
        [Required]
        public string Name {get;set;}
        [Required]
        [MinLength(10)]
        public string Description {get;set;}
        [Range(0, double.MaxValue)]
        public double Price {get;set;}
    }
}