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
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(Guid? id);
        Task<CategoryResponse> CreateCategoryAsync(CategoryRequest newData);
        Task<Category> UpdateCategoryAsync(Guid id, Category updatedData);
        Task<bool> DeleteCategoryAsync(Guid id);
    }
}
