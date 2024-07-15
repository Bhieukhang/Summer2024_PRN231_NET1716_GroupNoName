using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Services.Interface;
using Microsoft.Extensions.Logging;

namespace JSS_Services.Implement
{
    public class MaterialService : BaseService<MaterialService>, IMaterialService
    {
        public MaterialService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork, ILogger<MaterialService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<IEnumerable<Material>> GetAllMaterialsAsync()
        {
            return await _unitOfWork.GetRepository<Material>().GetListAsync();
        }

        public async Task<Material> GetMaterialByIdAsync(Guid id)
        {
            return await _unitOfWork.GetRepository<Material>().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<MaterialResponse> SearchMaterialByNameAsync(string materialName)
        {
            if (string.IsNullOrEmpty(materialName))
            {
                return null;
            }

            var material = await _unitOfWork.GetRepository<Material>().FirstOrDefaultAsync(
                p => p.MaterialName == materialName
            );

            if (material != null)
            {
                return new MaterialResponse(material.Id, material.MaterialName, material.InsDate);
            }

            return null;
        }

        public async Task<MaterialResponse> CreateMaterialAsync(MaterialRequest newData)
        {
            Material mate = new Material()
            {
                Id = Guid.NewGuid(),
                InsDate = DateTime.Now,
                MaterialName = newData.MaterialName
            };
            await _unitOfWork.GetRepository<Material>().InsertAsync(mate);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (isSuccessful == false) return null;
            return new MaterialResponse(mate.Id, mate.MaterialName, mate.InsDate);
        }

        public async Task<Material> UpdateMaterialAsync(Guid id, Material updatedData)
        {
            var existingMaterial = await _unitOfWork.GetRepository<Material>().FirstOrDefaultAsync(a => a.Id == id);
            if (existingMaterial == null) return null;

            existingMaterial.MaterialName = updatedData.MaterialName;

            _unitOfWork.GetRepository<Material>().UpdateAsync(existingMaterial);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccessful) return null;
            return existingMaterial;
        }

        public async Task<bool> DeleteMaterialAsync(Guid id)
        {
            var existingMaterial = await _unitOfWork.GetRepository<Material>().FirstOrDefaultAsync(a => a.Id == id);
            if (existingMaterial == null) return false;

            _unitOfWork.GetRepository<Material>().DeleteAsync(existingMaterial);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
