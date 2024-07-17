using Firebase.Storage;
using JewelrySalesSystem_NoName_FE.DTOs.Account;
using JewelrySalesSystem_NoName_FE.DTOs.Role;
using JewelrySalesSystem_NoName_FE.Pages.Admin.Account;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Admin.Account
{
    public class EditAccountModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly string _bucket;

        [BindProperty]
        public AccountDAO Account { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }
        public IList<RoleDAO> RoleList { get; set; } = new List<RoleDAO>();

        public EditAccountModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _bucket = _configuration["Firebase:Bucket"];
        }

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

                var apiUrl = $"{ApiPath.AccountList}/id?id={id}";
                var response = await client.GetAsync(apiUrl);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Truy cập không hợp lệ. Vui lòng đăng nhập lại.";
                    return RedirectToPage("/Auth/Login");
                }

                Account = JsonConvert.DeserializeObject<AccountDAO>(await response.Content.ReadAsStringAsync());

                await LoadRoleListAsync(client);

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

            const long MAX_ALLOWED_SIZE = 1024 * 1024 * 100;

            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                if (Image != null && Image.Length > MAX_ALLOWED_SIZE)
                {
                    ModelState.AddModelError(string.Empty, "Tệp tải lên quá lớn.");
                    await LoadRoleListAsync(client);
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
                    Status = Account.Status,
                };

                var json = JsonConvert.SerializeObject(accountRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var apiUrl = $"{ApiPath.AccountList}/id?id={id}";
                var response = await client.PutAsync(apiUrl, content);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Truy cập không hợp lệ. Vui lòng đăng nhập lại.";
                    return RedirectToPage("/Auth/Login");
                }

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Tài khoản đã được chỉnh sửa thành công!";
                    return RedirectToPage("./ListAccount");
                }
                else
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Đã xảy ra lỗi khi cập nhật tài khoản. Mã trạng thái: {response.StatusCode}, Phản hồi: {responseBody}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Đã xảy ra lỗi: {ex.Message}");
            }

            var clientForRoles = _httpClientFactory.CreateClient("ApiClient");
            clientForRoles.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            await LoadRoleListAsync(clientForRoles);

            return Page();
        }

        private async Task LoadRoleListAsync(HttpClient client)
        {
            var roleApiUrl = $"{ApiPath.RoleList}";
            var roleResponse = await client.GetAsync(roleApiUrl);
            RoleList = JsonConvert.DeserializeObject<List<RoleDAO>>(await roleResponse.Content.ReadAsStringAsync());
        }
    }
}
