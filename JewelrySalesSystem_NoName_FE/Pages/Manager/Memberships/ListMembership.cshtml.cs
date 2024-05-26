using JewelrySalesSystem_NoName_FE.DTOs.Membership;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using JewelrySalesSystem_NoName_FE.DTOs;
using Microsoft.AspNetCore.Mvc;
using JewelrySalesSystem_NoName_FE.DTOs.Account;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Memberships
{
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
        public async Task OnGetAsync(int? currentPage)
        {
            Page = currentPage ?? 1;
            Size = 4;
            Console.WriteLine($"Page: {Page}, Size: {Size}");
            var url = $"{ApiPath.MembershipList}?page={Page}&size={Size}";
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetStringAsync(url);

                var paginateResult = JsonConvert.DeserializeObject<Paginate<MembershipDTO>>(response);
                ListMembership = paginateResult.Items;
                TotalItems = paginateResult.Total;
                TotalPages = paginateResult.TotalPages;
            }
            catch (Exception ex)
            {
                ListMembership = new List<MembershipDTO>();
            }
        }

        public async Task<IActionResult> OnGetDetailsAsync(Guid UserId)
        {
            var url = $"{ApiPath.MembershipProfile}?id={UserId}";
            try
            {
                var client = _httpClientFactory.CreateClient();
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
