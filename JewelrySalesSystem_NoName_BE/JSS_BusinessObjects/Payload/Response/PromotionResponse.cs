using JSS_BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Response
{
    public class ProductMapPromotion
    {
        public ProductResponse Product { get; set; }
        public List<Promotion> Promotions { get; set; }

        public ProductMapPromotion()
        {
            Promotions = new List<Promotion>();
            Product = new ProductResponse();
        }
    }

    public class ProductMapPromotionItem
    {
        public ProductMapPromotionItem()
        {
            Promotions = new List<Promotion>();
        }
        public string ProductCode { get; set; }
        public List<Promotion> Promotions { get; set; }
    }

    public class PromotionItem
    {
        public Guid PromotionId { get; set; }
        public string PromotionName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? Percentage { get; set; }
    }
}
