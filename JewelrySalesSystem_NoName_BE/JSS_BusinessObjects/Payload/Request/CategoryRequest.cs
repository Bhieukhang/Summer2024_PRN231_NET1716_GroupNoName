using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Request
{
    public class CategoryRequest
    {
        public string? Name { get; set; }
        public double? PricePressure { get; set; }
    }
}
