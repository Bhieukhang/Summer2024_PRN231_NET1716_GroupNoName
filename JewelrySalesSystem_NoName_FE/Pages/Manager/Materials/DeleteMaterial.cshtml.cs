﻿using JewelrySalesSystem_NoName_FE.DTOs.Material;
using JewelrySalesSystem_NoName_FE.DTOs.Product;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Materials
{
    public class DeleteMaterialModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DeleteMaterialModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public MaterialDTO Material { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var apiUrl = $"{ApiPath.MaterialList}/id?id={id}";
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetStringAsync(apiUrl);
            Material = JsonConvert.DeserializeObject<MaterialDTO>(response);

            if (Material == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var apiUrl = $"{ApiPath.MaterialList}/id?id={id}";
            var client = _httpClientFactory.CreateClient();

            var response = await client.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Chất liệu trang sức được xóa thành công";
                return RedirectToPage("./ListMaterials");
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"An error occurred while deleting the material. Status Code: {response.StatusCode}, Response: {responseBody}");
            }

            return Page();
        }
    }
}

