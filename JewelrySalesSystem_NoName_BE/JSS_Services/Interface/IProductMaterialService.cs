using JSS_BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_Services.Interface
{
    public interface IProductMaterialService
    {
        Task<IEnumerable<ProductMaterial>> GetAllProductMaterialsAsync();
        Task<ProductMaterial> GetProductMaterialByIdAsync(Guid id);
        Task<ProductMaterial> CreateProductMaterialAsync(ProductMaterial newData);
        Task<ProductMaterial> UpdateProductMaterialAsync(Guid id, ProductMaterial updatedData);
        Task<bool> DeleteProductMaterialAsync(Guid id);
    }
}
