using JewelrySalesSystem_NoName_FE.DTOs.Membership;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using JewelrySalesSystem_NoName_FE.DTOs;
using Microsoft.AspNetCore.Mvc;
using JewelrySalesSystem_NoName_FE.DTOs.Account;
using Microsoft.AspNetCore.Authorization;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Memberships
{
    [Authorize(Roles = "Admin, Manager")]
    public class ListMembershipModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ListMembershipModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<MembershipDTO> ListMembership { get; set; } = new List<MembershipDTO>();
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public ProfileDTO profile { get; set; } = new ProfileDTO();

        [BindProperty]
        public Guid UserId { get; set; }
        public int TotalActiveMembership { get; set; }
        public int TotalUnMembership { get; set; }

        public async Task OnGetAsync(int? currentPage)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                RedirectToPage("/Auth/Login");
                return;
            }

            Page = currentPage ?? 1;
            Size = 5;
            Console.WriteLine($"Page: {Page}, Size: {Size}");
            var url = $"{ApiPath.MembershipList}?page={Page}&size={Size}";
            var urlTotalActiveMembership = $"{ApiPath.MembershipActive}";
            var urlTotalUnActiveMembership = $"{ApiPath.MembershipUnActive}";
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetStringAsync(url);

                var paginateResult = JsonConvert.DeserializeObject<Paginate<MembershipDTO>>(response);
                ListMembership = paginateResult.Items;
                TotalItems = paginateResult.Total;
                TotalPages = paginateResult.TotalPages;
                TotalActiveMembership = await client.GetFromJsonAsync<int>(urlTotalActiveMembership);
                TotalUnMembership = await client.GetFromJsonAsync<int>(urlTotalUnActiveMembership);
            }
            catch (Exception ex)
            {
                ListMembership = new List<MembershipDTO>();
            }
        }

        public async Task<IActionResult> OnGetDetailsAsync(Guid UserId)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            var url = $"{ApiPath.MembershipProfile}?id={UserId}";
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetStringAsync(url);

                profile = JsonConvert.DeserializeObject<ProfileDTO>(response);
                return Partial("_ProfilePartial", profile);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
