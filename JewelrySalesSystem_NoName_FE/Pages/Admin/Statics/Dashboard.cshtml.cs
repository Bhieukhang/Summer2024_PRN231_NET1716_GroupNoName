using JewelrySalesSystem_NoName_FE.DTOs;
using JewelrySalesSystem_NoName_FE.DTOs.Account;
using JewelrySalesSystem_NoName_FE.DTOs.Orders;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JewelrySalesSystem_NoName_FE.Pages.Admin.Statics
{
    public class AccountDashboardModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountDashboardModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = _httpClientFactory.CreateClient();
        }
        [BindProperty]
        public int TotalAccount { get; set; }
        public int TotalMember { get; set; }

        public async Task<IActionResult> OnGetAccountDataAsync()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }
            var url = $"{ApiPath.AccountDashboard}";

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<AccountDashboard>(response);
            TotalAccount = data.TotalAccount;

            return new JsonResult(data);
        }

        public async Task<IActionResult> OnGetMemberDataAsync()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var urlMember = $"{ApiPath.MemberDashboard}";
            //Dashboard member
            var responseMember = await client.GetStringAsync(urlMember);
            var dataMember = JsonConvert.DeserializeObject<MembershipDashboard>(responseMember);
            TotalMember = dataMember.TotalMember;

            return new JsonResult(dataMember);
        }
    }
}
