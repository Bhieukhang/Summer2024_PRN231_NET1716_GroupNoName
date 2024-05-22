using JSS_BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Request
{
    public class MaterialRequest
    {
        public string? MaterialName { get; set; }

        public double? Weight { get; set; }

        public DateTime? InsDate { get; set; }

        public Guid ProductMaterialId { get; set; }
    }
}
