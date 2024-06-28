using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JewelrySalesSystem_NoName_FE.Pages.Staff.Orders
{
    public class OrderBillModel : PageModel
    {

        [BindProperty]
        public string? SearchCode { get; set; }
    }
}
