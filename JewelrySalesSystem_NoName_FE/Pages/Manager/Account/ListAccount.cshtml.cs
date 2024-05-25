using JewelrySalesSystem_NoName_FE.DTOs.Membership;
using JewelrySalesSystem_NoName_FE.DTOs;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using JewelrySalesSystem_NoName_FE.DTOs.Account;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Account
{
    public class ListAccountModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ListAccountModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<AccountDAO> ListAccount { get; set; } = new List<AccountDAO>();
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int TotalAccountCount { get; set; }
        public int ActiveAccountCount { get; set; }
        public async Task OnGetAsync(int? page, int? size)
        {
            Page = page ?? 1;
            Size = size ?? 10;

            var url = $"{ApiPath.AccountList}?page={Page}&size={Size}";
            var totalCountUrl = $"{ApiPath.TotalAccount}";
            var activeCountUrl = $"{ApiPath.ActiveAccount}";
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetStringAsync(url);

                var paginateResult = JsonConvert.DeserializeObject<Paginate<AccountDAO>>(response);
                ListAccount = paginateResult.Items;
                TotalItems = paginateResult.Total;
                TotalPages = paginateResult.TotalPages;
                
               TotalAccountCount = await client.GetFromJsonAsync<int>(totalCountUrl);
                ActiveAccountCount = await client.GetFromJsonAsync<int>(activeCountUrl);
            }
            catch (Exception ex)
            {
                // Handle error appropriately
                ListAccount = new List<AccountDAO>();
                TotalAccountCount = 0;
                ActiveAccountCount= 0;
            }
        }
    }
}
