using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace JewelrySalesSystem_NoName_FE.Pages.Staff.Orders
{
    public class ListPromotionApplyModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ListPromotionApplyModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        //public IActionResult OnGet()
        //{
        //    var promotions = _promotionService.GetAllPromotions(); // Assume this fetches promotions
        //    ViewData["Order"] = new OrderDTO(); // Assuming some order data is set here
        //    return Page();
        //}
        public async Task<IActionResult> OnPostCheckAsync()
        {
            return new JsonResult(new { success = true, message = "Promotion applied successfully." });
        }
    }
}
