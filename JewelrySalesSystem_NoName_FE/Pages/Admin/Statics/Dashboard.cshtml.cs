using JewelrySalesSystem_NoName_FE.DTOs;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JewelrySalesSystem_NoName_FE.Pages.Admin.Statics
{
    public class DashboardModel : PageModel
    {
        [BindProperty]
        public DashboardDTO DashboardData { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var token = HttpContext.Session.GetString("Token") ?? "";
                DashboardData = await ApiClient.GetAsync<DashboardDTO>($"{ApiPath.Dashboard}", token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
            return Page();
        }
    }
}
