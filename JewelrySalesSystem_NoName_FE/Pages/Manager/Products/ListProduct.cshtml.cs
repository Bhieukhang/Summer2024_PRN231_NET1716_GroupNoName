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
        public ProductDTO searchItem = new ProductDTO();
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync(int? currentPage, string? searchCode, string? selectedCategory, string? selectedMaterial)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "Bạn cần phải login trước.";
                return RedirectToPage("/Auth/Login");
            }

            Page = currentPage ?? 1;
            Size = 12;
            SearchCode = searchCode;
            SelectedCategory = selectedCategory;
            SelectedMaterial = selectedMaterial;

            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            try
            {
                if (!string.IsNullOrEmpty(SearchCode))
                {
                    var searchUrl = $"{ApiPath.ProductList}/code?code={SearchCode}";
                    var productResponse = await client.GetStringAsync(searchUrl);
                    searchItem = JsonConvert.DeserializeObject<ProductDTO>(productResponse);
                }
                else
                {
                    var filterUrl = $"{ApiPath.ProductList}/filter?categoryId={SelectedCategory}&materialId={SelectedMaterial}&page={Page}&size={Size}";
                    var filterResponse = await client.GetStringAsync(filterUrl);
                    var paginateResult = JsonConvert.DeserializeObject<Paginate<ProductDTO>>(filterResponse);

                    if (paginateResult != null)
                    {
                        TotalPages = paginateResult.TotalPages;
                        productList = paginateResult.Items;
                    }
                    else
                    {
                        productList = new List<ProductDTO>();
                    }
                }

                var categoryResponse = await client.GetAsync(ApiPath.CategoryList);
                if (categoryResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Kết nối không được xác thực! Hãy login lại.";
                    return RedirectToPage("/Auth/Login");
                }
                cateList = JsonConvert.DeserializeObject<List<CategoryDTO>>(await categoryResponse.Content.ReadAsStringAsync());

                var materialResponse = await client.GetAsync(ApiPath.MaterialList);
                if (materialResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Kết nối không được xác thực! Hãy login lại.";
                    return RedirectToPage("/Auth/Login");
                }
                mateList = JsonConvert.DeserializeObject<List<MaterialDTO>>(await materialResponse.Content.ReadAsStringAsync());

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
