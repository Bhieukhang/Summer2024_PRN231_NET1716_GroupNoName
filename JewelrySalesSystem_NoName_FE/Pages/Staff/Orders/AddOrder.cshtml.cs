using JewelrySalesSystem_NoName_FE.DTOs.Account;
using JewelrySalesSystem_NoName_FE.DTOs.Orders;
using JewelrySalesSystem_NoName_FE.DTOs.Product;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Xml.Linq;

namespace JewelrySalesSystem_NoName_FE.Pages.Staff.Orders
{
    public class AddOrderModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddOrderModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = _httpClientFactory.CreateClient();
        }
        [BindProperty]
        public string CustomerPhone { get; set; }
        [BindProperty]
        public string CustomerName { get; set; }
        [BindProperty]
        public string PaymentMethod { get; set; }
        [BindProperty]
        public List<ProductDetail> ProductDetails { get; set; }
        public IList<Promotion> listPromotions { get; set; } = new List<Promotion>();

        public async Task<JsonResult> OnGetProductAsync(string productCode)
        {
            var apiUrl = $"{ApiPath.ProductCodeGetListPromoton}?productCode={productCode}";
            var response = await _httpClient.GetStringAsync(apiUrl);
            var product = JsonConvert.DeserializeObject<ProductResponse>(response);
            listPromotions = product.Promotions.ToList();
            return new JsonResult(product);
        }

        public async Task<IActionResult> OnPostHandleCustomerAsync(string phone, string fullName, string action)
        {
            if (action == "search")
            {
                var result = await SearchCustomerAsync(phone);

                if (result is SearchAccountDTO account)
                {
                    TempData["Message"] = "Đã đăng kí thành viên";
                    TempData["ShowCreateMemberButton"] = false;
                    TempData["Phone"] = phone;
                    TempData["Name"] = fullName;
                }
                else if (result is ErrorResponse errorResponse)
                {
                    TempData["Message"] = "Cần đăng kí thành viên";
                    TempData["ShowCreateMemberButton"] = true;
                    TempData["Phone"] = phone;
                    TempData["Name"] = fullName;
                }
            }
            else if (action == "create")
            {
                var createResponse = await CreateAccountAsync(phone, fullName);
                if (createResponse != null)
                {
                    TempData["Phone"] = phone;
                    TempData["Name"] = fullName;
                    TempData["Message"] = "Tạo tài khoản thành công";
                }
            }

            return Page();
        }

        public async Task<object> SearchCustomerAsync(string phone)
        {
            var token = HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var apiUrl = $"{ApiPath.SearchAccount}?phone={phone}";
            var response = await client.GetAsync(apiUrl);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var account = JsonConvert.DeserializeObject<SearchAccountDTO>(responseString);
                return account;
            }
            else
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseString);
                return errorResponse;
            }
        }

        public async Task<CreateAccountResponse> CreateAccountAsync(string phone, string fullName)
        {
            var token = HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var apiUrl = $"{ApiPath.MembershipList}";
            var payload = new { phone, fullName };
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CreateAccountResponse>(responseString);
        }

        public async Task<IActionResult> OnPostCreateInvoiceAsync([FromBody] OrderDTO orderData)
        {
            if (orderData == null || string.IsNullOrEmpty(orderData.CustomerPhone) || orderData.TotalPrice == null || orderData.Details.Count == 0)
            {
                return BadRequest(new { message = "Invalid order data" });
            }
            var token = HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var apiUrl = $"{ApiPath.OrderListPromotion}";
            var payload = new OrderDTO()
            {
                CustomerPhone = orderData.CustomerPhone,
                //PromotionId = orderData.PromotionId,
                DiscountId = orderData.DiscountId,
                TotalPrice = orderData.TotalPrice,
                MaterialProccessPrice = orderData.MaterialProccessPrice,
                Details = orderData.Details,
            };
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseString);

            // Extracting the Id value
            var orderId = (string)jsonResponse.Id;
            var phone = orderData.CustomerPhone;

            var redirectUrl = $"/Manager/Warranty/CreateWarranties?orderId={orderId}&phone={phone}";

            Console.WriteLine(redirectUrl);
            return new JsonResult(new { redirectUrl });
        }
    }
}
