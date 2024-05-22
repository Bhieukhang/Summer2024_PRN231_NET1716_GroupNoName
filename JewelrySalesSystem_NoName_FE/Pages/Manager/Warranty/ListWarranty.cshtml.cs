using JewelrySalesSystem_NoName_FE.DTOs.Warranty;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        public async Task OnGetAsync()
        {
            var apiUrl = "https://localhost:44318/api/v1/warranty";

            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetStringAsync(apiUrl);
                WarrantyList = JsonConvert.DeserializeObject<List<WarrantyDTO>>(response);
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately
                // For now, just set WarrantyList to an empty list
                WarrantyList = new List<WarrantyDTO>();
            }
        }
    }
}
