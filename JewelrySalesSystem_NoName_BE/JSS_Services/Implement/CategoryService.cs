using JSS_DataAccessObjects;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_Repositories;
using JSS_Services.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSS_Repositories.Repo.Interface;

namespace JSS_Services.Implement
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(IUnitOfWork unitOfWork, ILogger<CategoryService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _unitOfWork.CategoryRepository.GetListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(Guid? id)
        {
            return await _unitOfWork.CategoryRepository.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<CategoryResponse> SearchCategoryByNameAsync(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                return null;
            }

            var category = await _unitOfWork.CategoryRepository.FirstOrDefaultAsync(
                p => p.Name == categoryName
            );

            if (category != null)
            {
                return new CategoryResponse(category.Id, category.Name);
            }

            return null;
        }

        public async Task<CategoryResponse> CreateCategoryAsync(CategoryRequest newData)
        {
            Category cate = new Category()
            {
                Id = Guid.NewGuid(),
                Name = newData.Name,
            };
            await _unitOfWork.CategoryRepository.InsertAsync(cate);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (isSuccessful == false) return null;
            return new CategoryResponse(cate.Id, cate.Name);
        }

        public async Task<Category> UpdateCategoryAsync(Guid id, Category updatedData)
        {
            var existingCategory = await _unitOfWork.CategoryRepository.FirstOrDefaultAsync(a => a.Id == id);
            if (existingCategory == null) return null;

            existingCategory.Name = updatedData.Name;

            _unitOfWork.CategoryRepository.UpdateAsync(existingCategory);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccessful) return null;
            return existingCategory;
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var existingCategory = await _unitOfWork.CategoryRepository.FirstOrDefaultAsync(a => a.Id == id);
            if (existingCategory == null) return false;

            _unitOfWork.CategoryRepository.DeleteAsync(existingCategory);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
