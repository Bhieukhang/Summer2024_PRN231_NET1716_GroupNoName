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
using System.Drawing;
using JewelrySalesSystem_NoName_FE.DTOs.Membership;
using JewelrySalesSystem_NoName_FE.DTOs;
using JewelrySalesSystem_NoName_FE.DTOs.Promotions;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Warranty
{
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
        public IList<ConditionWarrantyDTO> ConditionWarranties { get; set; } = new List<ConditionWarrantyDTO>();
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageCondition { get; set; }

        public async Task OnGetAsync(int? currentPage)
        {
            Page = currentPage ?? 1;
            Size = 10;

            var url = $"{ApiPath.WarrantyList}?page={Page}&size={Size}";
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetStringAsync(url);

                var paginateResult = JsonConvert.DeserializeObject<Paginate<WarrantyDTO>>(response);
                WarrantyList = paginateResult.Items;
                TotalItems = paginateResult.Total;
                TotalPages = paginateResult.TotalPages;
            }
            catch (Exception ex)
            {
                WarrantyList = new List<WarrantyDTO>();
            }
        }

        public async Task<IActionResult> OnGetConditionAsync()
        {
            var url = $"{ApiPath.ConditionWarrantyList}?page=1&size=100";
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetStringAsync(url);

                var paginateResult = JsonConvert.DeserializeObject<Paginate<ConditionWarrantyDTO>>(response);
                ConditionWarranties = paginateResult.Items;
                return Partial("_ListConditionWarranties", ConditionWarranties);
            }
            catch (HttpRequestException httpRequestException)
            {
                return BadRequest($"Error fetching data from API: {httpRequestException.Message}");
            }
        }
    }
}
