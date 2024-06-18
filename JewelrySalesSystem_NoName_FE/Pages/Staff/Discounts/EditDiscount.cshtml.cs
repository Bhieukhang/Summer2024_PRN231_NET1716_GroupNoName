using JewelrySalesSystem_NoName_FE.Requests.Discounts;
using JewelrySalesSystem_NoName_FE.Responses;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JewelrySalesSystem_NoName_FE.Pages.Staff.Discounts
{
    public class EditDiscountModel : PageModel
    {
        [BindProperty]
        public DiscountRequest DiscountRequest { get; set; } = new();

        [BindProperty]
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGet(Guid discountId)
        {
            try
            {
                var token = HttpContext.Session.GetString("Token") ?? "";
                DiscountRequest = await ApiClient.GetAsync<DiscountRequest>($"{ApiPath.Discount}/id?id={discountId}", token);
                if (DiscountRequest == null) throw new Exception("Cannot found disocunt with id = " + discountId);
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
                if (DiscountRequest == null) throw new Exception("Discount data is invalid.");
                var token = HttpContext.Session.GetString("Token") ?? "";
                var response = await ApiClient.PutAsync<ApiResponse>($"{ApiPath.Discount}", DiscountRequest, token);
                if (!response.Success) throw new Exception(response.Message);

                return RedirectToPage("ListDiscount");
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
