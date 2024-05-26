using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JewelrySalesSystem_NoName_FE.Pages.Auth
{
    public class LogoutModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public LogoutModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var response = await _httpClient.PostAsync("https://localhost:44318/api/v1/Logout", null);
                if (response.IsSuccessStatusCode)
                {
                    // Xóa token kh?i session ho?c cookie
                    HttpContext.Session.Remove("Token");

                    // Chuy?n h??ng ??n trang ??ng nh?p ho?c trang ch?
                    return RedirectToPage("/Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Logout failed. Please try again.");
                }
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
            }

            return Page();
        }
    }
}
