using JewelrySalesSystem_NoName_FE.DTOs.Product;
using JewelrySalesSystem_NoName_FE.DTOs.Promotions;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Promotions
{
    public class PromotionDetailsModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PromotionDetailsModel(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public PromotionDTO? Promotion { get; private set; }

        [BindProperty]
        public List<GroupDTO> Groups { get; set; } = new();

        [BindProperty]
        public List<ProductDTO> Products { get; set; } = new();

        public async Task OnGet(Guid promotionId)
        {
            try
            {
                var token = HttpContext.Session.GetString("Token") ?? "";
                Promotion = await ApiClient.GetAsync<PromotionDTO>($"{ApiPath.Promotion}/id?id={promotionId}", token);
                Groups = await ApiClient.GetAsync<List<GroupDTO>>($"{ApiPath.PromotionGroup}?id={promotionId}", token);
                Products = await ApiClient.GetAsync<List<ProductDTO>>($"{ApiPath.AllProductEndpoint}", token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
        }
    }
}
