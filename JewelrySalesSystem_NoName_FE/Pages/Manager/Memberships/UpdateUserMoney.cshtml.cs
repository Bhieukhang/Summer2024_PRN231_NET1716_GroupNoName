using JewelrySalesSystem_NoName_FE.DTOs.Membership;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Memberships
{
    [Authorize(Roles = "Admin, Manager")]
    public class UpdateUserMoneyModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateUserMoneyModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public MembershipDTO Membership { get; set; }
        [BindProperty]
        public double UserMoney { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid userId)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            var url = $"{ApiPath.MembershipProfile}?id={userId}";
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetStringAsync(url);

                Membership = JsonConvert.DeserializeObject<MembershipDTO>(response);
                UserMoney = Membership.UsedMoney;
                return Page();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> OnPostAsync(Guid userId)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            var url = $"{ApiPath.MembershipUserMoney}?userId={userId}&userMoney={UserMoney}";

            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await client.PatchAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "C?p nh?t s? ti?n s? d?ng thành công.";
                    return RedirectToPage("./ListMembership");
                }
                else
                {
                    TempData["ErrorMessage"] = "C?p nh?t s? ti?n s? d?ng th?t b?i.";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có l?i x?y ra khi c?p nh?t s? ti?n s? d?ng.";
                return Page();
            }
        }
    }
}
