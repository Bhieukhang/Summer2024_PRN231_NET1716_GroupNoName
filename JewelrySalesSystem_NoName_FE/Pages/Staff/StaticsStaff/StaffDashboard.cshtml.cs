using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;

namespace JewelrySalesSystem_NoName_FE.Pages.Staff.StaticsStaff
{
    public class StaffDashboardModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StaffDashboardModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public int TotalProductCount { get; set; }
        public int TotalOrderInDay { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            var currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            var url = $"{ApiPath.OrderTotalInDay}?time={currentDate}";
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var totalProductCountResponse = await client.GetAsync(ApiPath.TotalProductCount);
            TotalProductCount = JsonConvert.DeserializeObject<int>(await totalProductCountResponse.Content.ReadAsStringAsync());

            var totalOrderInDayResponse = await client.GetAsync(url);
            TotalOrderInDay = JsonConvert.DeserializeObject<int>(await totalOrderInDayResponse.Content.ReadAsStringAsync());

            return Page();
        }
    }
}
