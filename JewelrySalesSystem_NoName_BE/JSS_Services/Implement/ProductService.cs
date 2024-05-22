using JSS_BusinessObjects.Models;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Services.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_Services.Implement
{
    public class ProductService : BaseService<ProductService>, IProductService
    {
        public ProductService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork, ILogger<ProductService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _unitOfWork.GetRepository<Product>().GetListAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _unitOfWork.GetRepository<Product>().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Product> CreateProductAsync(Product newData)
        {
            newData.Id = Guid.NewGuid();
            await _unitOfWork.GetRepository<Product>().InsertAsync(newData);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccessful) return null;
            return newData;
        }

        public async Task<Product> UpdateProductAsync(Guid id, Product updatedData)
        {
            var existingProduct = await _unitOfWork.GetRepository<Product>().FirstOrDefaultAsync(a => a.Id == id);
            if (existingProduct == null) return null;

            _unitOfWork.GetRepository<Product>().UpdateAsync(updatedData);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccessful) return null;
            return updatedData;
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var existingProduct = await _unitOfWork.GetRepository<Product>().FirstOrDefaultAsync(a => a.Id == id);
            if (existingProduct == null) return false;

            _unitOfWork.GetRepository<Product>().DeleteAsync(existingProduct);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
