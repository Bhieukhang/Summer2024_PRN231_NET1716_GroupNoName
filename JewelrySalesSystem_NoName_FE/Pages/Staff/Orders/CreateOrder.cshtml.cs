using JewelrySalesSystem_NoName_FE.DTOs.Account;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using JewelrySalesSystem_NoName_FE.DTOs.Promotions;
using JewelrySalesSystem_NoName_FE.DTOs.Orders;
using System.Text;

namespace JewelrySalesSystem_NoName_FE.Pages.Staff.Orders
{
    public class CreateOrderModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateOrderModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<PromotionDTO> ListPromotion { set; get; } = new List<PromotionDTO>();
        [BindProperty]
        public Guid CustomerID { set; get; }
        [BindProperty]
        public Guid PromotionId { set; get; }
        [BindProperty]
        public Guid DiscountId { set; get; }
        public double TotalPrice = 0;
        public double MatertailProcessPrice = 0;

        [BindProperty]
        public OrderDTO Order { get; set; } = new OrderDTO();
        public async Task<IActionResult> OnPostOrderAsync()
        {
            Order.TotalPrice = CalculateTotalPrice(Order.Details);
            Order.CustomerId = "F8D700A3-1CD8-4A98-89C1-EB27DE0AE5A6";
            var apiUrl = $"{ApiPath.OrderCreate}";
            try
            {
                var client = _httpClientFactory.CreateClient();
                var jsonContent = new StringContent(JsonConvert.SerializeObject(Order), Encoding.UTF8, "application/json");
                await client.PostAsync(apiUrl, jsonContent);
                return Redirect("/Staff/Orders/OrderSuccess");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error sending order to backend.");
                return Page();
            }

        }
        private decimal CalculateTotalPrice(List<OrderDetailDTO> details)
        {
            decimal total = 0;
            foreach (var detail in details)
            {
                total += detail.Amount * detail.Quantity;
            }
            return total;
        }


        public async Task<IActionResult> OnGetPromotionAsync()
        {
            var url = $"{ApiPath.Promotion}";
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetStringAsync(url);

                ListPromotion = JsonConvert.DeserializeObject<IList<PromotionDTO>>(response);
                return Partial("_ListPromotionApply", ListPromotion);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IActionResult OnGetDiscount()
        {
            return Partial("_DiscountRequirement");
        }
    }
}
