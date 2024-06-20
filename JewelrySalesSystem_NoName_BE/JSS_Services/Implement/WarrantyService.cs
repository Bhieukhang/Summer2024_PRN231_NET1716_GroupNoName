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
                                                    x.ConditionWarrantyId, x.Note),
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
                                                    x.Deflag, x.Status,x.ConditionWarrantyId,x.Note),
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
                                                   x.Deflag, x.Status, x.ConditionWarrantyId, x.Note),
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

        public async Task<WarrantyResponse> UpdateWarranty(Guid id, WarrantyRequest request)
        {
            var warranty = await _unitOfWork.GetRepository<Warranty>().SingleOrDefaultAsync(
                predicate: x => x.Id == id);

            if (warranty == null)
            {
                throw new KeyNotFoundException("Warranty not found.");
            }

            var currentDate = DateTime.UtcNow;

            if (currentDate > warranty.ExpirationDate)
            {
                warranty.Status = "Expired";
                warranty.Note = "Đã quá thời hạn được bảo hành";
                _unitOfWork.GetRepository<Warranty>().UpdateAsync(warranty);
                await _unitOfWork.CommitAsync();
            }
            else if (currentDate >= warranty.DateOfPurchase && currentDate <= warranty.ExpirationDate)
            {
                warranty.Status = "Valid for Warranty";

                if (string.IsNullOrWhiteSpace(request.Note))
                {
                    throw new ArgumentException("Note is required.");
                }
                warranty.Note = request.Note;

                _unitOfWork.GetRepository<Warranty>().UpdateAsync(warranty);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                throw new ArgumentException("Invalid DateOfPurchase or ExpirationDate.");
            }

            return new WarrantyResponse(warranty.Id, warranty.DateOfPurchase, warranty.ExpirationDate, warranty.Period, warranty.Status, warranty.ConditionWarrantyId, warranty.Note);
        }

    }
}
