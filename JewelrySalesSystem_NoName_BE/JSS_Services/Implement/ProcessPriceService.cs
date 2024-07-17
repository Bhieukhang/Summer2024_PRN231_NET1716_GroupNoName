using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Response;
using JSS_BusinessObjects;
using JSS_DataAccessObjects;
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
    public class ProcessPriceService : IProcessPriceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProcessPriceService> _logger;

        public ProcessPriceService(IUnitOfWork unitOfWork, ILogger<ProcessPriceService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IPaginate<ProcessPriceResponse>> GetListProcessPriceAsync(int page, int size)
        {
            IPaginate<ProcessPriceResponse> list = await _unitOfWork.ProcessPriceRepository.GetList(
                selector: x => new ProcessPriceResponse(x.ProcesspriceId, x.ProcessPrice1, x.Size, x.CategoryId, x.Description, x.UpsDate, x.Status),
                orderBy: x => x.OrderByDescending(x => x.ProcesspriceId),
                page: page,
                size: size); ;
            return list;
        }
        public async Task<ProcessPrice> UpdateProcessPriceAsync(int id, ProcessPrice updatedData)
        {
            var existingProcessPrice = await _unitOfWork.ProcessPriceRepository.FirstOrDefaultAsync(a => a.ProcesspriceId == id);
            if (existingProcessPrice == null) return null;

            existingProcessPrice.ProcessPrice1 = updatedData.ProcessPrice1;
            existingProcessPrice.Size = updatedData.Size;
            existingProcessPrice.CategoryId = updatedData.CategoryId;
            existingProcessPrice.Description = updatedData.Description;
            existingProcessPrice.UpsDate = DateTime.Now;
            existingProcessPrice.Status = updatedData.Status;


            _unitOfWork.ProcessPriceRepository.UpdateAsync(existingProcessPrice);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccessful) return null;
            return existingProcessPrice;
        }

        public async Task<ProcessPrice> GetProcessPriceByIdAsync(int id)
        {
            try
            {
                var processPriceRepository = _unitOfWork.ProcessPriceRepository;
                var ProcessPrice = await processPriceRepository.FirstOrDefaultAsync(s => s.ProcesspriceId == id);
                return ProcessPrice;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting stall by ID");
                throw;
            }
        }
    }
}
