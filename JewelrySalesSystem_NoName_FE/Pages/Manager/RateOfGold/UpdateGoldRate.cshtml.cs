using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.RateOfGold
{
    public class UpdateGoldRateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public UpdateGoldRateModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        //[BindProperty]
        //public double NewRate { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "You need to login first.";
                return RedirectToPage("/Auth/Login");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync([FromBody] double NewRate)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return new JsonResult(new { success = false, message = "You need to login first." }) { StatusCode = 401 };
            }

            try
            {
                Console.WriteLine($"NewRate: {NewRate}");

                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var content = new StringContent(JsonConvert.SerializeObject(NewRate), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(ApiPath.GoldRateList, content);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return new JsonResult(new { success = false, message = "Unauthorized access. Please login again." }) { StatusCode = 401 };
                }

                if (response.IsSuccessStatusCode)
                {
                    return new JsonResult(new { success = true, message = "Gold rate updated successfully." });
                }
                else
                {
                    return new JsonResult(new { success = false, message = "Failed to update the gold rate." });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = $"Error: {ex.Message}" });
            }
        }
    }
}
