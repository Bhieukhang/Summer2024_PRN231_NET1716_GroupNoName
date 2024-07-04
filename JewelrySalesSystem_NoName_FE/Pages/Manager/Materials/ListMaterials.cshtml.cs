using JewelrySalesSystem_NoName_FE.DTOs.Material;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Materials
{
    public class ListMaterialsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ListMaterialsModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public string? SearchCode { get; set; }
        public IList<MaterialDTO> mateList { get; set; } = new List<MaterialDTO>();

        public async Task<IActionResult> OnGetAsync()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            var apiUrl = $"{ApiPath.MaterialList}";
            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetStringAsync(apiUrl);
                mateList = JsonConvert.DeserializeObject<List<MaterialDTO>>(response);
            }
            catch (Exception ex)
            {
                mateList = new List<MaterialDTO>();
            }
            return Page();
        }

        //public async Task<IActionResult> OnPostSearchAsync()
        //{
        //    var token = HttpContext.Session.GetString("Token");
        //    if (string.IsNullOrEmpty(token))
        //    {
        //        return RedirectToPage("/Auth/Login");
        //    }

        //    if (string.IsNullOrEmpty(SearchCode))
        //    {
        //        await LoadCategoryListAsync();
        //    }

        //    var client = _httpClientFactory.CreateClient("ApiClient");
        //    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        //    var apiUrl = $"{ApiPath.CategoryList}/code?code={SearchCode}";
        //    var response = await client.GetAsync(apiUrl);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var jsonResponse = await response.Content.ReadAsStringAsync();
        //        var category = JsonConvert.DeserializeObject<CategoryDTO>(jsonResponse);
        //        cateList = new List<CategoryDTO> { category };
        //    }
        //    else
        //    {
        //        ModelState.AddModelError(string.Empty, "Không tìm thấy loại trang sức.");
        //        cateList = new List<CategoryDTO>();
        //    }
        //    return Page();
        //}

        //private async Task LoadCategoryListAsync()
        //{
        //    var apiUrl = $"{ApiPath.CategoryList}";
        //    try
        //    {
        //        var client = _httpClientFactory.CreateClient("ApiClient");
        //        var response = await client.GetStringAsync(apiUrl);
        //        cateList = JsonConvert.DeserializeObject<List<CategoryDTO>>(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        cateList = new List<CategoryDTO>();
        //    }
        //}
    }
}
