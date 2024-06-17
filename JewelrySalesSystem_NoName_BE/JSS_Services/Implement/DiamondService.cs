using Firebase.Storage;
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
    public class DiamondService : BaseService<DiamondService>, IDiamondService
    {
        private readonly string _bucket = "jssimage-253a4.appspot.com";

        public DiamondService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork, ILogger<DiamondService> logger)
            : base(unitOfWork, logger)
        {
        }

        public async Task<IEnumerable<Diamond>> GetAllDiamondsAsync()
        {
            return await _unitOfWork.GetRepository<Diamond>().GetListAsync();
        }

        public async Task<Diamond> GetDiamondByIdAsync(Guid id)
        {
            return await _unitOfWork.GetRepository<Diamond>().FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Diamond> CreateDiamondAsync(Diamond diamond, Stream imageStream, string imageName)
        {
            try
            {
                var imageUrl = await UploadImageToFirebase(imageStream, imageName);
                if (string.IsNullOrEmpty(imageUrl))
                {
                    throw new Exception("Image upload failed, URL is empty.");
                }

                diamond.Id = Guid.NewGuid();
                diamond.ImageDiamond = imageUrl;

                await _unitOfWork.GetRepository<Diamond>().InsertAsync(diamond);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
                if (!isSuccessful)
                {
                    throw new Exception("Commit failed, no rows affected.");
                }

                return diamond;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the diamond.");
                throw;
            }
        }

        public async Task<Diamond> UpdateDiamondAsync(Guid id, Diamond diamond, Stream imageStream, string imageName)
        {
            try
            {
                var existingDiamond = await _unitOfWork.GetRepository<Diamond>().FirstOrDefaultAsync(d => d.Id == id);
                if (existingDiamond == null) return null;

                existingDiamond.DiamondName = diamond.DiamondName ?? existingDiamond.DiamondName;
                existingDiamond.Carat = diamond.Carat ?? existingDiamond.Carat;
                existingDiamond.Color = diamond.Color ?? existingDiamond.Color;
                existingDiamond.Clarity = diamond.Clarity ?? existingDiamond.Clarity;
                existingDiamond.Cut = diamond.Cut ?? existingDiamond.Cut;
                existingDiamond.Price = diamond.Price ?? existingDiamond.Price;
                existingDiamond.JewelryId = diamond.JewelryId != Guid.Empty ? diamond.JewelryId : existingDiamond.JewelryId;

                if (imageStream != null)
                {
                    var imageUrl = await UploadImageToFirebase(imageStream, imageName);
                    existingDiamond.ImageDiamond = imageUrl;
                }

                _unitOfWork.GetRepository<Diamond>().UpdateAsync(existingDiamond);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
                if (!isSuccessful) return null;

                return existingDiamond;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the diamond.");
                throw;
            }
        }

        public async Task<bool> DeleteDiamondAsync(Guid id)
        {
            try
            {
                var existingDiamond = await _unitOfWork.GetRepository<Diamond>().FirstOrDefaultAsync(a => a.Id == id);
                if (existingDiamond == null) return false;

                _unitOfWork.GetRepository<Diamond>().DeleteAsync(existingDiamond);
                return await _unitOfWork.CommitAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the diamond.");
                throw;
            }
        }

        private async Task<string> UploadImageToFirebase(Stream imageStream, string imageName)
        {
            var storage = new FirebaseStorage(_bucket);
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageName);

            var uploadTask = storage
                .Child("uploads")
                .Child(uniqueFileName)
                .PutAsync(imageStream);

            return await uploadTask;
        }
    }
}
