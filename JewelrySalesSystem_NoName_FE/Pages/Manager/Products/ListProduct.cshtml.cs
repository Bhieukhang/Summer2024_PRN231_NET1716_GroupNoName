    using JewelrySalesSystem_NoName_FE.DTOs.Product;
    using JewelrySalesSystem_NoName_FE.DTOs.Promotions;
    using JewelrySalesSystem_NoName_FE.DTOs.Warranty;
    using JewelrySalesSystem_NoName_FE.Ultils;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

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

        public async Task<IActionResult> OnGetAsync()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "You need to login first.";
                return RedirectToPage("/Auth/Login");
            }

            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var productResponse = await client.GetAsync(ApiPath.ProductList);
                if (productResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Unauthorized access. Please login again.";
                    return RedirectToPage("/Auth/Login");
                }
                productList = JsonConvert.DeserializeObject<List<ProductDTO>>(await productResponse.Content.ReadAsStringAsync());

                var categoryResponse = await client.GetAsync(ApiPath.CategoryList);
                if (categoryResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Unauthorized access. Please login again.";
                    return RedirectToPage("/Auth/Login");
                }
                cateList = JsonConvert.DeserializeObject<List<CategoryDTO>>(await categoryResponse.Content.ReadAsStringAsync());

                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostSearchAsync()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "You need to login first.";
                return RedirectToPage("/Auth/Login");
            }

            if (string.IsNullOrEmpty(SearchCode))
            {
                await LoadProductListAsync();
                return Page();
            }

            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var apiUrl = $"{ApiPath.ProductList}/code?code={SearchCode}";
                var response = await client.GetAsync(apiUrl);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Unauthorized access. Please login again.";
                    return RedirectToPage("/Auth/Login");
                }

                if (response.IsSuccessStatusCode)
                {
                    var product = JsonConvert.DeserializeObject<ProductDTO>(await response.Content.ReadAsStringAsync());
                    productList = new List<ProductDTO> { product };

                    var categoryResponse = await client.GetAsync(ApiPath.CategoryList);
                    if (categoryResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        TempData["ErrorMessage"] = "Unauthorized access. Please login again.";
                        return RedirectToPage("/Auth/Login");
                    }
                    cateList = JsonConvert.DeserializeObject<List<CategoryDTO>>(await categoryResponse.Content.ReadAsStringAsync());
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Product not found.");
                    productList = new List<ProductDTO>();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                return Page();
            }

            return Page();
        }

        private async Task LoadProductListAsync()
        {
            var token = HttpContext.Session.GetString("Token");
            var apiUrl = $"{ApiPath.ProductList}";
            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var productResponse = await client.GetAsync(apiUrl);
                if (productResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Unauthorized access. Please login again.";
                    RedirectToPage("/Auth/Login");
                }
                productList = JsonConvert.DeserializeObject<List<ProductDTO>>(await productResponse.Content.ReadAsStringAsync());

                var categoryResponse = await client.GetAsync(ApiPath.CategoryList);
                if (categoryResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Unauthorized access. Please login again.";
                    RedirectToPage("/Auth/Login");
                }
                cateList = JsonConvert.DeserializeObject<List<CategoryDTO>>(await categoryResponse.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                productList = new List<ProductDTO>();
            }
        }
    }
}
