using JSS_BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Response
{
    public class CategoryResponse
    {
        public CategoryResponse(Guid id, string? name)
        {
            Id = id;
            Name = name;   
        }
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public virtual ICollection<Product> Products { get; } = new List<Product>();
    }
}
