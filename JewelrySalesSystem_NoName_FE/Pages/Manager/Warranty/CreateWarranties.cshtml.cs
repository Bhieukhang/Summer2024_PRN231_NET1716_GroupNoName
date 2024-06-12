using JewelrySalesSystem_NoName_FE.DTOs;
using JewelrySalesSystem_NoName_FE.DTOs.Warranty;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Drawing;
using System.Text;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Warranty
{
    public class CreateWarrantiesModel : PageModel
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateWarrantiesModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public WarrantyCreate Warranty { get; set; }

        public async Task<IActionResult> OnGetCondition()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            var url = $"{ApiPath.ConditionWarrantyList}?page=1&size=100";
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetStringAsync(url);

                var paginateResult = JsonConvert.DeserializeObject<Paginate<ConditionWarrantyDTO>>(response);
            }
            catch (Exception ex)
            {
                // Handle error appropriately
                //ConditionWarranties = new List<ConditionWarrantyDTO>();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var apiUrlCreate = $"{ApiPath.Warranty}";
            try
            {
                var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToPage("/Auth/Login");
                }
                Warranty.ConditionWarrantyId = Guid.Parse("51142E3C-729C-49B4-B6CF-135D4DF06BA5");
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var jsonContent = new StringContent(JsonConvert.SerializeObject(Warranty), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrlCreate, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    // Nếu thành công, chuyển hướng đến trang khác
                    return Redirect("~/Staff/Orders/OrderBill");
                }
                else
                {
                    // Nếu không thành công, xử lý lỗi
                    ModelState.AddModelError(string.Empty, "Error sending order to backend.");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error sending order to backend.");
                return Page();
            }
        }
    }
}
