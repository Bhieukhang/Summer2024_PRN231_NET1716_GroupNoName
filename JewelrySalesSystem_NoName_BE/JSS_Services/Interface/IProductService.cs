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
<<<<<<< HEAD
<<<<<<< HEAD
        Task<IEnumerable<Product>> GetAllProductsAsync();
=======


        Task<IPaginate<ProductResponse>> GetAllProductsAsync(int page, int size);
>>>>>>> 2d80fe07a84d0c7c85e1ed663bff0957718eb5b2
=======


        Task<IPaginate<ProductResponse>> GetAllProductsAsync(int page, int size);
>>>>>>> 2d80fe07a84d0c7c85e1ed663bff0957718eb5b2
        Task<Product> GetProductByIdAsync(Guid id);
        Task<Product> GetProductByCodeAsync(string code);
        Task<Product> CreateProductAsync(Product newData, Stream imageStream, string imageName);
        Task<Product> UpdateProductAsync(Guid id, Product updatedData, Stream imageStream, string imageName);
        Task<bool> DeleteProductAsync(Guid id);
        Task<ProductMapPromotion> GetPromotionByProductCode(string productCode);
   
    }
}
