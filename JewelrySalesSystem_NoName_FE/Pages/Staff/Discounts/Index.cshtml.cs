using JewelrySalesSystem_NoName_FE.DTOs.Discounts;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Staff.Discounts
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IHttpClientFactory clientFactory, ILogger<IndexModel> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public List<DiscountDTO> Discounts { get; private set; } = new();
        public string? Search { get; private set; } = string.Empty;

        public async Task OnGetAsync()
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:44318/api/v1/discount");

            if (response.IsSuccessStatusCode)
            {
                var token = HttpContext.Session.GetString("Token") ?? "";
                var content = await response.Content.ReadAsStringAsync();
                var discounts = await ApiClient.GetAsync<List<DiscountDTO>>($"{ApiPath.Discount}?search={Search}", token);
                Discounts = discounts;
            }
            else
            {
                Discounts = new List<DiscountDTO>(); // Xử lý khi lấy dữ liệu thất bại
            }
        }
    }

    public class Discount
    {
        public string id { get; set; }
        public int percentDiscount { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string insDate { get; set; }
        public string upsDate { get; set; }
    }
}
