using JewelrySalesSystem_NoName_FE.DTOs;
using JewelrySalesSystem_NoName_FE.DTOs.GRate;
using JewelrySalesSystem_NoName_FE.DTOs.Product;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.GoldRates
{
    public class ListGoldRateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ListGoldRateModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public IList<GoldRateDTO> goldRateList { get; set; } = new List<GoldRateDTO>();
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalPages { get; set; }
        public async Task<IActionResult> OnGetAsync(int? currentPage)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "You need to login first.";
                return RedirectToPage("/Auth/Login");
            }

            Page = currentPage ?? 1;
            Size = 5;

            var url = $"{ApiPath.GoldRateList}?page={Page}&size={Size}";
            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var goldRateResponse = await client.GetStringAsync(url);
                var paginateResult = JsonConvert.DeserializeObject<Paginate<GoldRateDTO>>(goldRateResponse);
                TotalPages = paginateResult.TotalPages;

                goldRateList = paginateResult.Items;
                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                goldRateList = new List<GoldRateDTO>();
                return Page();
            }
        }
    }
}
