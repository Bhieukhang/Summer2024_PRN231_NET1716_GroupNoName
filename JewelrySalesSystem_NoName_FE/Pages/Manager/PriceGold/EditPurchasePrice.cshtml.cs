using BusinessObjects.Mo;
using Firebase.Storage;
using JewelrySalesSystem_NoName_FE.DTOs.Product;
using JewelrySalesSystem_NoName_FE.Pages.Manager.Products;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.PriceGold
{
    public class EditPurchasePriceModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly string _bucket;

        [BindProperty]
        public PurchasePriceDTO PurchasePrice { get; set; }


        public EditPurchasePriceModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _bucket = _configuration["Firebase:Bucket"];
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "You need to login first.";
                return RedirectToPage("/Auth/Login");
            }

            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var apiUrl = $"{ApiPath.PurchasePriceList}/id?id={id}";
                var response = await client.GetAsync(apiUrl);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Unauthorized access. Please login again.";
                    return RedirectToPage("/Auth/Login");
                }

                PurchasePrice = JsonConvert.DeserializeObject<PurchasePriceDTO>(await response.Content.ReadAsStringAsync());

               
                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "You need to login first.";
                return RedirectToPage("/Auth/Login");
            }

            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


                var purchasePriceRequest = new PurchasePriceRequest
                {
                    PurchasePrice1 = PurchasePrice.PurchasePrice1,
                    Size = PurchasePrice.Size,
                    CategoryId = PurchasePrice.CategoryId,
                    Description = PurchasePrice.Description,
                    UpsDate =  DateTime.Now,
                    Status = PurchasePrice.Status,
                };

                var json = JsonConvert.SerializeObject(purchasePriceRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var apiUrl = $"{ApiPath.PurchasePriceList}/id?id={id}";
                var response = await client.PutAsync(apiUrl, content);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Unauthorized access. Please login again.";
                    return RedirectToPage("/Auth/Login");
                }

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "The Jewelry is updated successfully!";
                    return RedirectToPage("./PurchasePrice");
                }
                else
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"An error occurred while updating the product. Status Code: {response.StatusCode}, Response: {responseBody}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return Page();
        }
    }
}
