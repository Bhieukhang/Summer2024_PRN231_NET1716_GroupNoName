using System;
using System.Collections.Generic;

namespace JewelrySalesSystem_NoName_FE.Models
{
    public partial class SubProducts
    {
        public Guid Id { get; set; }
        public string TitleProductName { get; set; }
        public string Description { get; set;}
        public int Status { get; set; }

    }
}
