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
    [Authorize(Roles = "Manager, Staff")]
    public class UpdateWarrantyModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UpdateWarrantyModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public WarrantyRequest Warranty { get; set; }

        public bool IsExpired { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "Bạn cần đăng nhập trước.";
                return RedirectToPage("/Auth/Login");
            }

            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var url = $"{ApiPath.Warranty}/{id}";
                var response = await client.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Truy cập trái phép. Vui lòng đăng nhập lại.";
                    return RedirectToPage("/Auth/Login");
                }

                Warranty = JsonConvert.DeserializeObject<WarrantyRequest>(await response.Content.ReadAsStringAsync());

                var currentDate = DateTime.UtcNow;
                IsExpired = currentDate > Warranty.ExpirationDate;

                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Đã xảy ra lỗi: {ex.Message}";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "Bạn cần đăng nhập trước.";
                return RedirectToPage("/Auth/Login");
            }

            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var currentDate = DateTime.UtcNow;
                IsExpired = currentDate > Warranty.ExpirationDate;

                var warrantyUpdateRequest = new WarrantyRequest
                {
                    Status = Warranty.Status,
                    Note = Warranty.Note
                };

                var jsonContent = JsonConvert.SerializeObject(warrantyUpdateRequest);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var url = $"{ApiPath.Warranty}/{id}";
                var response = await client.PutAsync(url, content);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Truy cập trái phép. Vui lòng đăng nhập lại.";
                    return RedirectToPage("/Auth/Login");
                }

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Bảo hành đã được cập nhật thành công!";
                    return RedirectToPage("./ListWarranty");
                }
                else
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Đã xảy ra lỗi khi cập nhật bảo hành. Mã trạng thái: {response.StatusCode}, Phản hồi: {responseBody}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Đã xảy ra lỗi: {ex.Message}");
            }

            return Page();
        }
    }
}
