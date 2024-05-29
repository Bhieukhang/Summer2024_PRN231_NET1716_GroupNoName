using JewelrySalesSystem_NoName_FE.DTOs.Account;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace JewelrySalesSystem_NoName_FE.Pages.Auth
{
    public class UpdateProfileModel : PageModel
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public UpdateProfileModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public AccountProfileDTO accountProfile { get; set; }

        [TempData]
        public string OriginalAccountProfileJson { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var url = $"{ApiPath.Profile}";

            var token = HttpContext.Session.GetString("Token"); // Ho?c HttpContext.Request.Cookies["Token"]
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

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

            var client = _httpClientFactory.CreateClient();
            var url = $"{ApiPath.ProfileUpdate}";

            var token = HttpContext.Session.GetString("Token"); // Ho?c HttpContext.Request.Cookies["Token"]
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

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
