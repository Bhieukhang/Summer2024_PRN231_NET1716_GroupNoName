using System.ComponentModel.DataAnnotations;

namespace JewelrySalesSystem_NoName_FE.Requests.Promotions
{
    public class EditPromotionRequest
    {
        [Required(ErrorMessage = "Chọn khuyến mãi để chỉnh sửa.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nhập tiêu đề khuyến mãi.")]
        public string? PromotionName { get; set; }

        [Required(ErrorMessage = "Please enter Type.")]
        public string? Type { get; set; }

        [Required(ErrorMessage = "Nhập mô tả khuyến mãi.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Nhập số lượng sản phẩm.")]
        public int? ProductQuantity { get; set; } = 0;

        [Required(ErrorMessage = "Nhập % khuyến mãi.")]
        public int? Percentage { get; set; }

        [Required(ErrorMessage = "Chọn này bắt đầu.")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Chọn ngày kết thúc.")]
        public DateTime? EndDate { get; set; }

        public string? ProductJson { get; set; }
        public List<Guid>? ProductIds { get; set; }
        public List<ProductConditionGroup>? ProductConditionGroups { get; set; }

    }
}
