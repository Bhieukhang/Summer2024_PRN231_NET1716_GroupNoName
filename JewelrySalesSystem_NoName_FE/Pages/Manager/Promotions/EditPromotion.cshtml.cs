using JewelrySalesSystem_NoName_FE.Requests.Promotions;
using JewelrySalesSystem_NoName_FE.Responses;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Promotions
{
    public class EditPromotionModel : PageModel
    {
        [BindProperty]
        public EditPromotionRequest EditPromotionRequest { get; set; } = new();

        public async Task<IActionResult> OnGet(Guid promotionId)
        {
            try
            {
                EditPromotionRequest = await ApiClient.GetAsync<EditPromotionRequest>($"{ApiPath.Promotion}/id?id={promotionId}");
                if (EditPromotionRequest == null) throw new Exception("Cannot found promotion with id = " + promotionId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (EditPromotionRequest == null) throw new Exception("Promotion data is invalid.");
                var token = HttpContext.Session.GetString("Token");

                var response = await ApiClient.PutAsync<ApiResponse>($"{ApiPath.Promotion}", EditPromotionRequest, token ?? "");
                if (!response.Success) throw new Exception(response.Message);

                return RedirectToPage("ListPromotion");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                Console.WriteLine(ex.ToString());
                return Page();
            }
        }
    }
}
