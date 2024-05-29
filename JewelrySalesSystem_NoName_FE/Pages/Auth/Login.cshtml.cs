using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JewelrySalesSystem_NoName_FE.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public LoginModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty]
        public LoginRequest LoginRequest { get; set; }

        public string Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var url = $"{ApiPath.Login}";
                var response = await _httpClient.PostAsJsonAsync(url, LoginRequest);
                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

                    if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.Token))
                    {
                        HttpContext.Session.SetString("Token", loginResponse.Token);
                        return RedirectToPage("/Index");
                    }

                    Message = "Sign in failed. Please try again!";
                }
                else
                {
                    Message = "Sign in failed. Please try again!";
                }
            }
            catch (HttpRequestException ex)
            {
                Message = $"Error: {ex.Message}";
            }

            return Page();
        }
    }
}
