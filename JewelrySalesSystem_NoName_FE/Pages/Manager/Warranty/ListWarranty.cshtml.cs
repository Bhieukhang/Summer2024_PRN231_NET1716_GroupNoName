using JewelrySalesSystem_NoName_FE.DTOs.Warranty;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Authorization;
using JewelrySalesSystem_NoName_FE.DTOs;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Warranty
{
    [Authorize(Roles = "Manager")]
    public class ListWarrantyModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ListWarrantyModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<WarrantyDTO> WarrantyList { get; set; } = new List<WarrantyDTO>();
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync(int? currentPage)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            Page = currentPage ?? 1;
            Size = 10;

            var url = $"{ApiPath.Warranty}?page={Page}&size={Size}";
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetStringAsync(url);

                var paginateResult = JsonConvert.DeserializeObject<Paginate<WarrantyDTO>>(response);
                WarrantyList = paginateResult.Items;
                TotalItems = paginateResult.Total;
                TotalPages = paginateResult.TotalPages;
            }
            catch (Exception ex)
            {
                // Handle error appropriately
                WarrantyList = new List<WarrantyDTO>();
            }

            return Page();
        }

        public async Task<IActionResult> OnGetSearchAsync(string code)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            var url = $"{ApiPath.WarrantySearch}?code={code}";
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetStringAsync(url);

                var warranty = JsonConvert.DeserializeObject<WarrantyDTO>(response);
                return new JsonResult(warranty);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { error = "Error retrieving warranty details" }) { StatusCode = 500 };
            }
        }
    }
}
