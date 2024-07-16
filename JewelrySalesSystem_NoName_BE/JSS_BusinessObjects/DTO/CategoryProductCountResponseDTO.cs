using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.DTO
{
    public class CategoryProductCountResponseDTO
    {
        public Guid? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ProductCount { get; set; }
        public List<ProductDTO> Products { get; set; }
    }

    public class ProductDTO
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
