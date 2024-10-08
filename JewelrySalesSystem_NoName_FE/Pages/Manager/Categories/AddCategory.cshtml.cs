﻿using Firebase.Storage;
using JewelrySalesSystem_NoName_FE.DTOs.Diamonds;
using JewelrySalesSystem_NoName_FE.DTOs.Material;
using JewelrySalesSystem_NoName_FE.DTOs.Product;
using JewelrySalesSystem_NoName_FE.Pages.Manager.Products;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Categories
{
    public class AddCategoryModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        [BindProperty]  
        public CategoryDTO Category { get; set; }

        public AddCategoryModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        private async Task<bool> IsCategoryNameExistsAsync(string categoryName, string token)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var apiUrl = $"{ApiPath.CategoryList}/categoryName?name={categoryName}";
            var response = await client.GetAsync(apiUrl);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                TempData["ErrorMessage"] = "Kết nối không được xác thực ! Hãy login lại .";
                return false;
            }

            var category = JsonConvert.DeserializeObject<CategoryDTO>(await response.Content.ReadAsStringAsync());
            return category != null;
        }

        public async Task<IActionResult> OnGetAsync()
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
                TempData["ErrorMessage"] = "Bạn cần phải login trước.";
                return RedirectToPage("/Auth/Login");
            }

            if (await IsCategoryNameExistsAsync(Category.Name, token))
            {
                TempData["ErrorMessage"] = "Tên loại này đã tồn tại.";
                return Page();
            }

            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                Category.Id = Guid.NewGuid();

                var cate = new CategoryDTO
                {
                    Name = Category.Name,
                };

                var json = JsonConvert.SerializeObject(cate);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var apiUrl = $"{ApiPath.CategoryList}";
                var response = await client.PostAsync(apiUrl, content);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["ErrorMessage"] = "Kết nối không được xác thực ! Hãy login lại .";
                    return RedirectToPage("/Auth/Login");
                }

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Loại trang sức mới được thêm thành công !";
                    return RedirectToPage("./ListCategories");
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
