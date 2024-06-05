using JewelrySalesSystem_NoName_FE.DTOs.Account;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace JewelrySalesSystem_NoName_FE.Pages.Auth
{
    [Authorize]
    public class UpdateProfileModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateProfileModel(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public AccountProfileDTO accountProfile { get; set; }

        [TempData]
        public string OriginalAccountProfileJson { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            var client = _httpClientFactory.CreateClient();
            var url = $"{ApiPath.Profile}";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                accountProfile = JsonConvert.DeserializeObject<AccountProfileDTO>(content);

                OriginalAccountProfileJson = JsonConvert.SerializeObject(accountProfile);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Error retrieving profile: {errorContent}");
                return Page();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!string.IsNullOrEmpty(OriginalAccountProfileJson))
            {
                var OriginalAccountProfile = JsonConvert.DeserializeObject<AccountProfileDTO>(OriginalAccountProfileJson);

                accountProfile.FullName = string.IsNullOrEmpty(accountProfile.FullName) ? OriginalAccountProfile.FullName : accountProfile.FullName;
                accountProfile.Dob = accountProfile.Dob == null ? OriginalAccountProfile.Dob : accountProfile.Dob;
                accountProfile.Address = string.IsNullOrEmpty(accountProfile.Address) ? OriginalAccountProfile.Address : accountProfile.Address;
                accountProfile.ImgUrl = string.IsNullOrEmpty(accountProfile.ImgUrl) ? OriginalAccountProfile.ImgUrl : accountProfile.ImgUrl;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Original account profile is missing.");
                return Page();
            }

            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            var client = _httpClientFactory.CreateClient();
            var url = $"{ApiPath.ProfileUpdate}";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonConvert.SerializeObject(accountProfile), Encoding.UTF8, "application/json");
            var response = await client.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Profile");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Error updating profile: {errorContent}");
                return Page();
            }
        }
    }
}
