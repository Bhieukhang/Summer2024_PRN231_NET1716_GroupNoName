﻿using JewelrySalesSystem_NoName_FE.DTOs;
using JewelrySalesSystem_NoName_FE.DTOs.Diamonds;
using JewelrySalesSystem_NoName_FE.DTOs.Product;
using JewelrySalesSystem_NoName_FE.Responses;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Diamonds
{
    public class ListDiamondModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ListDiamondModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public string? SearchCode { get; set; }
        public IList<DiamondDTO> diamondList { get; set; } = new List<DiamondDTO>();
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public DiamondDTO searchItem = new DiamondDTO();
        public int TotalDiamondCount { get; set; }

        public async Task<IActionResult> OnGetAsync(int? page, string? searchCode)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "You need to login first.";
                return RedirectToPage("/Auth/Login");
            }

            Page = page ?? 1;
            Size = 12;
            SearchCode = searchCode;

            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            try
            {
                if (!string.IsNullOrEmpty(SearchCode))
                {
                    var searchUrl = $"{ApiPath.DiamondList}/code?code={SearchCode}";
                    var diamondResponse = await client.GetStringAsync(searchUrl);
                    searchItem = JsonConvert.DeserializeObject<DiamondDTO>(diamondResponse);
                }
                else
                {
                    var listUrl = $"{ApiPath.DiamondList}?page={Page}&size={Size}";
                    var listResponse = await client.GetStringAsync(listUrl);
                    //var diamondListResponse = JsonConvert.DeserializeObject<DiamondDTO>(listResponse);

                    var paginateResult = JsonConvert.DeserializeObject<Paginate<DiamondDTO>>(listResponse);

                    if (paginateResult != null)
                    {
                        TotalPages = paginateResult.TotalPages;
                        diamondList = paginateResult.Items;
                    }
                    else
                    {
                        diamondList = new List<DiamondDTO>();
                    }
                }

                var totalDiamondCountResponse = await client.GetStringAsync($"{ApiPath.DiamondTotalCount}");
                TotalDiamondCount = JsonConvert.DeserializeObject<int>(totalDiamondCountResponse);

                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                diamondList = new List<DiamondDTO>();
                return Page();
            }
        }
    }
}
