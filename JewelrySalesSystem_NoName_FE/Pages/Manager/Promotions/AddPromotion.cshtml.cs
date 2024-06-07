using JewelrySalesSystem_NoName_FE.Requests.Promotions;
using JewelrySalesSystem_NoName_FE.Responses;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Promotions
{
    public class AddPromotionModel : PageModel
    {
        [BindProperty]
        public AddPromotionRequest AddPromotionRequest { get; set; } = new();

        [BindProperty]
        public string? ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            ErrorMessage = string.Empty;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (AddPromotionRequest == null) throw new Exception("Promotion data is invalid.");
                var token = HttpContext.Session.GetString("Token") ?? "";

                var response = await ApiClient.PostAsync<ApiResponse>($"{ApiPath.Promotion}", AddPromotionRequest, token);
                if (!response.Success) throw new Exception(response.Message);

                return RedirectToPage("ListPromotion");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                Console.WriteLine(ex.ToString());
                ErrorMessage = ex.Message;
                return Page();
            }
        }

    }
}
