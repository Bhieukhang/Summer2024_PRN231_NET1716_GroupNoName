using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Staff.Orders
{
    public class DiscountOrderModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public DiscountOrderModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public DiscountRequest DiscountRequest { get; set; } = new DiscountRequest();

        public string ResponseMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _clientFactory.CreateClient();
            var jsonContent = JsonSerializer.Serialize(DiscountRequest);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:44318/api/v1/discount/confirm", content);

            if (response.IsSuccessStatusCode)
            {
                ResponseMessage = "Discount created successfully!";
            }
            else
            {
                ResponseMessage = $"Error: {response.ReasonPhrase}";
            }

            return Page();
        }
    }

    public class DiscountRequest
    {
        public string OrderId { get; set; }
        public string ManagerId { get; set; }
        public int PercentDiscount { get; set; }
        public string Description { get; set; }
        public int ConditionDiscount { get; set; }
    }
}
