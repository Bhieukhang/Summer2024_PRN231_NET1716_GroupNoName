using JewelrySalesSystem_NoName_FE.DTOs.Product;
using JewelrySalesSystem_NoName_FE.DTOs.Promotions;
using JewelrySalesSystem_NoName_FE.DTOs.Warranty;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Categories
{
    public class ListCategoriesModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ListCategoriesModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        [BindProperty]
        public IList<CategoryDTO> cateList { get; set; } = new List<CategoryDTO>();

        public async Task OnGetAsync()
        {
            await LoadProductListAsync();
        }
        public async Task OnPostSearchAsync()
        {

            var client = _httpClientFactory.CreateClient();
            var apiUrl = $"{ApiPath.ProductList}";
            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var category = JsonConvert.DeserializeObject<CategoryDTO>(jsonResponse);
                cateList = new List<CategoryDTO> { category };

                var categories = await ApiClient.GetAsync<List<CategoryDTO>>(ApiPath.CategoryList);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Category not found.");
                cateList = new List<CategoryDTO>();
            }
        }

        private async Task LoadProductListAsync()
        {
            var apiUrl = $"{ApiPath.CategoryList}";
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetStringAsync(apiUrl);
                cateList = JsonConvert.DeserializeObject<List<CategoryDTO>>(response);
            }
            catch (Exception ex)
            {
                cateList = new List<CategoryDTO>();
            }
        }
    }
}
