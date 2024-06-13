using JewelrySalesSystem_NoName_FE.DTOs;
using JewelrySalesSystem_NoName_FE.DTOs.Account;
using JewelrySalesSystem_NoName_FE.DTOs.Product;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Products
{
    [Authorize(Roles = "Admin, Manager, Staff")]
    public class ListSubProductModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ListSubProductModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public IList<ProductDTO> ListProducts { get; set; } = new List<ProductDTO>();

        public Guid SubId { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? subId, int? page, int? size)
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Auth/Login");
            }

            SubId = subId ?? new Guid("b0aae9d9-96f5-43fd-b0ae-379b1fb3f7a1");
            Page = page ?? 1;
            Size = size ?? 10;

            var url = $"{ApiPath.SubProductsList}?subid={SubId}&page={Page}&size={Size}";
         
            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetStringAsync(url);

                var paginateResult = JsonConvert.DeserializeObject<Paginate<ProductDTO>>(response);
                ListProducts = paginateResult.Items;


            }
            catch (Exception ex)
            {

                ListProducts = new List<ProductDTO>();

            }

            return Page();
        }
    }
}
