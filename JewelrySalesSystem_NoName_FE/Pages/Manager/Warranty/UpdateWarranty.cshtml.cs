using JewelrySalesSystem_NoName_FE.DTOs.Warranty;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using JewelrySalesSystem_NoName_FE.Ultils;
using System;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Warranty
{
    [Authorize(Roles = "Manager")]
    public class UpdateWarrantyModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateWarrantyModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public WarrantyDTO Warranty { get; set; }

        public bool IsExpired { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            var url = $"{ApiPath.Warranty}/{id}";
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetStringAsync(url);

                Warranty = JsonConvert.DeserializeObject<WarrantyDTO>(response);

                var currentDate = DateTime.UtcNow;
                IsExpired = currentDate > Warranty.ExpirationDate;

                if (IsExpired)
                {
                    Warranty.Status = "Expired";
                    Warranty.Note = "?ã quá th?i h?n ???c b?o hành";

                    // G?i API ?? c?p nh?t tr?ng thái và ghi chú
                    var jsonContent = JsonConvert.SerializeObject(Warranty);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var updateResponse = await client.PutAsync(url, content);
                    updateResponse.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                // Handle error appropriately
                return RedirectToPage("./ListWarranty");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            var url = $"{ApiPath.Warranty}/{id}";
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var warrantyRequest = new WarrantyRequest
                {
                    DateOfPurchase = Warranty.DateOfPurchase,
                    ExpirationDate = Warranty.ExpirationDate,
                    Period = Warranty.Period,
                    Deflag = Warranty.Deflag,
                    Status = Warranty.Status,
                    ConditionWarrantyId = Warranty.ConditionnWarrantyId,
                    Note = Warranty.Note
                };

                var jsonContent = JsonConvert.SerializeObject(warrantyRequest);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PutAsync(url, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                // Handle error appropriately
                return Page();
            }

            return RedirectToPage("./ListWarranty");
        }
    }
}
