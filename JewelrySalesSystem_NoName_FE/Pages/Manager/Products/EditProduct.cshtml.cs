using Firebase.Storage;
using JewelrySalesSystem_NoName_FE.DTOs.Product;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Products
{
    public class EditProductModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly string _bucket;

        [BindProperty]
        public ProductDTO Product { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }
        public IList<CategoryDTO> CategoryList { get; set; } = new List<CategoryDTO>();

        public EditProductModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _bucket = _configuration["Firebase:Bucket"];
        }

        public async Task OnGetAsync(Guid id)
        {
            var apiUrl = $"{ApiPath.ProductList}/id?id={id}";
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetStringAsync(apiUrl);
            Product = JsonConvert.DeserializeObject<ProductDTO>(response);

            var categoryApiUrl = $"{ApiPath.CategoryList}";
            var categoryResponse = await client.GetStringAsync(categoryApiUrl);
            CategoryList = JsonConvert.DeserializeObject<List<CategoryDTO>>(categoryResponse);

            if (!string.IsNullOrEmpty(Product.ImgProduct) && Product.ImgProduct.StartsWith("data:image"))
            {
                var base64Data = Product.ImgProduct.Split(',')[1];
                Product.ImgProduct = $"data:image/jpeg;base64,{base64Data}";
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var apiUrl = $"{ApiPath.ProductList}/id?id={Product.Id}";
            const long MAX_ALLOWED_SIZE = 1024 * 1024 * 100;

            try
            {
                var client = _httpClientFactory.CreateClient();

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

                var productRequest = new ProductRequest
                {
                    
                    ProductName = Product.ProductName,
                    Description = Product.Description,
                    TotalPrice = Product.TotalPrice,
                    Size = Product.Size,
                    ImportPrice = Product.ImportPrice,
                    Deflag = Product.Deflag,
                    CategoryId = Product.CategoryId,
                    Quantity = Product.Quantity,
                    ProcessPrice = Product.ProcessPrice,
                    AccessoryId = Product.AccessoryId,
                    ProductMaterialId = Product.ProductMaterialId,
                    Code = Product.Code,
                    ImgProduct = Product.ImgProduct, // base64 string
                };

                var json = JsonConvert.SerializeObject(productRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PutAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("./ListProduct");
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
