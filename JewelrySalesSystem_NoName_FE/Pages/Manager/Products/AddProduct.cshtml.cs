using Firebase.Storage;
using JewelrySalesSystem_NoName_FE.DTOs.Product;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Products
{
    public class AddProductModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly string _bucket;

        [BindProperty]
        public ProductDTO Product { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }
        public IList<CategoryDTO> CategoryList { get; set; } = new List<CategoryDTO>();

        public AddProductModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _bucket = _configuration["Firebase:Bucket"];
        }

        public async Task OnGetAsync()
        {
            var categoryApiUrl = $"{ApiPath.CategoryList}";
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetStringAsync(categoryApiUrl);
            CategoryList = JsonConvert.DeserializeObject<List<CategoryDTO>>(response);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    await OnGetAsync();
            //    return Page();
            //}

            var apiUrl = $"{ApiPath.ProductList}";
            const long MAX_ALLOWED_SIZE = 1024 * 1024 * 100;

            try
            {
                var client = _httpClientFactory.CreateClient();

                if (Image != null && Image.Length > MAX_ALLOWED_SIZE)
                {
                    ModelState.AddModelError(string.Empty, "The uploaded file is too large.");
                    return Page();
                }

                // Upload image to Firebase and get URL
                if (Image != null)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                    var storage = new FirebaseStorage(_bucket);
                    using (var stream = new MemoryStream())
                    {
                        Image.CopyTo(stream);
                        stream.Seek(0, SeekOrigin.Begin); // Reset stream position to the beginning
                        var uploadTask = storage.Child("uploads").Child(uniqueFileName).PutAsync(stream);
                        Product.ImgProduct = await uploadTask; // Get URL from Firebase
                    }

                    // Convert image to base64 string
                    using (var memoryStream = new MemoryStream())
                    {
                        Image.CopyTo(memoryStream);
                        Product.ImgProduct = Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
                Product.Id = Guid.NewGuid();
                Product.InsDate = DateTime.Now;
                Product.Deflag = true;

                var categoryApiUrl = $"{ApiPath.CategoryList}/id?id={Product.CategoryId}";
                var categoryResponse = await client.GetStringAsync(categoryApiUrl);
                var category = JsonConvert.DeserializeObject<CategoryDTO>(categoryResponse);

                if (category == null)
                {
                    ModelState.AddModelError(string.Empty, "Category not found.");
                    return Page();
                }

                // Map ProductDTO to ProductRequest
                var productRequest = new ProductRequest
                {
                    ProductName = Product.ProductName,
                    Description = Product.Description,
                    TotalPrice = Product.TotalPrice,
                    Size = Product.Size,
                    ImportPrice = Product.ImportPrice,
                    InsDate = Product.InsDate,
                    Deflag = Product.Deflag,
                    CategoryId = Product.CategoryId,
                    Quantity = Product.Quantity,
                    ProcessPrice = Product.ProcessPrice,
                    ProductMaterialId = Product.ProductMaterialId,
                    Code = Product.Code,
                    ImgProduct = Product.ImgProduct, // base64 string
                    Category = category,
                };

                // Serialize ProductRequest to JSON
                var json = JsonConvert.SerializeObject(productRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "The Jewelry is added successfully !";
                    return RedirectToPage("./ListProduct");
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
