using JSS_BusinessObjects;
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
        Task<IPaginate<ProductResponse>> GetAllProductsAsync(int page, int size);
        Task<IPaginate<ProductResponse>> SearchAndFilterProductsAsync(string? code, Guid? categoryId, Guid? materialId, int? page, int? size);
        Task<Product> GetProductByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAsync();
        Task<Product> CreateProductAsync(Product newData, Stream imageStream, string imageName);
        Task<Product> UpdateProductAsync(Guid id, Product updatedData, Stream imageStream, string imageName);
        Task<bool> DeleteProductAsync(Guid id);
        Task AddProductConditionGroup(Guid productId, Guid promotionId);
        Task DeleteProductConditionGroup( Guid promotionId);
        Task<ProductMapPromotion> GetPromotionByProductCode(string productCode);
   
    }
}
