using JSS_BusinessObjects.Models;
using System;
using System.Collections.Generic;

namespace JSS_BusinessObjects.Payload.Response
{
    public class SubProductResponse
    {
        public SubProductResponse(Guid id, string? titleProductName, string? description, int? status)
        {
            Id = id;
            TitleProductName = titleProductName;
            Description = description;
            Status = status;
        }

        public Guid Id { get; set; }
        public string? TitleProductName { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }
        public virtual ICollection<Product> Products { get; } = new List<Product>();
    }
}
