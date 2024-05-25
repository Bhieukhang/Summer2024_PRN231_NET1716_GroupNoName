using JewelrySalesSystem_NoName_FE.DTOs.Promotions;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Promotions
{
    public class PromotionDetailsModel : PageModel
    {
        public PromotionDTO? Promotion { get; private set; }

        public async Task OnGet(Guid promotionId)
        {
            try
            {
                Promotion = await ApiClient.GetAsync<PromotionDTO>($"{ApiPath.Promotion}/id?id={promotionId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
        }
    }
}
