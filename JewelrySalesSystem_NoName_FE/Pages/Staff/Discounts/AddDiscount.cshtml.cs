using JewelrySalesSystem_NoName_FE.Requests.Discounts;
using JewelrySalesSystem_NoName_FE.Responses;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JewelrySalesSystem_NoName_FE.Pages.Staff.Discounts
{
    public class AddDiscountModel : PageModel
    {
        [BindProperty]
        public DiscountRequest DiscountRequest { get; set; } = new();

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
                if(ModelState.IsValid)
                {
                    if (DiscountRequest == null) throw new Exception("Discount data is invalid.");
                    var token = HttpContext.Session.GetString("Token") ?? "";

                    var response = await ApiClient.PostAsync<ApiResponse>($"{ApiPath.Discount}", DiscountRequest, token);
                    if (!response.Success) throw new Exception(response.Message);

                    return RedirectToPage("ListDiscount");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                Console.WriteLine(ex.ToString());
                ErrorMessage = ex.Message;
            }
            return Page();
        }

    }
}
