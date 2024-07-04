using JewelrySalesSystem_NoName_FE.DTOs.Product;
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

        [BindProperty]
        public List<ProductDTO>? Products { get; set; } = new();

        public async Task<IActionResult> OnGet()
        {
            try
            {
                var token = HttpContext.Session.GetString("Token");
                if (string.IsNullOrEmpty(token))
                {
                    TempData["ErrorMessage"] = "You need to login first.";
                    return RedirectToPage("/Auth/Login");
                }
                Products = await ApiClient.GetAsync<List<ProductDTO>>($"{ApiPath.AllProductEndpoint}", token);
                ErrorMessage = string.Empty;
               
            }
            catch (Exception ex)
            {

            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = HttpContext.Session.GetString("Token") ?? "";
            try
            { 
                if(ModelState.IsValid)
                {
                    if (AddPromotionRequest == null) throw new Exception("Promotion data is invalid.");

                    if (AddPromotionRequest.StartDate > AddPromotionRequest.EndDate)
                    {
                        ModelState.AddModelError("EndDate", "Ngày kết thúc phải sau ngày bắt đầu.");
                    }

                    var list = (AddPromotionRequest.ProductJson ?? "").Split(" ");
                    List<Guid> guids = new List<Guid>();
                    foreach (var item in list)
                    {
                        guids.Add(Guid.Parse(item));
                    }
                    AddPromotionRequest.ProductIds = guids;

                    var response = await ApiClient.PostAsync<ApiResponse>($"{ApiPath.Promotion}", AddPromotionRequest, token);
                    if (!response.Success) throw new Exception(response.Message);

                    return RedirectToPage("ListPromotion");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                ErrorMessage = ex.Message;
            }
          
            Products = await ApiClient.GetAsync<List<ProductDTO>>($"{ApiPath.AllProductEndpoint}", token);
            return Page();
        }

    }
}
