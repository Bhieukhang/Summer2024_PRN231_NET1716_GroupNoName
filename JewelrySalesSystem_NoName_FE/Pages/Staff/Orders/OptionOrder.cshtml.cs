using Firebase.Storage;
using JewelrySalesSystem_NoName_FE.DTOs;
using JewelrySalesSystem_NoName_FE.DTOs.Discounts;
using JewelrySalesSystem_NoName_FE.DTOs.Membership;
using JewelrySalesSystem_NoName_FE.DTOs.Orders;
using JewelrySalesSystem_NoName_FE.DTOs.Warranty;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Text.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Staff.Orders
{
    public class OptionOrderModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OptionOrderModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = _httpClientFactory.CreateClient();
        }

        [BindProperty]
        public DiscountRequest DiscountRequest { get; set; } = new DiscountRequest();
        public string ResponseMessage { get; set; }
        [FromQuery(Name = "phone")]
        public string Phone { get; set; }
        [FromQuery(Name = "orderId")]
        public string OrderId { get; set; }
        [BindProperty(SupportsGet = true)]
        public OrderProccess OrderProccess { get; set; } = new OrderProccess();
        public List<OrderDetailProccess> orderDetailProccesses { get; set; } = new List<OrderDetailProccess>();
        public MembershipInfo member { get; set; } = new MembershipInfo();

        public async Task<IActionResult> OnGetAsync()
        {

            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            var url = $"{ApiPath.MembershipInfoOrder}?phone={Phone}";
            var apiUrl = $"{ApiPath.OrderOption}?id={OrderId}";
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                //API: List product proccess
                var response = await client.GetStringAsync(apiUrl);
                var result = JsonConvert.DeserializeObject<OrderProccess>(response);
                //API: Membership info
                var responseMember = await client.GetStringAsync(url);
                var resultMember = JsonConvert.DeserializeObject<MembershipInfo>(responseMember);

                OrderProccess = result;
                orderDetailProccesses = result.Details;
                member = resultMember;

                TempData["ListOrder"] = JsonConvert.SerializeObject(orderDetailProccesses);
            }
            catch (Exception ex)
            {
                return Page();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostSendRequirementAsync()
        {
            using (var reader = new StreamReader(HttpContext.Request.Body))
            {
                var body = await reader.ReadToEndAsync();
                var discountData = JsonConvert.DeserializeObject<DiscountRequest>(body);
                Console.WriteLine($"Data order: {discountData}");

                var token = HttpContext.Session.GetString("Token");
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var content = new StringContent(JsonConvert.SerializeObject(discountData), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{ApiPath.DiscountConfirm}", content);

                if (response.IsSuccessStatusCode)
                {
                    ResponseMessage = "Discount created successfully!";
                }
                else
                {
                    ResponseMessage = $"Error: {response.ReasonPhrase}";
                }

                return new JsonResult(new { success = response.IsSuccessStatusCode, message = ResponseMessage });
            }
        }

        public async Task<IActionResult> OnPutCompleteOrderAsync()
        {
            using (var reader = new StreamReader(HttpContext.Request.Body))
            {
                var body = await reader.ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<OrderPatch>(body);
                Console.WriteLine($"Data order: {data}");
                var orderId = data.OrderId;

                var token = HttpContext.Session.GetString("Token");
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"{ApiPath.OrderUpdate}?id={orderId}", content);
                var responseString = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseString);

                if (response.IsSuccessStatusCode)
                {
                    var phone = (string)jsonResponse.Phone;

                    var redirectUrl = $"/Manager/Warranty/CreateWarranties?orderId={orderId}&phone={phone}";
                    Console.WriteLine(redirectUrl);
                    return new JsonResult(new { redirectUrl });
                }
                else
                {
                    // Handle failure
                    ResponseMessage = $"Error: {response.ReasonPhrase}";
                }

                return new JsonResult(new { success = response.IsSuccessStatusCode, message = ResponseMessage });
            }
        }

        public async Task<IActionResult> OnPostSendDiscountAsync()
        {
            return new JsonResult(new { success = "200", message = ResponseMessage });
        }
    }
}
