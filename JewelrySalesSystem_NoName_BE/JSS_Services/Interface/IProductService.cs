﻿using JSS_BusinessObjects;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_Services.Interface
{
    public interface IProductService
    {
        //Do huu Thuan
        Task<int> GetTotalSubProductAsync();
        public Task<IPaginate<ProductResponse>> GetProductBySubIdAsync(Guid subId, int page, int size);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        Task<Product> GetProductByCodeAsync(string code);
        Task<Product> CreateProductAsync(Product newData, Stream imageStream, string imageName);
        Task<Product> UpdateProductAsync(Guid id, Product updatedData, Stream imageStream, string imageName);
        Task<bool> DeleteProductAsync(Guid id);
        Task<ProductMapPromotion> GetPromotionByProductCode(string productCode);
   
    }
}
