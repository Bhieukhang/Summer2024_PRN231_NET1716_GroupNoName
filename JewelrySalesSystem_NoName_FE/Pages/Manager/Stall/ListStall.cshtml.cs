using JewelrySalesSystem_NoName_FE.DTOs.Stall;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using JewelrySalesSystem_NoName_FE.DTOs;
using JewelrySalesSystem_NoName_FE.DTOs.Account;
using System.Net.Http.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Stall
{
    [Authorize(Roles = "Admin, Manager")]
    public class ListStallModels : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ListStallModels(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
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

        public async Task<IActionResult> OnGetAsync(int? page, int? size)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            Page = page ?? 1;
            Size = size ?? 10;

            var url = $"{ApiPath.StallList}?page={Page}&size={Size}";
            var totalCountUrl = $"{ApiPath.TotalAccount}";
            var activeCountUrl = $"{ApiPath.ActiveAccount}";

            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var paginateResult = JsonConvert.DeserializeObject<Paginate<StallDTO>>(responseBody);
                    ListStall = paginateResult.Items;
                    TotalItems = paginateResult.Total;
                    TotalPages = paginateResult.TotalPages;
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to fetch stall data: {response.StatusCode} - {errorResponse}");
                }
                var totalAccountResponse = await client.GetAsync(totalCountUrl);
                if (totalAccountResponse.IsSuccessStatusCode)
                {
                    TotalAccountCount = await totalAccountResponse.Content.ReadFromJsonAsync<int>();
                }
                else
                {
                    var errorResponse = await totalAccountResponse.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to fetch total account count: {totalAccountResponse.StatusCode} - {errorResponse}");
                }
                var activeAccountResponse = await client.GetAsync(activeCountUrl);
                if (activeAccountResponse.IsSuccessStatusCode)
                {
                    ActiveAccountCount = await activeAccountResponse.Content.ReadFromJsonAsync<int>();
                }
                else
                {
                    var errorResponse = await activeAccountResponse.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to fetch active account count: {activeAccountResponse.StatusCode} - {errorResponse}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }

            return Page();
        }
    }
}
