using JewelrySalesSystem_NoName_FE.DTOs.Material;
using JewelrySalesSystem_NoName_FE.DTOs.Product;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Categories
{
    public class DeleteCategoryModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DeleteCategoryModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public CategoryDTO Category { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "Bạn cần phải login trước.";
                return RedirectToPage("/Auth/Login");
            }

            var apiUrl = $"{ApiPath.CategoryList}/id?id={id}";
            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetStringAsync(apiUrl);
            Category = JsonConvert.DeserializeObject<CategoryDTO>(response);

            if (Category == null)
            {
                return NotFound();
            }

            await LoadOnGet(id,token);
            return Page();
        }

        private async Task LoadOnGet(Guid id, string token)
        {
            var apiUrl = $"{ApiPath.CategoryList}/id?id={id}";
            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetStringAsync(apiUrl);
            Category = JsonConvert.DeserializeObject<CategoryDTO>(response);
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "Bạn cần phải login trước.";
                return RedirectToPage("/Auth/Login");
            }

            var productsApiUrl = $"{ApiPath.ProductList}/category?categoryId={id}";

            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var hasProductsResponse = await client.GetAsync(productsApiUrl);

            if (hasProductsResponse.IsSuccessStatusCode)
            {
                var hasProducts = JsonConvert.DeserializeObject<bool>(await hasProductsResponse.Content.ReadAsStringAsync());
                if (hasProducts)
                {
                    TempData["ErrorMessage"] = "Không thể xóa những loại đã được phân loại cho trang sức.";
                    await LoadOnGet(id, token);
                    return Page();
                }
            }
            else
            {
                TempData["ErrorMessage"] = "An error occurred while checking for associated products.";
                return Page();
            }

            var apiUrl = $"{ApiPath.CategoryList}/id?id={id}";
            var response = await client.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Loại trang sức được xóa thành công";
                return RedirectToPage("./ListCategories");
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"An error occurred while deleting the category of product. Status Code: {response.StatusCode}, Response: {responseBody}");
            }

            return Page();
        }
    }
}

