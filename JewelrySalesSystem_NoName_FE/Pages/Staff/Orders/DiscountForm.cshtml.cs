using JewelrySalesSystem_NoName_FE.DTOs.Discounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JewelrySalesSystem_NoName_FE.Pages.Staff.Orders
{
    public class DiscountFormModel : PageModel
    {
        [BindProperty]
        public DiscountRequest DiscountRequest { get; set; }

        public void OnGet(string orderId, string managerId)
        {
            // Khởi tạo giá trị ban đầu cho form
            //DiscountRequest = new DiscountRequest
            //{
            //    OrderId = orderId,
            //    ManagerId = managerId
            //};
        }

        public IActionResult OnPostSaveDiscount()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Xử lý lưu thông tin discount request tại đây

            return RedirectToPage("Success");
        }
    }
}
