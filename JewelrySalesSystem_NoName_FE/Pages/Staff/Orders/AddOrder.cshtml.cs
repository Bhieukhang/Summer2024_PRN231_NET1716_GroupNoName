using JewelrySalesSystem_NoName_FE.DTOs.Account;
using JewelrySalesSystem_NoName_FE.DTOs.Product;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Staff.Orders
{
    public class AddOrderModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddOrderModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = _httpClientFactory.CreateClient();
        }

        public IList<Promotion> listPromotions { get; set; } = new List<Promotion>();

        public async Task<JsonResult> OnGetProductAsync(string productCode)
        {
            var apiUrl = $"{ApiPath.ProductCodeGetListPromoton}?productCode={productCode}";
            var response = await _httpClient.GetStringAsync(apiUrl);
            var product = JsonConvert.DeserializeObject<ProductResponse>(response);
            listPromotions = product.Promotions.ToList();
            return new JsonResult(product);
        }
    }
}
