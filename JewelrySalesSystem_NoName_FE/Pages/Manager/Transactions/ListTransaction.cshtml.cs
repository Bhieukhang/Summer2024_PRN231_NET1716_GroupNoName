using JewelrySalesSystem_NoName_FE.DTOs.Transactions;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Transactions
{
    public class ListTransactionModel : PageModel
    {
        public List<TransactionDTO> Transactions { get; private set; } = new();
        public int TotalPages { get; private set; } = 0;
        public int CurrentPage { get; private set; } = 1;
        public int PageSize { get; private set; } = 5;
        public int TotalRecord { get; private set; } = 0;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public async Task OnGet(int? currentPage, int? pageSize, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                CurrentPage = currentPage ?? 1;
                PageSize = pageSize ?? 5;
                StartDate = startDate ?? DateTime.Now.AddYears(-1);
                EndDate = endDate ?? DateTime.Now;

                var token = HttpContext.Session.GetString("Token") ?? "";
                var transactions = await ApiClient.GetAsync<List<TransactionDTO>>($"{ApiPath.Transaction}", token);
                transactions = transactions.Where(x => x.Order.InsDate >= startDate && x.Order.InsDate <= endDate).ToList();

                // Calculate pages
                TotalRecord = transactions.Count;
                TotalPages = (int)Math.Ceiling(TotalRecord / (double)PageSize);
                transactions = transactions.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                Transactions = transactions;
            }
            catch (Exception ex)
            {
                Transactions = new();
                Console.WriteLine($"Error: {ex}");
            }
        }
    }
}
