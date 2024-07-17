using Firebase.Storage;
using JSS_BusinessObjects;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Response;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Repositories.Repo.Interface;
using JSS_Services.Interface;
using Microsoft.Extensions.Logging;

namespace JSS_Services.Implement
{
    public class DiamondService : IDiamondService
    {
        private readonly string _bucket = "jssimage-253a4.appspot.com";

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DiamondService> _logger;

        public DiamondService(IUnitOfWork unitOfWork, ILogger<DiamondService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IPaginate<DiamondResponse>> GetAllDiamondsAsync(int page, int size)
        {
            IPaginate<DiamondResponse> list = await _unitOfWork.DiamondRepository.GetList(
                selector: x => new DiamondResponse(x.Id, x.DiamondName, x.Code, x.Carat, x.Color, x.Clarity, x.Cut,
                    x.Price, x.ImageDiamond, x.Quantity, x.InsDate, x.PeriodWarranty, x.Deflag),
                predicate: x => x.Deflag == true,
                orderBy: x => x.OrderByDescending(x => x.Id),
                page: page,
                size: size);
            return list;
        }

        public async Task<int> GetTotalDiamondCountAsync()
        {
            var diamondRepository = _unitOfWork.GetRepository<Diamond>();
            return await diamondRepository.CountAsync();
        }

        public async Task<DiamondResponse> SearchDiamondByCodeAsync(string code)
        {
            if (string.IsNullOrEmpty(code) || code.Length < 8)
            {
                return null;
            }

            var diamond = await _unitOfWork.DiamondRepository.FirstOrDefaultAsync(
                p => p.Code == code);

            if (diamond != null)
            {
                return new DiamondResponse(diamond.Id, diamond.DiamondName, diamond.Code, diamond.Carat, diamond.Color, diamond.Clarity, diamond.Cut,
                diamond.Price, diamond.ImageDiamond, diamond.Quantity, diamond.InsDate, diamond.PeriodWarranty, diamond.Deflag);
            }

            return null;
        }

        public async Task<IEnumerable<DiamondResponse>> AutocompleteDiamondsAsync(string query)
        {
            var diamonds = await _unitOfWork.DiamondRepository.GetListAsync(
                selector: diamond => new DiamondResponse(diamond.Id, diamond.DiamondName, diamond.Code, diamond.Carat, diamond.Color, diamond.Clarity, diamond.Cut,
                diamond.Price, diamond.ImageDiamond, diamond.Quantity, diamond.InsDate, diamond.PeriodWarranty, diamond.Deflag),
                predicate: diamond => diamond.DiamondName.Contains(query) || diamond.Code.Contains(query),
                orderBy: diamond => diamond.OrderBy(diamond => diamond.DiamondName)
            );

            return diamonds;
        }

        public async Task<Diamond> GetDiamondByIdAsync(Guid id)
        {
            return await _unitOfWork.DiamondRepository.FirstOrDefaultAsync(d => d.Id == id);
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
                diamond.InsDate = DateTime.Now;
                diamond.Deflag = true;

                await _unitOfWork.DiamondRepository.InsertAsync(diamond);
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
                var existingDiamond = await _unitOfWork.DiamondRepository.FirstOrDefaultAsync(d => d.Id == id);
                if (existingDiamond == null) return null;

                existingDiamond.DiamondName = diamond.DiamondName ?? existingDiamond.DiamondName;
                existingDiamond.Code = diamond.Code ?? existingDiamond.Code;
                existingDiamond.Carat = diamond.Carat ?? existingDiamond.Carat;
                existingDiamond.Color = diamond.Color ?? existingDiamond.Color;
                existingDiamond.Clarity = diamond.Clarity ?? existingDiamond.Clarity;
                existingDiamond.Cut = diamond.Cut ?? existingDiamond.Cut;
                existingDiamond.Price = diamond.Price ?? existingDiamond.Price;
                existingDiamond.Quantity = diamond.Quantity ?? existingDiamond.Quantity;
                existingDiamond.PeriodWarranty = diamond.PeriodWarranty ?? existingDiamond.PeriodWarranty;
                existingDiamond.Deflag = diamond.Deflag ?? existingDiamond.Deflag;

                if (imageStream != null)
                {
                    var imageUrl = await UploadImageToFirebase(imageStream, imageName);
                    existingDiamond.ImageDiamond = imageUrl;
                }
                else if (imageStream == null)
                {
                    diamond.ImageDiamond = existingDiamond.ImageDiamond;
                }

                existingDiamond.UpsDate = DateTime.Now;
                _unitOfWork.DiamondRepository.UpdateAsync(existingDiamond);
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
                var existingDiamond = await _unitOfWork.DiamondRepository.FirstOrDefaultAsync(a => a.Id == id);
                if (existingDiamond == null) return false;

                _unitOfWork.DiamondRepository.DeleteAsync(existingDiamond);
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
