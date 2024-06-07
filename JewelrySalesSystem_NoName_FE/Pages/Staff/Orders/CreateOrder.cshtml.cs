using JewelrySalesSystem_NoName_FE.DTOs.Account;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using JewelrySalesSystem_NoName_FE.DTOs.Promotions;
using JewelrySalesSystem_NoName_FE.DTOs.Orders;
using System.Text;
using JewelrySalesSystem_NoName_FE.Requests.Promotions;
using JewelrySalesSystem_NoName_FE.Responses;
using JewelrySalesSystem_NoName_FE.Pages.Manager.Products;

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
            Order.CustomerId = Guid.Parse("F8D700A3-1CD8-4A98-89C1-EB27DE0AE5A6");
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
        private double CalculateTotalPrice(List<OrderDetailDTO> details)
        {
            double total = 0;
            foreach (var detail in details)
            {
                total += detail.Amount * detail.Quantity;
            }
            return total;
        }


        public async Task<IActionResult> OnGetPromotionAsync(string order)
        {
            var url = $"{ApiPath.Promotion}";
            var token = _httpContextAccessor.HttpContext.Session.GetString("token");
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetStringAsync(url);

                ListPromotion = JsonConvert.DeserializeObject<IList<PromotionDTO>>(response);

                // Pass the order along with the promotion list
                ViewData["Order"] = JsonConvert.DeserializeObject<OrderDTO>(order);
                return Partial("_ListPromotionApply", ListPromotion);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        public IActionResult OnGetDiscount()
        {
            try
            {
                // Log để kiểm tra phương thức đã được gọi
                Console.WriteLine("OnGetDiscount called");

                // Giả sử bạn có một partial view tên là _DiscountRequirement
                return Partial("_DiscountRequirements");
            }
            catch (Exception ex)
            {
                // Log lỗi để kiểm tra nguyên nhân gây ra lỗi
                Console.WriteLine($"Error in OnGetDiscount: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync([FromBody] OrderDTO order)
        {
            // Log to check the method call
            Console.WriteLine("Order: " + JsonConvert.SerializeObject(order));

            var apiUrl = $"{ApiPath.OrderCheckPromotion}";

            OrderDetailDTO o = new OrderDetailDTO()
            {
                Amount = 0,
                Quantity = 1,
                ProductId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            };
            order.Details.Add(o);

            //var token = HttpContext.Session.GetString("Token");

            //var jsonContent = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");
            //var response = await ApiClient.PostAsync<ApiResponse>(apiUrl, order, token ?? "");
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Response error: " + response.ReasonPhrase);
                throw new Exception("Lỗi");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response: " + responseData);

            return new JsonResult(responseData);
            //var client = _httpClientFactory.CreateClient();
            //var jsonContent = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");
            //var response = await client.PostAsync(apiUrl, jsonContent);

            // Log the status code
            //Console.WriteLine("Response Status Code: " + response.StatusCode);

            //var responseContent = await response.Content.ReadAsStringAsync();

            //// Log the raw response content
            //Console.WriteLine("Response Content: " + response);

            //if (!response.IsCompletedSuccessfully)
            //{
            //    return StatusCode((int)response.Status, "Error from API: " + response);
            //}

            //bool isValid;
            //try
            //{
            //    isValid = JsonConvert.DeserializeObject<bool>( response);
            //}
            //catch (JsonReaderException jsonEx)
            //{
            //    Console.WriteLine($"JSON Deserialization Error: {jsonEx.Message}");
            //    return BadRequest("Invalid response format from the API");
            //}

            //if (isValid)
            //{
            //    return new JsonResult(new { success = true, message = "Promotion applied successfully." });
            //}
            //else
            //{
            //    return new JsonResult(new { success = false, message = "Promotion is not valid for this order." });
            //}

            //}
            //catch (Exception ex)
            //{
            //    // Log error to check the cause of the error
            //    Console.WriteLine($"Error in OnPostCheckAsync: {ex.Message}");
            //    return StatusCode(500, "Internal server error");
            //}
        }

        public async Task<IActionResult> OnPostCheckAsync([FromBody] OrderDTO order)
        {
            Console.WriteLine("Order: " + JsonConvert.SerializeObject(order));

            if (order.CustomerId == null)
            {
                order.CustomerId = Guid.Parse("00000000-0000-0000-0000-000000000000");
            }

            if (order.PromotionId == null)
            {
                order.PromotionId = Guid.NewGuid();
            }

            if (order.DiscountId == null)
            {
                order.DiscountId = Guid.Parse("00000000-0000-0000-0000-000000000000");
            }

            if (order.Details == null)
            {
                order.Details = new List<OrderDetailDTO>();
            }

            //foreach (var detail in order.Details)
            //{
             
            //    if (string.IsNullOrEmpty(detail.ProductId))
            //    {
            //        detail.ProductId = Guid.Parse("8381a026-c8df-404a-b4aa-3f5b08ca5070");
            //    }
            //}

            var apiUrl = $"{ApiPath.OrderCheckPromotion}";
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Response error: " + response.ReasonPhrase);
                throw new Exception("Lỗi");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response: " + responseData);

            return new JsonResult(responseData);
        }

    }
}
