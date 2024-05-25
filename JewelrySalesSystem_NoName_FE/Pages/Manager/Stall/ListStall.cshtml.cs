using JewelrySalesSystem_NoName_FE.DTOs;
using JewelrySalesSystem_NoName_FE.DTOs.Account;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Stall
{
    public class ListStallModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ListStallModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public IList<StallDTO> ListStall { get; set; } = new List<StallDTO>();
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

            var url = $"{ApiPath.StallList}?page={Page}&size={Size}";
            var totalCountUrl = $"{ApiPath.TotalAccount}";
            var activeCountUrl = $"{ApiPath.ActiveAccount}";
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetStringAsync(url);

                var paginateResult = JsonConvert.DeserializeObject<Paginate<StallDTO>>(response);
                ListStall = paginateResult.Items;
                TotalItems = paginateResult.Total;
                TotalPages = paginateResult.TotalPages;

                TotalAccountCount = await client.GetFromJsonAsync<int>(totalCountUrl);
                ActiveAccountCount = await client.GetFromJsonAsync<int>(activeCountUrl);
            }
            catch (Exception ex)
            {
                // Handle error appropriately
                ListStall = new List<StallDTO>();
                TotalAccountCount = 0;
                ActiveAccountCount = 0;
            }
        }
    }
}
