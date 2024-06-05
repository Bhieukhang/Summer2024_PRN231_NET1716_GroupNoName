using JewelrySalesSystem_NoName_FE.DTOs.Product;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Products
{
    public class DeleteProductModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DeleteProductModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public ProductDTO Product { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var apiUrl = $"{ApiPath.ProductList}/id?id={id}";
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetStringAsync(apiUrl);
            Product = JsonConvert.DeserializeObject<ProductDTO>(response);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var apiUrl = $"{ApiPath.ProductList}/id?id={id}";
            var client = _httpClientFactory.CreateClient();

            var response = await client.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "The product is deleted successfully.";
                return RedirectToPage("./ListProduct");
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"An error occurred while deleting the product. Status Code: {response.StatusCode}, Response: {responseBody}");
            }

            return Page();
        }
    }
}

