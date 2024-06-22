using JewelrySalesSystem_NoName_FE.DTOs.Account;
using JewelrySalesSystem_NoName_FE.DTOs.Orders;
using JewelrySalesSystem_NoName_FE.DTOs.Product;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
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
        [BindProperty]
        public OrderDTO orderData { get; set; }

        public async Task<JsonResult> OnGetProductAsync(string productCode)
        {
            var apiUrl = $"{ApiPath.ProductCodeGetListPromoton}?productCode={productCode}";
            var response = await _httpClient.GetStringAsync(apiUrl);
            var product = JsonConvert.DeserializeObject<ProductResponse>(response);
            if (product.Product.Quantity < 1)
            {
                TempData["OutOfProduct"] = "Sản phẩm tạm thời hết";
            }
            listPromotions = product.Promotions.ToList();
            return new JsonResult(product);
        }

        public async Task<IActionResult> OnGetHandleCustomerAsync(string phone, string fullName, string action)
        {
            if (action == "search")
            {
                var result = await SearchCustomerAsync(phone);

                if (result is SearchAccountDTO account)
                {
                    return new JsonResult(new
                    {
                        success = true,
                        message = "Đã đăng kí thành viên",
                        showCreateButton = false,
                        phone = phone,
                        name = account.FullName
                    });
                }
                else if (result is ErrorResponse errorResponse)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Cần đăng kí thành viên",
                        showCreateButton = true,
                        phone = phone,
                        name = fullName
                    });
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
                    return new JsonResult(new { success = true, message = "Tạo tài khoản thành công", phone = phone, name = fullName });
                }
            }

            return new JsonResult(new { success = false, message = "Không có thay đổi" });
        }



        public async Task<object> SearchCustomerAsync(string phone)
        {
            if (phone == null) return TempData["PhoneNull"] = "Điền số điện thoại khách hàng!";
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

        //[HttpPost]
        public async Task<IActionResult> OnPostCreateInvoiceAsync()
        {
            using (var reader = new StreamReader(HttpContext.Request.Body))
            {
                var body = await reader.ReadToEndAsync();
                var orderData = JsonConvert.DeserializeObject<OrderDTO>(body);
                Console.WriteLine($"Data order: {orderData}");

                //if (orderData == null || string.IsNullOrEmpty(orderData.CustomerPhone) || orderData.TotalPrice == null || orderData.Details.Count == 0)
                //{
                //    return BadRequest(new { message = "Invalid order data" });
                //}
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

        public IActionResult OnPostClearTempData()
        {
            TempData.Clear();
            return new JsonResult(new { success = true });
        }
    }
}
