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
                var response = await _httpClient.PostAsJsonAsync("https://localhost:44318/api/authentication/login", LoginRequest);
                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

                    if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.Token))
                    {
                        // L?u token v�o session ho?c cookie
                        HttpContext.Session.SetString("Token", loginResponse.Token);

                        // Chuy?n h??ng t?i trang kh�c sau khi ??ng nh?p th�nh c�ng
                        return RedirectToPage("/Index");
                    }

                    Message = loginResponse.Message;
                }
                else
                {
                    Message = "??ng nh?p th?t b?i. Vui l�ng th? l?i.";
                }
            }
            catch (HttpRequestException ex)
            {
                // X? l� l?i k?t n?i
                Message = $"L?i: {ex.Message}";
            }

            return Page();
        }
    }
}
