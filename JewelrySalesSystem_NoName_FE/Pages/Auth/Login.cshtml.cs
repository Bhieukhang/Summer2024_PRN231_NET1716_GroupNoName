using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

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

                        var handler = new JwtSecurityTokenHandler();
                        var jwtToken = handler.ReadJwtToken(loginResponse.Token);

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, jwtToken.Claims.FirstOrDefault(c => c.Type == "phone")?.Value ?? LoginRequest.Phone),
                            new Claim("Token", loginResponse.Token),
                            new Claim("ImgUrl", jwtToken.Claims.FirstOrDefault(c => c.Type == "ImgUrl")?.Value ?? string.Empty),
                            new Claim(ClaimTypes.Name, jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? string.Empty),
                            new Claim(ClaimTypes.Role, jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "User")
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                        // Check the role and redirect accordingly
                        if (await GetRoleAsync() == "Admin")
                        {
                            return RedirectToPage("/Admin/Statics/Dashboard");
                        }
                        else if (await GetRoleAsync() == "Manager")
                        {
                            return RedirectToPage("/Manager/Statics/Dashboard");
                        }
                        else if (await GetRoleAsync() == "Staff")
                        {
                            return RedirectToPage("/Manager/Products/ListProduct");
                        }
                    }
                    else
                    {
                        Message = "Đăng nhập thất bại. Hãy thử lại!";
                    }
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Message = $"Đăng nhập thất bại: {errorMessage}";
                }
            }
            catch (HttpRequestException ex)
            {
                Message = $"Error: {ex.Message}";
            }

            return Page();
        }

        private async Task<string> GetRoleAsync()
        {
            var token = HttpContext.Session.GetString("Token");
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            return jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? string.Empty;
        }
    }
}
