using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.PriceGold
{
    public class PriceGoldModel : PageModel
    {
        private readonly GoldPriceService _goldPriceService;
        private readonly ILogger<PriceGoldModel> _logger;

        public PriceGoldModel(GoldPriceService goldPriceService, ILogger<PriceGoldModel> logger)
        {
            _goldPriceService = goldPriceService;
            _logger = logger;
        }

        public decimal GoldPrice { get; private set; }
        public bool HasError { get; private set; }
        public DateTime SelectedDate { get; private set; }

        public async Task OnGetAsync(DateTime? date)
        {
            if (date.HasValue)
            {
                SelectedDate = date.Value;
                try
                {
                    GoldPrice = await _goldPriceService.GetGoldPriceAsync(SelectedDate);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to fetch gold price.");
                    HasError = true;
                }
            }
        }
    }
}
