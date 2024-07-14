using JewelrySalesSystem_NoName_FE.DTOs;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Statics
{
    public class DashboardModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        [BindProperty]
        public DashboardDTO DashboardData { get; set; } = new();

        [BindProperty]
        public int Year { get; set; } = DateTime.Now.Year;
        [BindProperty]
        public int TotalOrder {  get; set; }

        public async Task<IActionResult> OnGetAsync(int year)
        {
            
            try
            {
                var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var urlOrder = $"{ApiPath.OrderTotal}?year=2024";
                if(year != 0) Year = year;

                DashboardData = await ApiClient.GetAsync<DashboardDTO>($"{ApiPath.Dashboard}?year={Year}", token);
                TotalOrder = await client.GetFromJsonAsync<int>(urlOrder);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
            return Page();
        }
    }
}
