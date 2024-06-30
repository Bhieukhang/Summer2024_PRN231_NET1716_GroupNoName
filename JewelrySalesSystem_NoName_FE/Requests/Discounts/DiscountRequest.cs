using System.ComponentModel.DataAnnotations;

namespace JewelrySalesSystem_NoName_FE.Requests.Discounts
{
    public class DiscountRequest
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Nhập mã hóa đơn.")] 
        public Guid? OrderId { get; set; }

        public Guid? ManagerId { get; set; }

        [Required(ErrorMessage = "Nhập phần trăm chiết khấu.")] 
        public int? PercentDiscount { get; set; }

        public string? Status { get; set; }

        [Required(ErrorMessage = "Nhập mô tả chiết khấu.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Nhập điều kiện chiết khấu.")]
        public double? ConditionDiscount { get; set; }
        public string? Note { get; set; }
    }
}
