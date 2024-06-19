using JewelrySalesSystem_NoName_FE.DTOs;
using JewelrySalesSystem_NoName_FE.DTOs.Product;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Products
{
    public class ListProductModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ListProductModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        [BindProperty]
        public string? SearchCode { get; set; }
        public IList<ProductDTO> productList { get; set; } = new List<ProductDTO>();
        public IList<CategoryDTO> cateList { get; set; } = new List<CategoryDTO>();
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync(int? currentPage)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "You need to login first.";
                return RedirectToPage("/Auth/Login");
            }
            Page = currentPage ?? 1;
            Size = 2;
            var url = $"{ApiPath.ProductList}?page={Page}&size={Size}";

            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var productResponse = await client.GetStringAsync(url);
                var paginateResult = JsonConvert.DeserializeObject<Paginate<ProductDTO>>(productResponse);
                TotalPages = paginateResult.TotalPages;

                var categoryResponse = await client.GetAsync(ApiPath.CategoryList);
                if (categoryResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Unauthorized access. Please login again.";
                    return RedirectToPage("/Auth/Login");
                }
                cateList = JsonConvert.DeserializeObject<List<CategoryDTO>>(await categoryResponse.Content.ReadAsStringAsync());
                productList = paginateResult.Items;

                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                productList = new List<ProductDTO>();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostSearchAsync()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "You need to login first.";
                return RedirectToPage("/Auth/Login");
            }

            if (string.IsNullOrEmpty(SearchCode))
            {
                await LoadProductListAsync();
                return Page();
            }

            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var apiUrl = $"{ApiPath.ProductList}/code?code={SearchCode}";
                var response = await client.GetAsync(apiUrl);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Unauthorized access. Please login again.";
                    return RedirectToPage("/Auth/Login");
                }

                if (response.IsSuccessStatusCode)
                {
                    var product = JsonConvert.DeserializeObject<ProductDTO>(await response.Content.ReadAsStringAsync());
                    productList = new List<ProductDTO> { product };

                    var categoryResponse = await client.GetAsync(ApiPath.CategoryList);
                    if (categoryResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        TempData["ErrorMessage"] = "Unauthorized access. Please login again.";
                        return RedirectToPage("/Auth/Login");
                    }
                    cateList = JsonConvert.DeserializeObject<List<CategoryDTO>>(await categoryResponse.Content.ReadAsStringAsync());
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Product not found.");
                    productList = new List<ProductDTO>();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                return Page();
            }

            return Page();
        }

        //public async Task<IActionResult> OnGetDetailsAsync(Guid ProductId)
        //{
        //    var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
        //    if (string.IsNullOrEmpty(token))
        //    {
        //        return RedirectToPage("/Auth/Login");
        //    }

        //    var url = $"{ApiPath.ProductDetails}id={ProductId}";
        //    try
        //    {
        //        var client = _httpClientFactory.CreateClient();
        //        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        //        var response = await client.GetStringAsync(url);

        //        productDetail = JsonConvert.DeserializeObject<ProductDTO>(response);
        //        return Partial("_ProductDetailPartial", productDetail);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest();
        //    }
        //}

        private async Task LoadProductListAsync()
        {
            var token = HttpContext.Session.GetString("Token");
            var apiUrl = $"{ApiPath.ProductList}";
            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var productResponse = await client.GetAsync(apiUrl);
                if (productResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Unauthorized access. Please login again.";
                    RedirectToPage("/Auth/Login");
                }
                productList = JsonConvert.DeserializeObject<List<ProductDTO>>(await productResponse.Content.ReadAsStringAsync());

                var categoryResponse = await client.GetAsync(ApiPath.CategoryList);
                if (categoryResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Unauthorized access. Please login again.";
                    RedirectToPage("/Auth/Login");
                }
                cateList = JsonConvert.DeserializeObject<List<CategoryDTO>>(await categoryResponse.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                productList = new List<ProductDTO>();
            }
        }
    }
}
