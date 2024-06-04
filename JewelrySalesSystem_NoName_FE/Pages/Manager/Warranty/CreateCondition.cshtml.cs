using JewelrySalesSystem_NoName_FE.DTOs.Warranty;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Warranty
{
    public class CreateConditionModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateConditionModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        [BindProperty]
        public ConditionWarrantyCreate ConditionWarrantyItem { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var apiUrlCreate = $"{ApiPath.ConditionWarrantyList}";
            try
            {
                var client = _httpClientFactory.CreateClient();
                var jsonContent = new StringContent(JsonConvert.SerializeObject(ConditionWarrantyItem), Encoding.UTF8, "application/json");
                await client.PostAsync(apiUrlCreate, jsonContent);
                return Redirect("~/Manager/Warranty/ListConditionWarranties");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error sending order to backend.");
                return Page();
            }
        }
    }
}
