using System.ComponentModel.DataAnnotations;

namespace JewelrySalesSystem_NoName_FE.Requests.Promotions
{
    public class EditPromotionRequest
    {
        [Required(ErrorMessage = "Please choose a Promtion to edit.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please enter Promotion Name.")]
        public string? PromotionName { get; set; }

        [Required(ErrorMessage = "Please enter Type.")]
        public string? Type { get; set; }

        [Required(ErrorMessage = "Please enter Description.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Please enter Product Quantity.")]
        public int? ProductQuantity { get; set; }

        [Required(ErrorMessage = "Please enter Percentage.")]
        public int? Percentage { get; set; }

        [Required(ErrorMessage = "Please choose Start Date.")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Please choose End Date.")]
        public DateTime? EndDate { get; set; }

        [Required]
        public bool? Deflag { get; set; }

        public string? ProductJson { get; set; }
        public List<Guid>? ProductIds { get; set; }
        public List<ProductConditionGroup>? ProductConditionGroups { get; set; }

    }
}
