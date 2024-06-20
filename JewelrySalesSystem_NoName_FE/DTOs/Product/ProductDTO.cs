using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JewelrySalesSystem_NoName_FE.DTOs.Product;

public partial class ProductDTO
{
    public Guid Id { get; set; }
    [Required]
    public string? ImgProduct {  get; set; }
    [Required]
    public string? ProductName { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public double? ImportPrice { get; set; }
    [Required]
    public double? Size { get; set; }
    public double? SellingPrice { get; set; }

    public DateTime? InsDate { get; set; }
    public bool? Deflag { get; set; }
    [Required]
    public Guid? CategoryId { get; set; }

    public DateTime? UpsDate { get; set; }
    [Required]
    public int? Quantity { get; set; }

    public double? ProcessPrice { get; set; }

    public Guid? MaterialId { get; set; }
    public Guid? SubId { get; set; }
    [Required]
    public string? Code { get; set; }
    [Required]
    public double? Tax { get; set; }

    public CategoryDTO Category { get; set; }

    [NotMapped] 
    public bool DeflagChecked
    {
        get => Deflag.GetValueOrDefault();
        set => Deflag = value;
    }

}
