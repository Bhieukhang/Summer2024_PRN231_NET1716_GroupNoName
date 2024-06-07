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
}
