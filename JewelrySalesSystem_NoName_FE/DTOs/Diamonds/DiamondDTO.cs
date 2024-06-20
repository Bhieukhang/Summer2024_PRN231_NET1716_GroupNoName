using JewelrySalesSystem_NoName_FE.DTOs.Product;
using System.ComponentModel.DataAnnotations;

namespace JewelrySalesSystem_NoName_FE.DTOs.Diamonds
{
    public class DiamondDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string? DiamondName { get; set; }
        [Required]
        public string? Code { get; set; }
        [Required]
        public double? Carat { get; set; }
        [Required]
        public string? Color { get; set; }
        [Required]
        public string? Clarity { get; set; }
        [Required]
        public string? Cut { get; set; }
        [Required]
        public double? Price { get; set; }
        [Required]
        public string? ImageDiamond { get; set; }
        [Required]
        public int? Quantity { get; set; }

        public DateTime? InsDate { get; set; }

        public DateTime? UpsDate { get; set; }
    }
}
