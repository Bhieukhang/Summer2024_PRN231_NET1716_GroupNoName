using JewelrySalesSystem_NoName_FE.DTOs.Membership;
using JewelrySalesSystem_NoName_FE.DTOs;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using JewelrySalesSystem_NoName_FE.DTOs.Account;
using Microsoft.AspNetCore.Authorization;
using JewelrySalesSystem_NoName_FE.DTOs.Role;
using System.Net.Http.Headers;

namespace JewelrySalesSystem_NoName_FE.Pages.Admin.Account
{
    [Authorize(Roles = "Admin")]
    public class ListAccountModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ListAccountModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IList<AccountDAO> ListAccount { get; set; } = new List<AccountDAO>();
        public IList<RoleDAO> RoleList { get; set; } = new List<RoleDAO>();
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int TotalAccountCount { get; set; }
        public int ActiveAccountCount { get; set; }

        public string SearchTerm { get; set; }
        public Guid? FilterRoleId { get; set; }
        public bool? FilterDeflag { get; set; }

        //private readonly Guid excludedRoleId = Guid.Parse("7C9E6679-7425-40DE-944B-E07FC1F90AE9");

        public async Task<IActionResult> OnGetAsync(int? currentPage, string searchTerm, Guid? filterRoleId, bool? filterDeflag)
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            Page = currentPage ?? 1;
            Size = 2;
            SearchTerm = searchTerm;
            FilterRoleId = filterRoleId;
            FilterDeflag = filterDeflag;

            var url = $"{ApiPath.AccountList}?page={Page}&size={Size}";

            if (!string.IsNullOrEmpty(SearchTerm) || FilterRoleId.HasValue || FilterDeflag.HasValue)
            {
                url = $"{ApiPath.FilteredAccounts}?page={Page}&size={Size}";

                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    url += $"&searchTerm={SearchTerm}";
                }
                if (FilterRoleId.HasValue)
                {
                    url += $"&roleId={FilterRoleId}";
                }
                if (FilterDeflag.HasValue)
                {
                    url += $"&deflag={FilterDeflag}";
                }
            }

            var totalCountUrl = $"{ApiPath.TotalAccount}";
            var activeCountUrl = $"{ApiPath.ActiveAccount}";

            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetStringAsync(url);
                var paginateResult = JsonConvert.DeserializeObject<Paginate<AccountDAO>>(response);
                ListAccount = paginateResult.Items.Where(x => !AccountDAO.ExcludedRoleIds.Contains(x.RoleId)).ToList();
                TotalItems = paginateResult.Total;
                TotalPages = paginateResult.TotalPages;

                TotalAccountCount = await client.GetFromJsonAsync<int>(totalCountUrl);
                ActiveAccountCount = await client.GetFromJsonAsync<int>(activeCountUrl);

                var roleApiUrl = $"{ApiPath.RoleList}";
                var roleResponse = await client.GetAsync(roleApiUrl);
                RoleList = JsonConvert.DeserializeObject<List<RoleDAO>>(await roleResponse.Content.ReadAsStringAsync());

                RoleList = RoleList.Where(r => !AccountDAO.ExcludedRoleIds.Contains(r.Id)).ToList();

                foreach (var account in ListAccount)
                {
                    var role = RoleList.FirstOrDefault(r => r.Id == account.RoleId);
                    if (role != null)
                    {
                        account.RoleName = role.RoleName;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle error appropriately
                ListAccount = new List<AccountDAO>();
                TotalAccountCount = 0;
                ActiveAccountCount = 0;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeactivateAccountAsync(Guid accountId)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            var url = $"{ApiPath.UpdateDeflagAccount}/{accountId}";
            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.PutAsync(url, null);
                if (response.IsSuccessStatusCode)
                {
                    //return RedirectToPage(new { currentPage = Page, searchTerm = SearchTerm, filterRoleId = FilterRoleId, filterDeflag = FilterDeflag });
                    return RedirectToPage("/Admin/Account/ListAccount");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to deactivate account.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deactivating the account.");
            }

            //return await OnGetAsync(Page, SearchTerm, FilterRoleId, FilterDeflag);
            return RedirectToPage("/Admin/Account/ListAccount");
        }
    }
}
