using JewelrySalesSystem_NoName_FE.DTOs.Product;
using JewelrySalesSystem_NoName_FE.DTOs.Promotions;
using JewelrySalesSystem_NoName_FE.Requests.Promotions;
using JewelrySalesSystem_NoName_FE.Responses;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Promotions
{
    public class EditPromotionModel : PageModel
    {
        [BindProperty]
        public EditPromotionRequest EditPromotionRequest { get; set; } = new();

        [BindProperty]
        public List<ProductDTO> Products { get; set; } = new();

        [BindProperty]

        public List<GroupDTO> Groups { get; set; } = new();

        public async Task<IActionResult> OnGet(Guid promotionId)
        {
            try
            {
                var token = HttpContext.Session.GetString("Token") ?? "";
                EditPromotionRequest = await ApiClient.GetAsync<EditPromotionRequest>($"{ApiPath.Promotion}/id?id={promotionId}", token);
                if (EditPromotionRequest == null) throw new Exception("Cannot found promotion with id = " + promotionId);

                if (string.IsNullOrEmpty(token))
                {
                    TempData["ErrorMessage"] = "You need to login first.";
                    return RedirectToPage("/Auth/Login");
                }
                Products = await ApiClient.GetAsync<List<ProductDTO>>($"{ApiPath.AllProductEndpoint}", token);
                Groups = await ApiClient.GetAsync<List<GroupDTO>>($"{ApiPath.PromotionGroup}?id={promotionId}", token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
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
                    if (EditPromotionRequest == null) throw new Exception("Promotion data is invalid.");
                    var list = (EditPromotionRequest.ProductJson ?? "").Split(" ");
                    List<Guid> guids = new List<Guid>();
                    foreach (var item in list)
                    {
                        guids.Add(Guid.Parse(item));
                    }
                    EditPromotionRequest.ProductIds = guids;
                    var response = await ApiClient.PutAsync<ApiResponse>($"{ApiPath.Promotion}", EditPromotionRequest, token);
                    if (!response.Success) throw new Exception(response.Message);

                    return RedirectToPage("ListPromotion");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                Console.WriteLine(ex.ToString());
            }
            Products = await ApiClient.GetAsync<List<ProductDTO>>($"{ApiPath.AllProductEndpoint}", token);
            return Page();
        }
    }
}
