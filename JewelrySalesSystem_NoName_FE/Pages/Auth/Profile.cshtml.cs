using JewelrySalesSystem_NoName_FE.DTOs.Account;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;

namespace JewelrySalesSystem_NoName_FE.Pages.Auth
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileModel(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public AccountProfileDTO accountProfile { get; set; }
        public string RoleName { get; set; }

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
                RoleName = GetRoleName(accountProfile.RoleId);

            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Error retrieving profile: {errorContent}");
                return Page();
            }
            return Page();
        }

        private string GetRoleName(Guid roleId)
        {
            // This is a simplified example. In a real application, you might retrieve these from a database or configuration.
            var roles = new Dictionary<Guid, string>
        {
            { new Guid("0F8FAD5B-D9CB-469F-A165-70867728950E"), "Admin" },
            { new Guid("7C9E6679-7425-40DE-944B-E07FC1F90AE7"), "Manager" },
            { new Guid("7C9E6679-7425-40DE-944B-E07FC1F90AE8"), "Staff" }
        };

            return roles.ContainsKey(roleId) ? roles[roleId] : "User";
        }
    }
}
