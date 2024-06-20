using JSS_BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Response
{
    public class DiamondResponse
    {
        public DiamondResponse(Guid id, string? diamondName, string? code, double? carat, string? color, string? clarity
            , string? cut, double? price, string? imageDiamond, int? quantity, DateTime? insDate) 
        {
            Id = id;
            DiamondName = diamondName;
            Code = code;
            Carat = carat;
            Color = color;
            Clarity = clarity;
            Cut = cut;
            Price = price;
            ImageDiamond = imageDiamond;
            Quantity = quantity;
            InsDate = insDate;
        }

        public Guid Id { get; set; }

        public string? DiamondName { get; set; }
        public string? Code { get; set; }

        public double? Carat { get; set; }

        public string? Color { get; set; }

        public string? Clarity { get; set; }

        public string? Cut { get; set; }

        public double? Price { get; set; }

        public Guid? JewelryId { get; set; }

        public string? ImageDiamond { get; set; }

        public int? Quantity { get; set; }

        public DateTime? InsDate { get; set; }

        public DateTime? UpsDate { get; set; }
    }
}
