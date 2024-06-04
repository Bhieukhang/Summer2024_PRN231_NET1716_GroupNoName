using JewelrySalesSystem_NoName_FE.DTOs;
using JewelrySalesSystem_NoName_FE.DTOs.Promotions;
using JewelrySalesSystem_NoName_FE.DTOs.Warranty;
using JewelrySalesSystem_NoName_FE.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Warranty
{
    public class ListConditionWarrantiesModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ListConditionWarrantiesModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<ConditionWarrantyDTO> ConditionWarranties { get; set; } = new List<ConditionWarrantyDTO>();
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        [BindProperty]
        public ConditionWarrantyDTO ConditionWarranty { get; set; }


        public async Task OnGetAsync(int? currentPage)
        {
            Page = currentPage ?? 1;
            Size = 10;

            var url = $"{ApiPath.ConditionWarrantyList}?page={Page}&size={Size}";
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetStringAsync(url);

                var paginateResult = JsonConvert.DeserializeObject<Paginate<ConditionWarrantyDTO>>(response);
                ConditionWarranties = paginateResult.Items;
                TotalItems = paginateResult.Total;
                TotalPages = paginateResult.TotalPages;
            }
            catch (Exception ex)
            {
                ConditionWarranties = new List<ConditionWarrantyDTO>();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var apiUrlCreate = $"{ApiPath.ConditionWarrantyList}";
            try
            {
                var client = _httpClientFactory.CreateClient();
                var jsonContent = new StringContent(JsonConvert.SerializeObject(ConditionWarranty), Encoding.UTF8, "application/json");
                await client.PostAsync(apiUrlCreate, jsonContent);
                return Redirect("~/Manager/Warranty/ListConditionWarranties");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error sending order to backend.");
                return Page();
            }
        }

    }
}
