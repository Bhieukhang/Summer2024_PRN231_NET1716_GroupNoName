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
    public class ProductMaterialService : BaseService<ProductMaterialService>, IProductMaterialService
    {
        public ProductMaterialService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork, ILogger<ProductMaterialService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<IEnumerable<ProductMaterial>> GetAllProductMaterialsAsync()
        {
            return await _unitOfWork.GetRepository<ProductMaterial>().GetListAsync();
        }

        public async Task<ProductMaterial> GetProductMaterialByIdAsync(Guid id)
        {
            return await _unitOfWork.GetRepository<ProductMaterial>().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<ProductMaterial> CreateProductMaterialAsync(ProductMaterial newData)
        {
            newData.Id = Guid.NewGuid();
            await _unitOfWork.GetRepository<ProductMaterial>().InsertAsync(newData);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccessful) return null;
            return newData;
        }

        public async Task<ProductMaterial> UpdateProductMaterialAsync(Guid id, ProductMaterial updatedData)
        {
            var existingProductMaterial = await _unitOfWork.GetRepository<ProductMaterial>().FirstOrDefaultAsync(a => a.Id == id);
            if (existingProductMaterial == null) return null;

            _unitOfWork.GetRepository<ProductMaterial>().UpdateAsync(updatedData);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccessful) return null;
            return updatedData;
        }

        public async Task<bool> DeleteProductMaterialAsync(Guid id)
        {
            var existingProductMaterial = await _unitOfWork.GetRepository<ProductMaterial>().FirstOrDefaultAsync(a => a.Id == id);
            if (existingProductMaterial == null) return false;

            _unitOfWork.GetRepository<ProductMaterial>().DeleteAsync(existingProductMaterial);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
