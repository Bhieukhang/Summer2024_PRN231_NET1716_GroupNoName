using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Models
{
    public partial class SubProducts
    {
        public Guid Id { get; set; }
        public string TitleProductName { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
    }
}
