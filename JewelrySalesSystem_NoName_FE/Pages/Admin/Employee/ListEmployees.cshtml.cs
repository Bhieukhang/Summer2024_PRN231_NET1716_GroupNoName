using JewelrySalesSystem_NoName_FE.DTOs.Account;
using JewelrySalesSystem_NoName_FE.DTOs;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using JewelrySalesSystem_NoName_FE.DTOs.Role;
using Microsoft.AspNetCore.Authorization;
using System.Drawing;
using Newtonsoft.Json.Linq;

namespace JewelrySalesSystem_NoName_FE.Pages.Admin.Employee
{
    [Authorize(Roles = "Admin")]
    public class ListEmployeesModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ListEmployeesModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public string? SearchName { get; set; }
        public IList<AccountDAO> ListEmployees { get; set; } = new List<AccountDAO>();
        public IList<RoleDAO> roleList { get; set; } = new List<RoleDAO>();
        public Guid RoleId { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int TotalAccountCount { get; set; }
        public int ActiveAccountCount { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? roleId, int? page, int? size)
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            await LoadEmployeeListAsync(token, roleId, page, size);
            return Page();
        }

        public async Task<IActionResult> OnPostSearchAsync()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "You need to login first.";
                return RedirectToPage("/Auth/Login");
            }

            //if (string.IsNullOrEmpty(SearchName))
            //{
            //    await LoadEmployeeListAsync();
            //    return Page();
            //}

            if (string.IsNullOrEmpty(SearchName))
            {
                return RedirectToPage("/Admin/Employee/ListEmployees");
            }

            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var apiUrl = $"{ApiPath.EmployeesList}/name?name={SearchName}";
                var response = await client.GetAsync(apiUrl);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Unauthorized access. Please login again.";
                    return RedirectToPage("/Auth/Login");
                }

                if (response.IsSuccessStatusCode)
                {
                    var employee = JsonConvert.DeserializeObject<AccountDAO>(await response.Content.ReadAsStringAsync());
                    ListEmployees = new List<AccountDAO> { employee };

                    var roleResponse = await client.GetAsync(ApiPath.RoleList);
                    if (roleResponse.IsSuccessStatusCode)
                    {
                        roleList = JsonConvert.DeserializeObject<List<RoleDAO>>(await roleResponse.Content.ReadAsStringAsync());
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee not found.");
                    await LoadEmployeeListAsync(token, new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae8"), Page, Size);
                    return Page();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                await LoadEmployeeListAsync(token, new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae8"), Page, Size);
                return Page();
            }

            return Page();
        }

        private async Task LoadEmployeeListAsync(string token, Guid? roleId, int? page, int? size)
        {
            RoleId = roleId ?? new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae8");
            Page = page ?? 1;
            Size = size ?? 10;

            var url = $"{ApiPath.EmployeesList}?roleId={RoleId}&page={Page}&size={Size}";
            var totalCountUrl = $"{ApiPath.TotalAccount}";
            var activeCountUrl = $"{ApiPath.ActiveAccount}";
            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetStringAsync(url);

                var paginateResult = JsonConvert.DeserializeObject<Paginate<AccountDAO>>(response);
                ListEmployees = paginateResult.Items;
                TotalItems = paginateResult.Total;
                TotalPages = paginateResult.TotalPages;

                TotalAccountCount = await client.GetFromJsonAsync<int>(totalCountUrl);
                ActiveAccountCount = await client.GetFromJsonAsync<int>(activeCountUrl);

                var roleResponse = await client.GetAsync(ApiPath.RoleList);
                if (roleResponse.IsSuccessStatusCode)
                {
                    roleList = JsonConvert.DeserializeObject<List<RoleDAO>>(await roleResponse.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                // Handle error appropriately
                ListEmployees = new List<AccountDAO>();
                TotalAccountCount = 0;
                ActiveAccountCount = 0;
            }
        }
    }
}
