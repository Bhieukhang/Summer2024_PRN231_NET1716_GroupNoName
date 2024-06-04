using JewelrySalesSystem_NoName_FE.DTOs.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace JewelrySalesSystem_NoName_FE.Pages.Staff.Orders
{
    public class AddOrderModel : PageModel
    {
        [BindProperty]
        public OrderDTO Order { get; set; } = new OrderDTO();

        public void OnGet()
        {
            // Initialize Order with default values if needed
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            // Calculate totalPrice here if needed
            Order.TotalPrice = CalculateTotalPrice(Order.Details);

            // Send Order to API backend
            var apiResponse = await SendOrderToApiAsync(Order);

            if (apiResponse.IsSuccessStatusCode)
            {
                // Handle successful response
                return RedirectToPage("OrderSuccess");
            }
            else
            {
                // Handle error response
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

        private async Task<HttpResponseMessage> SendOrderToApiAsync(OrderDTO order)
        {
            using (var client = new HttpClient())
            {
                // Replace with your actual API endpoint
                var apiUrl = "YOUR_API_ENDPOINT";
                var jsonContent = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");
                return await client.PostAsync(apiUrl, jsonContent);
            }
        }
    }
    public class OrderDTO
    {
        public string CustomerId { get; set; }
        public string PromotionId { get; set; }
        public string DiscountId { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal MaterialProcessPrice { get; set; }
        public List<OrderDetailDTO> Details { get; set; } = new List<OrderDetailDTO>();
    }

    public class OrderDetailDTO
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
    }
}
