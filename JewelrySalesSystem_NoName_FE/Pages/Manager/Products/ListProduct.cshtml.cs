using JewelrySalesSystem_NoName_FE.DTOs.Product;
using JewelrySalesSystem_NoName_FE.DTOs.Promotions;
using JewelrySalesSystem_NoName_FE.DTOs.Warranty;
using JewelrySalesSystem_NoName_FE.Ultils;
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

        public IList<ProductDTO> productList { get; set; } = new List<ProductDTO>();

        public async Task OnGetAsync()
        {
            await LoadProductListAsync();
        }

        private async Task LoadProductListAsync()
        {
            var apiUrl = $"{ApiPath.ProductList}";
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetStringAsync(apiUrl);
                productList = JsonConvert.DeserializeObject<List<ProductDTO>>(response);
                var categories = await ApiClient.GetAsync<List<CategoryDTO>>(ApiPath.CategoryList);
            }
            catch (Exception ex)
            {
                productList = new List<ProductDTO>();
            }
        }
    }
}
