using System.ComponentModel.DataAnnotations;

namespace JewelrySalesSystem_NoName_FE.Requests.Promotions
{
    public class AddPromotionRequest
    {
        [Required(ErrorMessage = "Nhập tiêu đề khuyến mãi.")]
        [StringLength(100, ErrorMessage = "Tiêu đề khuyến mãi nhỏ hơn 100 ký tự.")]
        public string? PromotionName { get; set; }

        [Required(ErrorMessage = "Please enter Type.")]
        public string? Type { get; set; }

        [Required(ErrorMessage = "Nhập mô tả khuyến mãi.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Please enter Product Quantity.")]
        [Range(1, 99999, ErrorMessage = "Hãy nhập số từ 1 đến 99999.")]
        public int? ProductQuantity { get; set; } = 1;

        [Required(ErrorMessage = "Nhập % khuyến mãi.")]
        [Range(1, 100, ErrorMessage = "Hãy nhập số từ 1 đến 100.")]
        public int? Percentage { get; set; }

        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Chọn ngày kết thúc.")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Hãy chọn ít nhất 1 sản phẩm.")]
        public string? ProductJson { get; set; }
        public List<Guid>? ProductIds { get; set; }
    }
}
