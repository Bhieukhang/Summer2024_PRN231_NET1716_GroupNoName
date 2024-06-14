using AutoMapper;
using Azure;
using JSS_BusinessObjects;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JSS_Services.Implement
{
    public class WarrantyService : BaseService<WarrantyService>, IWarrantyService
    {
        public WarrantyService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork, 
            ILogger<WarrantyService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<WarrantyResponse> GetWarrantyDetail(Guid id)
        {
            var warrantyDetail = await _unitOfWork.GetRepository<Warranty>().SingleOrDefaultAsync(
                selector: x => new WarrantyResponse(x.Id, x.DateOfPurchase, x.ExpirationDate, x.Period,x.Deflag, x.Status,
                                                    x.ConditionWarrantyId),
                    predicate:
                    Guid.Empty.Equals(id)
                        ? x => true
                        : x => (x.Id.Equals(id)));
            return warrantyDetail;
        }

        public async Task<IPaginate<WarrantyResponse>> GetWarranties(int page, int size)
        {
            IPaginate<WarrantyResponse> ListWarranties =
            await _unitOfWork.GetRepository<Warranty>().GetList(
                selector: x => new WarrantyResponse(x.Id, x.DateOfPurchase, x.ExpirationDate, x.Period,
                                                    x.Deflag, x.Status,x.ConditionWarrantyId),
                page: page,
                size: size
                );
            return ListWarranties;
        }

        public async Task<IPaginate<WarrantyResponse>> GetWarrantiesNo(int page, int size)
        {
            IPaginate<WarrantyResponse> ListWarranties =
            await _unitOfWork.GetRepository<Warranty>().GetList(
                include: x => x.Include(x => x.WarrantyMappingConditions),
                selector: x => new WarrantyResponse(x.Id, x.DateOfPurchase, x.ExpirationDate, x.Period, 
                                                   x.Deflag, x.Status, x.ConditionWarrantyId),
                page: page,
                size: size
            );
            return ListWarranties;
        }

        public async Task<List<WarrantyCreateResponse>> CreateWarranty(List<WarrantyRequest> list, string phone)
        {
            List<WarrantyCreateResponse> listWarranty = new List<WarrantyCreateResponse>();
            var WarId = Guid.NewGuid();
            foreach (var item in list)
            {
                Warranty warranty = new Warranty()
                {
                    Id = WarId,
                    DateOfPurchase = item.DateOfPurchase,
                    ExpirationDate = item.ExpirationDate,
                    Period = item.Period,
                    Deflag = true,
                    OrderDetailId = (Guid)item.OrderDetailId,
                    Phone = phone,
                    Status = "Active",
                    Note = item.Note,
                    //ConditionWarrantyId = Guid.NewGuid(),
                };
                await _unitOfWork.GetRepository<Warranty>().InsertAsync(warranty);
                //var conditionId = item.ConditionMap.ConditionWarrantyId;
                foreach (var map in item.ConditionMap)
                {
                    WarrantyMappingCondition condition = new WarrantyMappingCondition()
                    {
                        Id = Guid.NewGuid(),
                        ConditionWarrantyId = map.ConditionWarrantyId,
                        WarrantyId = warranty.Id,
                        InsDate = DateTime.Now,
                    };
                    await _unitOfWork.GetRepository<WarrantyMappingCondition>().InsertAsync(condition);
                }
            }
           
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccessful)
            {
                throw new Exception("Commit failed, no rows affected.");
            }
            listWarranty.Add(new WarrantyCreateResponse { listWarrantyId = WarId });
            return listWarranty;
        }
    }
}
