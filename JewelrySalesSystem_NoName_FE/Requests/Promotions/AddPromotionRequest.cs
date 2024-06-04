using System.ComponentModel.DataAnnotations;

namespace JewelrySalesSystem_NoName_FE.Requests.Promotions
{
    public class AddPromotionRequest
    {
        [Required(ErrorMessage = "Please enter Promotion Name.")]
        [StringLength(100, ErrorMessage = "Please enter value less then 100 characters.")]
        public string? PromotionName { get; set; }

        [Required(ErrorMessage = "Please enter Type.")]
        [StringLength(100, ErrorMessage = "Please enter value less then 100 characters.")]
        public string? Type { get; set; }

        [Required(ErrorMessage = "Please enter Description.")]
        [StringLength(1000, ErrorMessage = "Please enter value less then 1000 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Please enter Product Quantity.")]
        [Range(0, 99999, ErrorMessage = "Please enter number between 0 - 99999.")]
        public int? ProductQuantity { get; set; }

        [Required(ErrorMessage = "Please enter Percentage.")]
        [Range(1, 100, ErrorMessage = "Please enter number between 1 - 100.")]
        public int? Percentage { get; set; }

        [Required(ErrorMessage = "Please choose Start Date.")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Please choose End Date.")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Please choose Deflag.")]
        public bool? Deflag { get; set; } = true;
    }
}
