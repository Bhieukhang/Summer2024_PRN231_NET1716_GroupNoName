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
    public interface IMaterialService
    {
        Task<IEnumerable<Material>> GetAllMaterialsAsync();
        Task<Material> GetMaterialByIdAsync(Guid id);
        Task<MaterialResponse> CreateMaterialAsync(MaterialRequest newData);
        Task<Material> UpdateMaterialAsync(Guid id, Material updatedData);
        Task<bool> DeleteMaterialAsync(Guid id);
    }
}
