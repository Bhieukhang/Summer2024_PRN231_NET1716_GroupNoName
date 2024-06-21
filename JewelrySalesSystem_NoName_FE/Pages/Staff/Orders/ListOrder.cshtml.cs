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
        public IList<OrderDTO> FilteredOrderList { get; set; } = new List<OrderDTO>();

        [BindProperty(SupportsGet = true)]
        public SearchCriteriaDTO SearchCriteria { get; set; } = new SearchCriteriaDTO();

        public ListOrderModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            await LoadOrderListAsync();
            ApplyFilters();
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
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error retrieving order data. Please try again later.");
            }
        }

        private void ApplyFilters()
        {
            var filteredOrders = OrderList.AsQueryable();

            if (SearchCriteria.OrderId.HasValue)
            {
                filteredOrders = filteredOrders.Where(o => o.Id == SearchCriteria.OrderId.Value);
            }

            //if (SearchCriteria.CustomerId.HasValue)
            //{
            //    filteredOrders = filteredOrders.Where(o => o.CustomerId == SearchCriteria.CustomerId.Value);
            //}

            if (SearchCriteria.StartDate.HasValue)
            {
                filteredOrders = filteredOrders.Where(o => o.InsDate >= SearchCriteria.StartDate.Value);
            }

            FilteredOrderList = filteredOrders.ToList();
        }
    }

    public class SearchCriteriaDTO
    {
        public Guid? OrderId { get; set; }
        public Guid? CustomerId { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
