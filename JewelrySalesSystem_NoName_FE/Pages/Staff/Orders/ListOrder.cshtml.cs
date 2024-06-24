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
        public OrderForCustomer CustomerOrder { get; set; }
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

            //if (!string.IsNullOrEmpty(SearchCriteria.CustomerPhone))
            //{
            //    filteredOrders = filteredOrders.Where(o => o.CustomerPhone == SearchCriteria.CustomerPhone);
            //}

            if (SearchCriteria.StartDate.HasValue)
            {
                filteredOrders = filteredOrders.Where(o => o.InsDate >= SearchCriteria.StartDate.Value);
            }

            FilteredOrderList = filteredOrders.ToList();
        }

        public async Task<IActionResult> OrderCustomer(string phone)
        {
            var token = HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var apiUrl = $"{ApiPath.SearchOrderCustomer}?phone={phone}";
            var response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                CustomerOrder = JsonConvert.DeserializeObject<OrderForCustomer>(jsonResponse);
                return new JsonResult(new { success = true, data = CustomerOrder });
            }
            else
            {
                return new JsonResult(new { success = false, message = "Khách hàng hiện tại không có đơn hàng đã mua" });
            }
        }

        public async Task<IActionResult> OnGetFilterOrderAsync(string phone)
        {
            var token = HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var apiUrl = $"{ApiPath.SearchOrderCustomer}?phone={phone}";
            var response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                CustomerOrder = JsonConvert.DeserializeObject<OrderForCustomer>(jsonResponse);
                return new JsonResult(new { success = true, data = CustomerOrder });
            }
            else
            {
                return new JsonResult(new { success = false, message = "Khách hàng hiện tại không có đơn hàng đã mua" });
            }
        }
    }
    public class SearchCriteriaDTO
    {
        public Guid? OrderId { get; set; }
        public Guid? CustomerId { get; set; }
        public DateTime? StartDate { get; set; }
    }
}

