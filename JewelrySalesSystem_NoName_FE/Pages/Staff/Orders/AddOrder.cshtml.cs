using JewelrySalesSystem_NoName_FE.DTOs.Account;
using JewelrySalesSystem_NoName_FE.DTOs.Product;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public IList<Promotion> listPromotions { get; set; } = new List<Promotion>();

        public async Task<JsonResult> OnGetProductAsync(string productCode)
        {
            var apiUrl = $"{ApiPath.ProductCodeGetListPromoton}?productCode={productCode}";
            var response = await _httpClient.GetStringAsync(apiUrl);
            var product = JsonConvert.DeserializeObject<ProductResponse>(response);
            listPromotions = product.Promotions.ToList();
            return new JsonResult(product);
        }

        public async Task<IActionResult> OnPostSearchCustomerAsync(string phone, string name)
        {
            var result = await SearchCustomerAsync(phone);

            if (result is SearchAccountDTO account)
            {
                TempData["Message"] = "Đã đăng kí thành viên";
                TempData["ShowCreateMemberButton"] = false;
                TempData["Phone"] = phone;
                TempData["Name"] = name;
            }
            else if (result is ErrorResponse errorResponse)
            {
                TempData["Message"] = "Cần đăng kí thành viên";
                TempData["ShowCreateMemberButton"] = true;
                TempData["Phone"] = phone;
                TempData["Name"] = name;
            }

            return Page();
        }


        public async Task<object> SearchCustomerAsync(string phone)
        {
            var token = HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var apiUrl = $"https://localhost:44318/api/v1/Account/search/member?phone={phone}";
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

        public async Task<CreateAccountResponse> CreateAccountAsync(string phone, string name)
        {
            var apiUrl = "https://localhost:44318/api/v1/membership";
            var payload = new { phone, name };
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CreateAccountResponse>(responseString);
        }
    }
}
