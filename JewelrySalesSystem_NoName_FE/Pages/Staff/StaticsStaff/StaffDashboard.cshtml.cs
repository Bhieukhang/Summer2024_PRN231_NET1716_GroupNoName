using JewelrySalesSystem_NoName_FE.DTOs;
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

        public async Task<IActionResult> OnGetOrderDashboardAsync()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            //var currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            //var url = $"{ApiPath.OrderTotalInDay}?time={currentDate}";
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            //var totalProductCountResponse = await client.GetAsync(ApiPath.TotalProductCount);
            //TotalProductCount = JsonConvert.DeserializeObject<int>(await totalProductCountResponse.Content.ReadAsStringAsync());

            //var totalOrderInDayResponse = await client.GetAsync(url);
            //TotalOrderInDay = JsonConvert.DeserializeObject<int>(await totalOrderInDayResponse.Content.ReadAsStringAsync());

            var response = await client.GetAsync(ApiPath.OrderDashboard);
            var jsonData = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<StaticOrderCountDto>>(jsonData);
            return new JsonResult(data);

        }

        public async Task<IActionResult> OnGetDashboardAsync()
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
            var dashboardData = new DashboardData
            {
                TotalOrderInDay = TotalOrderInDay,
                TotalProductCount = TotalProductCount
            };

            return new JsonResult(dashboardData);
        }

        public async Task<IActionResult> OnGetCategoryDataAsync()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }
            var url = $"{ApiPath.CategoryDashboard}";

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<CategoryDashboard>(response);

            return new JsonResult(data);
        }
    }
}
