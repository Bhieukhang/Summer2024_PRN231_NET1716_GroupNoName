using JewelrySalesSystem_NoName_FE.DTOs.Account;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Account
{
    [Authorize(Roles = "Admin")]
    public class CreateAccountModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateAccountModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public AccountDAO CreateAccountDTO { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            var url = $"{ApiPath.AccountList}";
            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await client.PostAsJsonAsync("api/accounts", CreateAccountDTO);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("./ListAccount");
                }

                ModelState.AddModelError(string.Empty, "An error occurred while creating the account.");
            }
            catch (Exception ex)
            {
                // Handle error appropriately
                ModelState.AddModelError(string.Empty, "An error occurred: " + ex.Message);
            }

            return Page();
        }
    }
}
