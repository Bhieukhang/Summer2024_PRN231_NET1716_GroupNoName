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
        [BindProperty]
        public string? SearchCode { get; set; }
        public IList<ProductDTO> productList { get; set; } = new List<ProductDTO>();
        public IList<CategoryDTO> cateList { get; set; } = new List<CategoryDTO>();

        public async Task OnGetAsync()
        {
            await LoadProductListAsync();
        }
        public async Task OnPostSearchAsync()
        {
            if (string.IsNullOrEmpty(SearchCode))
            {
                //ModelState.AddModelError(string.Empty, "Please enter a product code to search.");
                await LoadProductListAsync();
                return;
            }

            var client = _httpClientFactory.CreateClient();
            var apiUrl = $"{ApiPath.ProductList}/code?code={SearchCode}";
            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var product = JsonConvert.DeserializeObject<ProductDTO>(jsonResponse);
                productList = new List<ProductDTO> { product };

                var categories = await ApiClient.GetAsync<List<CategoryDTO>>(ApiPath.CategoryList);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Product not found.");
                productList = new List<ProductDTO>();
            }
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
