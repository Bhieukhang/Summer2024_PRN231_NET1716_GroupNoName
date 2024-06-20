using JewelrySalesSystem_NoName_FE.DTOs.Account;
using JewelrySalesSystem_NoName_FE.DTOs.Transactions;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Transactions
{
    public class ListTransactionModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ListTransactionModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public List<TransactionDTO> Transactions { get; private set; } = new();
        public int TotalPages { get; private set; } = 0;
        public int CurrentPage { get; private set; } = 1;
        public int PageSize { get; private set; } = 5;
        public int TotalRecord { get; private set; } = 0;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TransactionOrderDTO tran { get; set; } = new TransactionOrderDTO();

        public async Task OnGet(int? currentPage, int? pageSize, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                CurrentPage = currentPage ?? 1;
                PageSize = pageSize ?? 5;
                StartDate = startDate.HasValue ? startDate.Value : DateTime.Now.AddYears(-1);
                EndDate = endDate.HasValue ? endDate.Value : DateTime.Now;


                var token = HttpContext.Session.GetString("Token") ?? "";
                var transactions = await ApiClient.GetAsync<List<TransactionDTO>>($"{ApiPath.Transaction}", token);
                transactions = transactions.ToList();

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

        public async Task<IActionResult> OnGetDetailsAsync(Guid orderId)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            var url = $"{ApiPath.TransactionOrder}?orderId={orderId}";
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetStringAsync(url);

                tran = JsonConvert.DeserializeObject<TransactionOrderDTO>(response);
                return (IActionResult)tran;
                //return Partial("_ProfilePartial", tran);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
