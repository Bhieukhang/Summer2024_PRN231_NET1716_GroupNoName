using JewelrySalesSystem_NoName_FE.DTOs.Discounts;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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

        public IActionResult OnGetPartialView()
        {
            return new PartialViewResult
            {
                ViewName = "_DiscountOrder",
                ViewData = new ViewDataDictionary<DiscountOrderModel>(ViewData, this)
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _clientFactory.CreateClient();
            var jsonContent = JsonSerializer.Serialize(DiscountRequest);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{ApiPath.DiscountConfirm}", content);

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

    
}
