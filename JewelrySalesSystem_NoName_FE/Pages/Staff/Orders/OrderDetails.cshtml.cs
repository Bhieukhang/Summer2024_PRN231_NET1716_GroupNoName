using JewelrySalesSystem_NoName_FE.DTOs.Orders;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Staff.Orders
{
    public class OrderDetailsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderDetailsModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public Guid? OrderId { get; set; }
        public OrderDTO Order { get; set; }
        public List<OrderDetailResponse> OrderDetails { get; set; } = new List<OrderDetailResponse>();

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

                var apiUrl = $"{ApiPath.OrderByID}?id={id}";
                var response = await client.GetAsync(apiUrl);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Kết nối không được xác thực ! Hãy login lại .";
                    return RedirectToPage("/Auth/Login");
                }

                Order = JsonConvert.DeserializeObject<OrderDTO>(await response.Content.ReadAsStringAsync());

                var orderDetailsApiUrl = $"{ApiPath.OrderListDetail}?id={id}";
                var orderDetailsResponse = await client.GetAsync(orderDetailsApiUrl);

                if (orderDetailsResponse.IsSuccessStatusCode)
                {
                    var orderDetailsContent = await orderDetailsResponse.Content.ReadAsStringAsync();
                    var orderDetailsResult = JsonConvert.DeserializeObject<OrderDetailsResponse>(orderDetailsContent);
                    OrderDetails = orderDetailsResult.ListOrder;
                }

                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return Page();
            }
        }

        public class OrderDetailsResponse
        {
            public List<OrderDetailResponse> ListOrder { get; set; }
            public Guid OrderId { get; set; }
        }
    }
}
