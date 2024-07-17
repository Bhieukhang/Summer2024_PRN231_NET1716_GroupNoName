using Firebase.Storage;
using JewelrySalesSystem_NoName_FE.DTOs.Material;
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
        public IList<MaterialDTO> MaterialList { get; set; } = new List<MaterialDTO>();

        public AddProductModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
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
                TempData["ErrorMessage"] = "Bạn cần phải login trước.";
                return RedirectToPage("/Auth/Login");
            }

            await LoadSelectLists(token);

            return Page();
        }

        private async Task<bool> IsProductNameExistsAsync(string productName, string token)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var apiUrl = $"{ApiPath.ProductList}?productName={productName}";
            var response = await client.GetAsync(apiUrl);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                TempData["ErrorMessage"] = "Kết nối không được xác thực ! Hãy login lại .";
                return false;
            }

            var product = JsonConvert.DeserializeObject<ProductDTO>(await response.Content.ReadAsStringAsync());
            return product != null;
        }

        private async Task<bool> IsProductCodeExistsAsync(string code, string token)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var apiUrl = $"{ApiPath.ProductList}?code={code}";
            var response = await client.GetAsync(apiUrl);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                TempData["ErrorMessage"] = "Kết nối không được xác thực ! Hãy login lại .";
                return false;
            }

            var products = JsonConvert.DeserializeObject<List<ProductDTO>>(await response.Content.ReadAsStringAsync());
            return products.Any(p => p.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
        }

        private async Task LoadSelectLists(string token)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var categoryApiUrl = $"{ApiPath.CategoryList}";
            var materialApiUrl = $"{ApiPath.MaterialList}";

            var response = await client.GetAsync(categoryApiUrl);
            var mateResponse = await client.GetAsync(materialApiUrl);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                TempData["ErrorMessage"] = "Kết nối không được xác thực ! Hãy login lại .";
                return;
            }
            if (mateResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                TempData["ErrorMessage"] = "Kết nối không được xác thực ! Hãy login lại .";
                return;
            }

            CategoryList = JsonConvert.DeserializeObject<List<CategoryDTO>>(await response.Content.ReadAsStringAsync());
            MaterialList = JsonConvert.DeserializeObject<List<MaterialDTO>>(await mateResponse.Content.ReadAsStringAsync());
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "Bạn cần phải login trước.";
                return RedirectToPage("/Auth/Login");
            }

            const long MAX_ALLOWED_SIZE = 1024 * 1024 * 100;

            if (Product.Code.Length > 8)
            {
                TempData["ErrorMessage"] =  "Độ dài của mã trang sức không được quá 8 kí tự.";
                await LoadSelectLists(token);  
                return Page();
            }
            if (Product.ProductName.Length > 50)
            {
                TempData["ErrorMessage"] =  "Độ dài của mã trang sức không được quá 50 kí tự.";
                await LoadSelectLists(token);
                return Page();
            }

            if (Product.Quantity > 50)
            {
                TempData["ErrorMessage"] =  "Số lượng trang sức không vượt quá 50 cái.";
                await LoadSelectLists(token);
                return Page();
            }

            if (Product.PeriodWarranty > 24)
            {
                TempData["ErrorMessage"] =  "Thời gian bảo hành trang sức không vượt quá 2 năm.";
                await LoadSelectLists(token);
                return Page();
            }

            if (await IsProductNameExistsAsync(Product.ProductName, token))
            {
                TempData["ErrorMessage"] = "Tên trang sức đã tồn tại.";
                await LoadSelectLists(token); 
                return Page();
            }

            if (await IsProductCodeExistsAsync(Product.Code, token))
            {
                TempData["ErrorMessage"] = "Mã trang sức đã tồn tại.";
                await LoadSelectLists(token);
                return Page();
            }

            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                client.Timeout = TimeSpan.FromMinutes(2);

                if (Image != null && Image.Length > MAX_ALLOWED_SIZE)
                {
                    TempData["ErrorMessage"] = "Dung lượng file upload quá nặng !.";
                    await LoadSelectLists(token); 
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

                var productRequest = new ProductRequest
                {
                    ProductName = Product.ProductName,
                    Description = Product.Description,
                    SellingPrice = Product.SellingPrice,
                    Size = Product.Size,
                    ImportPrice = Product.ImportPrice,
                    InsDate = Product.InsDate,
                    Deflag = Product.Deflag,
                    CategoryId = Product.CategoryId,
                    Quantity = Product.Quantity,
                    ProcessPrice = Product.ProcessPrice,
                    MaterialId = Product.MaterialId,
                    Code = Product.Code,
                    ImgProduct = Product.ImgProduct,
                    Tax = Product.Tax,
                    PeriodWarranty = Product.PeriodWarranty,
                };

                var json = JsonConvert.SerializeObject(productRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var apiUrl = $"{ApiPath.ProductList}";
                var response = await client.PostAsync(apiUrl, content);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Kết nối không được xác thực ! Hãy login lại .";
                    return RedirectToPage("/Auth/Login");
                }

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Trang sức mới được thêm thành công !";
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

            await LoadSelectLists(token);  
            return Page();
        }
    }
}
