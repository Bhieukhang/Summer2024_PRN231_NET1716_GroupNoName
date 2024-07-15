using JewelrySalesSystem_NoName_FE.DTOs.Material;
using JewelrySalesSystem_NoName_FE.DTOs.Product;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Products
{
    public class ExportBillSubProductModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _bucket;
        [BindProperty]
        public ProductDTO Product { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }
        public IList<CategoryDTO> CategoryList { get; set; } = new List<CategoryDTO>();
        public IList<MaterialDTO> MaterialList { get; set; } = new List<MaterialDTO>();

        public ExportBillSubProductModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _bucket = _configuration["Firebase:Bucket"];
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "Bạn cần phải login trước.";
                return RedirectToPage("/Auth/Login");
            }

            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var apiUrl = $"{ApiPath.ProductList}/id?id={id}";
                var response = await client.GetAsync(apiUrl);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Kết nối không được xác thực ! Hãy login lại .";
                    return RedirectToPage("/Auth/Login");
                }

                Product = JsonConvert.DeserializeObject<ProductDTO>(await response.Content.ReadAsStringAsync());

                var categoryApiUrl = $"{ApiPath.CategoryList}";
                var categoryResponse = await client.GetAsync(categoryApiUrl);
                CategoryList = JsonConvert.DeserializeObject<List<CategoryDTO>>(await categoryResponse.Content.ReadAsStringAsync());

                var materialApiUrl = $"{ApiPath.MaterialList}";
                var materialResponse = await client.GetAsync(materialApiUrl);
                MaterialList = JsonConvert.DeserializeObject<List<MaterialDTO>>(await materialResponse.Content.ReadAsStringAsync());

                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return Page();
            }
        }
    }
}
