using JewelrySalesSystem_NoName_FE.Ultils;
using JewelrySalesSystem_NoName_FE.DTOs.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JewelrySalesSystem_NoName_FE.Pages.Staff.Orders
{
    public class ListOrderModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IList<OrderDTO> OrderList { get; set; } = new List<OrderDTO>();
        public OrderForCustomer CustomerOrder { get; set; }
        [BindProperty(SupportsGet = true)]
        public Guid? OrderId { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? InsDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Phone { get; set; }

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
            string apiUrl;

            if (OrderId.HasValue || InsDate.HasValue || !string.IsNullOrEmpty(Phone))
            {
                apiUrl = $"{ApiPath.SearchOrders}";

                var queryParameters = new List<string>();
                if (OrderId.HasValue)
                {
                    queryParameters.Add($"id={OrderId.Value}");
                }
                if (InsDate.HasValue)
                {
                    queryParameters.Add($"insDate={InsDate.Value:yyyy-MM-dd}");
                }
                if (!string.IsNullOrEmpty(Phone))
                {
                    queryParameters.Add($"phone={Phone}");
                }

                if (queryParameters.Any())
                {
                    apiUrl += "?" + string.Join("&", queryParameters);
                }
            }
            else
            {
                apiUrl = $"{ApiPath.GetListOrders}";
            }

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

