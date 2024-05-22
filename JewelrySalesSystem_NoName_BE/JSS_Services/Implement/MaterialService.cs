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

        public async Task<MaterialResponse> CreateMaterialAsync(MaterialRequest newData)
        {
            Material mate = new Material()
            {
                Id = Guid.NewGuid(),
                InsDate = DateTime.Now,
                MaterialName = newData.MaterialName,
                Weight = newData.Weight,
                Deflag = true, 
            };
            await _unitOfWork.GetRepository<Material>().InsertAsync(mate);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (isSuccessful == false) return null;
            return new MaterialResponse(mate.Id, mate.MaterialName, mate.Weight);
        }

        public async Task<Material> UpdateMaterialAsync(Guid id, Material updatedData)
        {
            var existingMaterial = await _unitOfWork.GetRepository<Material>().FirstOrDefaultAsync(a => a.Id == id);
            if (existingMaterial == null) return null;

            _unitOfWork.GetRepository<Material>().UpdateAsync(updatedData);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccessful) return null;
            return updatedData;
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
