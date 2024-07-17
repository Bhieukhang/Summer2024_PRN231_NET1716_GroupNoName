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
                TempData["ErrorMessage"] = "You need to login first.";
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
                    TempData["ErrorMessage"] = "Unauthorized access. Please login again.";
                    return RedirectToPage("/Auth/Login");
                }

                Warranty = JsonConvert.DeserializeObject<WarrantyRequest>(await response.Content.ReadAsStringAsync());

                var currentDate = DateTime.UtcNow;
                IsExpired = currentDate > Warranty.ExpirationDate;

                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "You need to login first.";
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
                    TempData["ErrorMessage"] = "Unauthorized access. Please login again.";
                    return RedirectToPage("/Auth/Login");
                }

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "The Warranty is updated successfully!";
                    return RedirectToPage("./ListWarranty");
                }
                else
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"An error occurred while updating the warranty. Status Code: {response.StatusCode}, Response: {responseBody}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return Page();
        }
    }
}
