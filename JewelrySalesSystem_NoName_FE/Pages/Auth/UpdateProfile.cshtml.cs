using JewelrySalesSystem_NoName_FE.DTOs.Account;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Firebase.Storage;

namespace JewelrySalesSystem_NoName_FE.Pages.Auth
{
    [Authorize]
    public class UpdateProfileModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly string _bucket;

        public UpdateProfileModel(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _bucket = _configuration["Firebase:Bucket"];
        }

        [BindProperty]
        public AccountProfileDTO accountProfile { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

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
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "You need to login first.";
                return RedirectToPage("/Auth/Login");
            }

            const long MAX_ALLOWED_SIZE = 1024 * 1024 * 100;

            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                if (Image != null && Image.Length > MAX_ALLOWED_SIZE)
                {
                    ModelState.AddModelError(string.Empty, "The uploaded file is too large.");
                    return Page();
                }

                if (Image != null)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                    var storage = new FirebaseStorage(_bucket);
                    using (var stream = new MemoryStream())
                    {
                        Image.CopyTo(stream);
                        stream.Seek(0, SeekOrigin.Begin);
                        var uploadTask = storage.Child("uploads").Child(uniqueFileName).PutAsync(stream);
                        accountProfile.ImgUrl = await uploadTask;
                    }
                }

                var accountRequest = new AccountProfileDTO
                {
                    FullName = accountProfile.FullName,
                    Dob = accountProfile.Dob,
                    Address = accountProfile.Address,
                    ImgUrl = accountProfile.ImgUrl, 
                    Password = accountProfile.Password,
                };

                var json = JsonConvert.SerializeObject(accountRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var url = $"{ApiPath.ProfileUpdate}";
                var response = await client.PutAsync(url, content);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Unauthorized access. Please login again.";
                    return RedirectToPage("/Auth/Login");
                }

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Profile updated successfully!";
                    return RedirectToPage("./Profile");
                }
                else
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"An error occurred while updating the profile. Status Code: {response.StatusCode}, Response: {responseBody}");
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
