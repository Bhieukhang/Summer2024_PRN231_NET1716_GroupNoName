using AutoMapper;
using JSS_BusinessObjects;
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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JSS_Services.Implement
{
    public class WarrantyService : BaseService<WarrantyService>, IWarrantyService
    {
        public WarrantyService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork, ILogger<WarrantyService> logger) : base(unitOfWork, logger)
        {
        }

        public Task<WarrantyResponse> GetWarrantyDetail(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IPaginate<WarrantyResponse>> GetWarranties(int page, int size)
        {
            IPaginate<WarrantyResponse> ListWarranties =
            await _unitOfWork.GetRepository<Warranty>().GetList(
                selector: x => new WarrantyResponse(x.Id, x.DateOfPurchase, x.ExpirationDate),
                page: page,
                size: size
                );
            return ListWarranties;
        }

        public async Task<WarrantyResponse> CreateWarranty(WarrantyRequest newData)
        {
            Warranty war = new Warranty()
            {
                Id = Guid.NewGuid(),
                DateOfPurchase = DateTime.Now,
                ExpirationDate = DateTime.Now,
                Period = newData.Period,
                Deflag = true,
                ProductId = newData.ProductId,
                OrderDetailId = null,
                ConditionWarrantyId = Guid.Parse("B1958280-788A-4BD8-95C3-EEF953878098"),
                Status = "Active"
            };   
            await _unitOfWork.GetRepository<Warranty>().InsertAsync(war);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (isSuccessful == false) return null;
            return new WarrantyResponse(war.Id, war.DateOfPurchase, war.ExpirationDate);
        }
    }
}
