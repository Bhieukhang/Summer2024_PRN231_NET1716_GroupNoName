using JewelrySalesSystem_NoName_FE.DTOs.Product;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Staff.Orders
{
    public class AddOrderModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public AddOrderModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<JsonResult> OnGetProductAsync(string productCode)
        {
            var apiUrl = $"{ApiPath.ProductCodeGetListPromoton}?productCode={productCode}";
            var response = await _httpClient.GetStringAsync(apiUrl);
            var product = JsonConvert.DeserializeObject<ProductResponse>(response);
            return new JsonResult(product);
        }
    }
}
