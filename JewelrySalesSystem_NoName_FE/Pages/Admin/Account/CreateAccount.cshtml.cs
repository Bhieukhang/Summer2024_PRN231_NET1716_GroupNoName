using Firebase.Storage;
using JewelrySalesSystem_NoName_FE.DTOs.Account;
using JewelrySalesSystem_NoName_FE.DTOs.Role;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Admin.Account
{
    public class CreateAccountModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly string _bucket;

        [BindProperty]
        public AccountDAO Account { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }
        public IList<RoleDAO> RoleList { get; set; } = new List<RoleDAO>();

        public CreateAccountModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _bucket = _configuration["Firebase:Bucket"];
        }

        public async Task<IActionResult> OnGetAsync()
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

                var roleApiUrl = $"{ApiPath.RoleList}";
                var response = await client.GetAsync(roleApiUrl);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Unauthorized access. Please login again.";
                    return RedirectToPage("/Auth/Login");
                }

                RoleList = JsonConvert.DeserializeObject<List<RoleDAO>>(await response.Content.ReadAsStringAsync());

                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "You need to login first.";
                return RedirectToPage("/Auth/Login");
            }

            const long MAX_ALLOWED_SIZE = 1024 * 1024 * 100;

            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

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
                        Account.ImgUrl = await uploadTask;

                        stream.Seek(0, SeekOrigin.Begin);
                        Account.ImgUrl = Convert.ToBase64String(stream.ToArray());
                    }
                }

                Account.Id = Guid.NewGuid();
                Account.InsDate = DateTime.Now;
                Account.UpsDate = DateTime.Now;
                Account.Deflag = true;

                var accountRequest = new AccountDAO
                {
                    FullName = Account.FullName,
                    Phone = Account.Phone,
                    Dob = Account.Dob,
                    Password = Account.Password,
                    Address = Account.Address,
                    ImgUrl = Account.ImgUrl,
                    Deflag = Account.Deflag,
                    RoleId = Account.RoleId,
                    InsDate = Account.InsDate
                };

                var json = JsonConvert.SerializeObject(accountRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var apiUrl = $"{ApiPath.AccountList}";
                var response = await client.PostAsync(apiUrl, content);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Unauthorized access. Please login again.";
                    return RedirectToPage("/Auth/Login");
                }

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "The new account is created successfully!";
                    return RedirectToPage("./ListAccount");
                }
                else
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"An error occurred while adding the product. Status Code: {response.StatusCode}, Response: {responseBody}");
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
