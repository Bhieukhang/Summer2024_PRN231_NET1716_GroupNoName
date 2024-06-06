using JewelrySalesSystem_NoName_FE.DTOs.Promotions;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Promotions
{
    [Authorize(Roles = "Manager")]
    public class ListPromotionModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ListPromotionModel(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<PromotionDTO> Promotions { get; private set; } = new();
        public int TotalPages { get; private set; } = 0;
        public int CurrentPage { get; private set; } = 1;
        public int PageSize { get; private set; } = 5;
        public int TotalRecord { get; private set; } = 0;
        public string? Search { get; private set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int? currentPage, int? pageSize, string? search)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            try
            {
                CurrentPage = currentPage ?? 1;
                PageSize = pageSize ?? 5;
                Search = search;
                
                var token = HttpContext.Session.GetString("Token") ?? "";
                var promotions = await ApiClient.GetAsync<List<PromotionDTO>>($"{ApiPath.Promotion}?search={Search}", token);

                // Calculate pages
                TotalRecord = promotions.Count;
                TotalPages = (int)Math.Ceiling(TotalRecord / (double)PageSize);
                promotions = promotions.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                Promotions = promotions;
            }
            catch (Exception ex)
            {
                Promotions = new();
                Console.WriteLine($"Error: {ex}");
            }

            return Page();
        }
    }
}
