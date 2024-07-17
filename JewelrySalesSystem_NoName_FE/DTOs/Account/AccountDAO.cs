using System.ComponentModel.DataAnnotations;

namespace JewelrySalesSystem_NoName_FE.DTOs.Account
{
    public class AccountDAO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập họ tên.")]
        [StringLength(50, ErrorMessage = "Họ tên phải có độ dài từ 3 đến 50 ký tự.", MinimumLength = 3)]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập số điện thoại.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string? Phone { get; set; } = null!;

        [Required(ErrorMessage = "Yêu cầu nhập ngày tháng năm sinh.")]
        public DateTime? Dob { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu.")]
        [StringLength(16, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.", MinimumLength = 6)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Yêu cầu nhập địa chỉ.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Yêu cầu chọn hình ảnh.")]
        public string? ImgUrl { get; set; }

        public string? Status { get; set; }

        public bool? Deflag { get; set; }

        [Required(ErrorMessage = "Yêu cầu chọn chức vụ.")]
        public Guid RoleId { get; set; }

        public DateTime? InsDate { get; set; }

        public DateTime? UpsDate { get; set; }
        public string RoleName { get; set; }
        public static readonly List<Guid> ExcludedRoleIds = new List<Guid>
        {
            Guid.Parse("7C9E6679-7425-40DE-944B-E07FC1F90AE9"),
            Guid.Parse("0F8FAD5B-D9CB-469F-A165-70867728950E")
        };
    }
}
