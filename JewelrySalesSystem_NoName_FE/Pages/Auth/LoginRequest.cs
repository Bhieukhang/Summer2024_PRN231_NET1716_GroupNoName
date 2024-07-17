using System.ComponentModel.DataAnnotations;

namespace JewelrySalesSystem_NoName_FE.Pages.Auth
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Yêu cầu nhập số điện thoại.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu.")]
        [StringLength(16, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.", MinimumLength = 6)]
        public string Password { get; set; }

    }
}