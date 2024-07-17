using Azure;
using JSS_BusinessObjects;
using JSS_DataAccessObjects;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_Repositories;
using JSS_Services.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSS_Repositories.Repo.Interface;

namespace JSS_Services.Implement
{
    public class ConditionWarrantyService : IConditionWarrantyServicecs
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ConditionWarrantyService> _logger;

        public ConditionWarrantyService(IUnitOfWork unitOfWork, ILogger<ConditionWarrantyService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ConditionWarrantyResponse> CreateConditionWarranty(ConditionWarrantyRequest newData)
        {
            ConditionWarranty conditionWarranty = new ConditionWarranty()
            {
                Id = Guid.NewGuid(),
                Condition = newData.Condition,
                InsDate = DateTime.Now,
                Deflag = true
            };

            await _unitOfWork.ConditionWarrantyRepository.InsertAsync(conditionWarranty);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (isSuccessful == false) return null;
            return new ConditionWarrantyResponse(conditionWarranty.Id, conditionWarranty.Condition, conditionWarranty.InsDate,
                                                 conditionWarranty.Deflag);
        }

        public async Task<IPaginate<ConditionWarrantyResponse>> GetListConditionWarranties(int page, int size)
        {
            IPaginate<ConditionWarrantyResponse> condition = await _unitOfWork.ConditionWarrantyRepository.GetList(
                selector: x => new ConditionWarrantyResponse(x.Id, x.Condition,x.InsDate, x.Deflag),
                predicate: x => x.Deflag.Equals(true),
                orderBy: x => x.OrderBy(x => x.InsDate),
                page: page,
                size: size);
            return condition;
        }

        public async Task<ConditionWarrantyResponse> GetConditionWarranty(Guid id)
        {
            var condition = await _unitOfWork.ConditionWarrantyRepository.FirstOrDefaultAsync(x => x.Id == id);
            return new ConditionWarrantyResponse(condition.Id, condition.Condition, condition.InsDate, condition.Deflag);
        }
    }
}
