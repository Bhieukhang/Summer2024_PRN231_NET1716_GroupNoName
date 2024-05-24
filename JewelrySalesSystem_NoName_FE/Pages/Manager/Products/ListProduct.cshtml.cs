using JewelrySalesSystem_NoName_FE.DTOs.Warranty;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Products
{
    public class ListProductModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ListProductModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<Product> productList { get; set; } = new List<Product>();

        public async Task OnGetAsync()
        {
            var apiUrl = "https://localhost:44318/api/v1/Product";

            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetStringAsync(apiUrl);
                productList = JsonConvert.DeserializeObject<List<Product>>(response);
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately
                // For now, just set WarrantyList to an empty list
                productList = new List<Product>();
            }
        }
    }
}
