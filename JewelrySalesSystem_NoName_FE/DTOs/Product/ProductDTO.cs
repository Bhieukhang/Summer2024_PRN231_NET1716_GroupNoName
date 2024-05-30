using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JewelrySalesSystem_NoName_FE.DTOs.Product;

public partial class ProductDTO
{
    public Guid Id { get; set; }
    public string? ImgProduct {  get; set; }

    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public double? ImportPrice { get; set; }

    public double? Size { get; set; }

    public double? TotalPrice { get; set; }

    public DateTime? InsDate { get; set; }

    public bool? Deflag { get; set; }

    public Guid? CategoryId { get; set; }

    public DateTime? UpsDate { get; set; }

    public int? Quantity { get; set; }

    public double? ProcessPrice { get; set; }

    public Guid? ProductMaterialId { get; set; }

    public Guid? AccessoryId { get; set; }

    public string? Code { get; set; }

    public CategoryDTO Category { get; set; }

    [NotMapped] 
    public bool DeflagChecked
    {
        get => Deflag.GetValueOrDefault();
        set => Deflag = value;
    }
}
