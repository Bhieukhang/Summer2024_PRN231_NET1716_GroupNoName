using JewelrySalesSystem_NoName_FE.Ultils;
using JewelrySalesSystem_NoName_FE.DTOs.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JewelrySalesSystem_NoName_FE.Pages.Staff.Orders
{
    public class ListOrderModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty]
        public string SearchCode { get; set; }

        public IList<OrderDTO> OrderList { get; set; } = new List<OrderDTO>();
        public int TotalOrderCount { get; set; }

        public ListOrderModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            await LoadOrderListAsync();
        }

        private async Task LoadOrderListAsync()
        {
            var client = _httpClientFactory.CreateClient();

            var apiUrl = $"{ApiPath.GetListOrders}";
            var response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                OrderList = JsonConvert.DeserializeObject<IList<OrderDTO>>(jsonResponse);
                TotalOrderCount = OrderList.Count;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error retrieving order data. Please try again later.");
            }
        }

        public async Task OnPostSearchAsync()
        {
            if (string.IsNullOrEmpty(SearchCode))
            {
                ModelState.AddModelError(string.Empty, "Please enter an order code to search.");
                return;
            }

            var client = _httpClientFactory.CreateClient();

            var apiUrl = $"{ApiPath.OrderByID}?id={SearchCode}";
            var response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                OrderList = JsonConvert.DeserializeObject<IList<OrderDTO>>(jsonResponse);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error retrieving order data. Please try again later.");
            }
        }
    }
}
