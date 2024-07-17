using Firebase.Storage;
using JewelrySalesSystem_NoName_FE.DTOs.Material;
using JewelrySalesSystem_NoName_FE.DTOs.Product;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;

// DO huu Thuan

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Products
{
    public class EditSubProductModel : PageModel
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

        public EditSubProductModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _bucket = _configuration["Firebase:Bucket"];
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "Bạn cần phải login trước.";
                return RedirectToPage("/Auth/Login");
            }

            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var apiUrl = $"{ApiPath.ProductList}/id?id={id}";
                var response = await client.GetAsync(apiUrl);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Kết nối không được xác thực ! Hãy login lại .";
                    return RedirectToPage("/Auth/Login");
                }

                Product = JsonConvert.DeserializeObject<ProductDTO>(await response.Content.ReadAsStringAsync());

                var categoryApiUrl = $"{ApiPath.CategoryList}";
                var categoryResponse = await client.GetAsync(categoryApiUrl);
                CategoryList = JsonConvert.DeserializeObject<List<CategoryDTO>>(await categoryResponse.Content.ReadAsStringAsync());

                var materialApiUrl = $"{ApiPath.MaterialList}";
                var materialResponse = await client.GetAsync(materialApiUrl);
                MaterialList = JsonConvert.DeserializeObject<List<MaterialDTO>>(await materialResponse.Content.ReadAsStringAsync());

                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "Bạn cần phải login trước.";
                return RedirectToPage("/Auth/Login");
            }

            const long MAX_ALLOWED_SIZE = 1024 * 1024 * 100;

            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                if (Image != null && Image.Length > MAX_ALLOWED_SIZE)
                {
                    ModelState.AddModelError(string.Empty, "Dung lượng file quá lớn !");
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
                    SellingPrice = Product.SellingPrice,
                    Size = Product.Size,
                    ImportPrice = Product.ImportPrice,
                    Deflag = Product.Deflag,
                    CategoryId = Product.CategoryId,
                    Quantity = Product.Quantity,
                    ProcessPrice = Product.ProcessPrice,
                    MaterialId = Product.MaterialId,
                    Code = Product.Code,
                    ImgProduct = Product.ImgProduct,
                    SubId = Guid.Parse("b0aae9d9-96f5-43fd-b0ae-379b1fb3f7a1"),
                    Tax = Product.Tax
                };

                var json = JsonConvert.SerializeObject(productRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var apiUrl = $"{ApiPath.ProductList}/id?id={id}";
                var response = await client.PutAsync(apiUrl, content);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Kết nối không được xác thực ! Hãy login lại .";
                    return RedirectToPage("/Auth/Login");
                }

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Trang sức " + productRequest.Code + "được cập nhật thành công !";
                    return RedirectToPage("./ListSubProduct");
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