using JewelrySalesSystem_NoName_FE.DTOs.Discounts;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JewelrySalesSystem_NoName_FE.Pages.Staff.Discounts
{
    public class ListDiscountModel : PageModel
    {
        public List<DiscountDTO> Discounts { get; private set; } = new();
        public int TotalPages { get; private set; } = 0;
        public int CurrentPage { get; private set; } = 1;
        public int PageSize { get; private set; } = 5;
        public int TotalRecord { get; private set; } = 0;
        public string? Search { get; private set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int? currentPage, int? pageSize, string? search)
        {
            try
            {
                CurrentPage = currentPage ?? 1;
                PageSize = pageSize ?? 5;
                Search = search;

                var token = HttpContext.Session.GetString("Token") ?? "";
                var promotions = await ApiClient.GetAsync<List<DiscountDTO>>($"{ApiPath.Discount}?search={Search}", token);

                // Calculate pages
                TotalRecord = promotions.Count;
                TotalPages = (int)Math.Ceiling(TotalRecord / (double)PageSize);
                promotions = promotions.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                Discounts = promotions;
            }
            catch (Exception ex)
            {
                Discounts = new();
                Console.WriteLine($"Error: {ex}");
            }

            return Page();
        }
    }
}
