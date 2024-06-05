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
                            new Claim(ClaimTypes.Name, jwtToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value ?? LoginRequest.Phone),
                            new Claim("Token", loginResponse.Token),
                            new Claim("ImgUrl", jwtToken.Claims.FirstOrDefault(c => c.Type == "imgUrl")?.Value ?? string.Empty),
                            new Claim(ClaimTypes.Role, jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "User")
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                        return RedirectToPage("/Manager/Products/ListProduct");
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
