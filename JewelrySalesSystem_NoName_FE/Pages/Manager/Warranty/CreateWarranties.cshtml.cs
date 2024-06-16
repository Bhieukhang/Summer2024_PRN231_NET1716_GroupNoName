using JewelrySalesSystem_NoName_FE.DTOs;
using JewelrySalesSystem_NoName_FE.DTOs.Orders;
using JewelrySalesSystem_NoName_FE.DTOs.Warranty;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Drawing;
using System.Text;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Warranty
{
    public class CreateWarrantiesModel : PageModel
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateWarrantiesModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public WarrantyCreate Warranty { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<OrderDetailResponse> ListOrder { get; set; } = new List<OrderDetailResponse>();
        public IList<ConditionWarrantyDTO> ConditionWarranties { get; set; } = new List<ConditionWarrantyDTO>();
        public string orderIdToAPI;
        [FromQuery(Name = "phone")]
        public string Phone { get; set; }
        [FromQuery(Name = "orderId")]
        public string OrderId { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {

            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            var url = $"{ApiPath.ConditionWarrantyList}?page=1&size=100";
            var apiUrl = $"{ApiPath.OrderListDetail}?id={OrderId}";
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetStringAsync(apiUrl);
                var result = JsonConvert.DeserializeObject<OrderDetailList>(response);
                ListOrder = result.ListOrder;

                var responseCondition = await client.GetStringAsync(url); 
                var resultCondition = JsonConvert.DeserializeObject<Paginate<ConditionWarrantyDTO>>(responseCondition);
                ConditionWarranties = resultCondition.Items;
                orderIdToAPI = OrderId;

                TempData["ListOrder"] = JsonConvert.SerializeObject(ListOrder);
                TempData["ConditionWarranties"] = JsonConvert.SerializeObject(ConditionWarranties);
            }
            catch (Exception ex)
            {
                return Page();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
           
            try
            {
                var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToPage("/Auth/Login");
                }

                var client = _httpClientFactory.CreateClient();
                client.Timeout = TimeSpan.FromMinutes(5);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var serializedWarranties = Request.Form["SerializedWarranties"];
                var customerPhone = Request.Form["CustomerPhone"];
                var warranties = JsonConvert.DeserializeObject<List<WarrantyCreate>>(serializedWarranties);

                var apiUrlCreate = $"{ApiPath.Warranty}?phone={Phone}";


                var jsonContent = new StringContent(JsonConvert.SerializeObject(warranties), Encoding.UTF8, "application/json");
                Console.WriteLine(JsonConvert.SerializeObject(warranties));
                var response = await client.PostAsync(apiUrlCreate, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return Redirect("~/Staff/Orders/OrderBill");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error sending order to backend.");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error sending order to backend.");
                return Page();
            }
        }
    }
}
