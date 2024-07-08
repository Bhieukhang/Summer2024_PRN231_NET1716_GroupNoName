using Firebase.Storage;
using JewelrySalesSystem_NoName_FE.DTOs.Material;
using JewelrySalesSystem_NoName_FE.DTOs.Product;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Products
{
    public class AddSubProductModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly string _bucket;

        [BindProperty]
        public ProductDTO Product { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }
        public IList<CategoryDTO> CategoryList { get; set; } = new List<CategoryDTO>();
        public IList<MaterialDTO> MaterialList { get; set; } = new List<MaterialDTO>();
        public IList<SubProductsDTO> SubProductList { get; set; } = new List<SubProductsDTO>();

        public AddSubProductModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _bucket = _configuration["Firebase:Bucket"];
        }

        public async Task<IActionResult> OnGetAsync()
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

                var categoryApiUrl = $"{ApiPath.CategoryList}";
                var subProductApiUrl = $"{ApiPath.SubProductList}";
                var materialApiUrl = $"{ApiPath.MaterialList}";

                var response = await client.GetAsync(categoryApiUrl);
                var subResponse = await client.GetAsync(subProductApiUrl);
                var mateResponse = await client.GetAsync(materialApiUrl);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Unauthorized access. Please login again.";
                    return RedirectToPage("/Auth/Login");
                }
                if (mateResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Kết nối không được xác thực ! Hãy login lại .";
                    return RedirectToPage("/Auth/Login");
                }
                if (subResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Unauthorized access. Please login again.";
                    return RedirectToPage("/Auth/Login");
                }



                CategoryList = JsonConvert.DeserializeObject<List<CategoryDTO>>(await response.Content.ReadAsStringAsync());
                SubProductList = JsonConvert.DeserializeObject<List<SubProductsDTO>>(await response.Content.ReadAsStringAsync());
                MaterialList = JsonConvert.DeserializeObject<List<MaterialDTO>>(await mateResponse.Content.ReadAsStringAsync());
                Product = new ProductDTO
                {
                    SubId = Guid.Parse("b0aae9d9-96f5-43fd-b0ae-379b1fb3f7a1")
                };
                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "You need to login first.";
                return RedirectToPage("/Auth/Login");
            }

            const long MAX_ALLOWED_SIZE = 1024 * 1024 * 100;

            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                if (Image != null && Image.Length > MAX_ALLOWED_SIZE)
                {
                    ModelState.AddModelError(string.Empty, "The uploaded file is too large.");
                    return Page();
                }

                if (Image != null)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                    var storage = new FirebaseStorage(_bucket);
                    using (var stream = new MemoryStream())
                    {
                        Image.CopyTo(stream);
                        stream.Seek(0, SeekOrigin.Begin);
                        var uploadTask = storage.Child("uploads").Child(uniqueFileName).PutAsync(stream);
                        Product.ImgProduct = await uploadTask;

                        stream.Seek(0, SeekOrigin.Begin);
                        Product.ImgProduct = Convert.ToBase64String(stream.ToArray());
                    }
                }

                Product.Id = Guid.NewGuid();
                Product.InsDate = DateTime.Now;
                Product.Deflag = true;
                Product.SubId = Guid.Parse("b0aae9d9-96f5-43fd-b0ae-379b1fb3f7a1");

                var productRequest = new ProductRequest
                {
                    ProductName = Product.ProductName,
                    Code = Product.Code,
                    MaterialId = Product.MaterialId,
                    Description = Product.Description,
                    ImgProduct = Product.ImgProduct,
                    ImportPrice = Product.ImportPrice,
                    Size = Product.Size,
                    Tax = Product.Tax,
                    ProcessPrice = Product.ProcessPrice,
                    SellingPrice = Product.SellingPrice,
                    Quantity = Product.Quantity,
                    CategoryId = Product.CategoryId,
                    InsDate = Product.InsDate,
                    Deflag = Product.Deflag,
                    SubId = Guid.Parse("b0aae9d9-96f5-43fd-b0ae-379b1fb3f7a1"),
                PeriodWarranty = Product.PeriodWarranty,

                };
                Console.WriteLine(JsonConvert.SerializeObject(productRequest));
                Console.WriteLine($"SubId: {productRequest.SubId}");

                var json = JsonConvert.SerializeObject(productRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var apiUrl = $"{ApiPath.ProductList}";
                var response = await client.PostAsync(apiUrl, content);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Unauthorized access. Please login again.";
                    return RedirectToPage("/Auth/Login");
                }

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "The Jewelry is added successfully!";
                    return RedirectToPage("./ListSubProduct");
                }
                else
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"An error occurred while adding the product. Status Code: {response.StatusCode}, Response: {responseBody}");
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
