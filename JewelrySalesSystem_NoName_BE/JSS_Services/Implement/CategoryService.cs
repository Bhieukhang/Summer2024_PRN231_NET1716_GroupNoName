using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
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
    public class CategoryService : BaseService<CategoryService>, ICategoryService
    {
        public CategoryService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork, ILogger<CategoryService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _unitOfWork.GetRepository<Category>().GetListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await _unitOfWork.GetRepository<Category>().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<CategoryResponse> CreateCategoryAsync(CategoryRequest newData)
        {
            Category cate = new Category()
            {
                Id = Guid.NewGuid(),
                Description = newData.Description,
                Name = newData.Name,
                PricePressure = newData.PricePressure,
                Type = newData.Type
            };
            await _unitOfWork.GetRepository<Category>().InsertAsync(cate);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (isSuccessful == false) return null;
            return new CategoryResponse(cate.Id, cate.Name, cate.Type, cate.PricePressure)
            {
                Description = newData.Description
            };
        }

        public async Task<Category> UpdateCategoryAsync(Guid id, Category updatedData)
        {
            var existingCategory = await _unitOfWork.GetRepository<Category>().FirstOrDefaultAsync(a => a.Id == id);
            if (existingCategory == null) return null;

            existingCategory.Name = updatedData.Name;
            existingCategory.Type = updatedData.Type;
            existingCategory.Description = updatedData.Description;
            existingCategory.PricePressure = updatedData.PricePressure;

            _unitOfWork.GetRepository<Category>().UpdateAsync(existingCategory);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccessful) return null;
            return existingCategory;
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var existingCategory = await _unitOfWork.GetRepository<Category>().FirstOrDefaultAsync(a => a.Id == id);
            if (existingCategory == null) return false;

            _unitOfWork.GetRepository<Category>().DeleteAsync(existingCategory);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
