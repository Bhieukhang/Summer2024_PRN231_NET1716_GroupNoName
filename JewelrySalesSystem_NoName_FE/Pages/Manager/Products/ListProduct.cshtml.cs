using JewelrySalesSystem_NoName_FE.DTOs;
using JewelrySalesSystem_NoName_FE.DTOs.Material;
using JewelrySalesSystem_NoName_FE.DTOs.Product;
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
        [BindProperty]
        public string? SelectedCategory { get; set; }
        [BindProperty]
        public string? SelectedMaterial { get; set; }
        public IList<ProductDTO> productList { get; set; } = new List<ProductDTO>();
        public IList<CategoryDTO> cateList { get; set; } = new List<CategoryDTO>();
        public IList<MaterialDTO> mateList { get; set; } = new List<MaterialDTO>();
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync(int? currentPage, string? searchCode, string? selectedCategory, string? selectedMaterial)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "You need to login first.";
                return RedirectToPage("/Auth/Login");
            }

            Page = currentPage ?? 1;
            Size = 12;
            SearchCode = searchCode;
            SelectedCategory = selectedCategory;
            SelectedMaterial = selectedMaterial;

            var url = $"{ApiPath.ProductList}/searchAndFilter?page={Page}&size={Size}";

            if (!string.IsNullOrEmpty(SearchCode))
            {
                url += $"&code={SearchCode}";
            }
            if (!string.IsNullOrEmpty(SelectedCategory))
            {
                url += $"&categoryId={SelectedCategory}";
            }
            if (!string.IsNullOrEmpty(SelectedMaterial))
            {
                url += $"&materialId={SelectedMaterial}";
            }

            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var productResponse = await client.GetStringAsync(url);
                var paginateResult = JsonConvert.DeserializeObject<Paginate<ProductDTO>>(productResponse);
                TotalPages = paginateResult.TotalPages;

                var categoryResponse = await client.GetAsync(ApiPath.CategoryList);
                if (categoryResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Unauthorized access. Please login again.";
                    return RedirectToPage("/Auth/Login");
                }
                cateList = JsonConvert.DeserializeObject<List<CategoryDTO>>(await categoryResponse.Content.ReadAsStringAsync());

                var materialResponse = await client.GetAsync(ApiPath.MaterialList);
                if (materialResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Unauthorized access. Please login again.";
                    return RedirectToPage("/Auth/Login");
                }
                mateList = JsonConvert.DeserializeObject<List<MaterialDTO>>(await materialResponse.Content.ReadAsStringAsync());
                productList = paginateResult.Items;

                ViewData["SelectedCategory"] = SelectedCategory;
                ViewData["SelectedMaterial"] = SelectedMaterial;

                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                productList = new List<ProductDTO>();
                return Page();
            }
        }
    }
}
