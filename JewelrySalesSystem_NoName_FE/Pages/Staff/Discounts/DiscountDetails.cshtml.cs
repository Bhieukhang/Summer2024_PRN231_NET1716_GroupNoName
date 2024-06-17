using JewelrySalesSystem_NoName_FE.DTOs.Discounts;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JewelrySalesSystem_NoName_FE.Pages.Staff.Discounts
{
    public class DiscountDetailsModel : PageModel
    {
        public DiscountDTO? Discount { get; private set; }

        public async Task OnGet(Guid promotionId)
        {
            try
            {
                var token = HttpContext.Session.GetString("Token") ?? "";
                Discount = await ApiClient.GetAsync<DiscountDTO>($"{ApiPath.Discount}?id={promotionId}", token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
        }
    }
}
