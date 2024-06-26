using AutoMapper;
using Azure;
using JSS_BusinessObjects;
using JSS_BusinessObjects.Helper;
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
                selector: x => new WarrantyResponse(x.Id, x.DateOfPurchase, x.ExpirationDate, x.Period, x.Deflag, x.Status,
                                                    x.Note),
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
                                                    x.Deflag, x.Status, x.Note, x.OrderDetail.Product.ProductName),
                include: x => x.Include(w => w.OrderDetail)
                                .ThenInclude(w => w.Product),
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
                                                   x.Deflag, x.Status, x.Note),
                page: page,
                size: size
            );
            return ListWarranties;
        }

        public async Task<List<WarrantyCreateResponse>> CreateWarranty(List<WarrantyRequest> list, string phone)
        {
            List<WarrantyCreateResponse> listWarranty = new List<WarrantyCreateResponse>();
            foreach (var item in list)
            {
                var WarId = Guid.NewGuid();
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
                    CodeWarranty = RandomCode.GenerateRandomCode(5).ToUpper()
                };
                await _unitOfWork.GetRepository<Warranty>().InsertAsync(warranty);
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
                listWarranty.Add(new WarrantyCreateResponse { listWarrantyId = WarId });
            }

            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccessful)
            {
                throw new Exception("Commit failed, no rows affected.");
            }
            return listWarranty;
        }

        public async Task<WarrantyResponse> UpdateWarranty(Guid id, WarrantyUpdateRequest request)
        {
            var warranty = await _unitOfWork.GetRepository<Warranty>().SingleOrDefaultAsync(
                predicate: x => x.Id == id
            );

            if (warranty == null)
            {
                throw new KeyNotFoundException("Warranty not found.");
            }

            if (DateTime.Now > warranty.ExpirationDate)
            {
                warranty.Status = "Expired";
                warranty.Note = "Đã quá thời hạn được bảo hành";
            }
            else
            {
                if (string.IsNullOrWhiteSpace(request.Note))
                {
                    throw new ArgumentException("Note is required when updating warranty within valid period.");
                }
                warranty.Status = request.Status;
                warranty.Note = request.Note;
            }

            _unitOfWork.GetRepository<Warranty>().UpdateAsync(warranty);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccessful)
            {
                throw new Exception("Commit failed, no rows affected.");
            }

            return new WarrantyResponse(warranty.Id, warranty.DateOfPurchase, warranty.ExpirationDate,
                warranty.Period, warranty.Deflag, warranty.Status, warranty.Note);
        }

        public async Task<WarrantyResponse> GetDetailById(Guid id)
        {
            var war = await _unitOfWork.GetRepository<Warranty>().FirstOrDefaultAsync(w => w.Id == id,
                                include: w => w.Include(w => w.WarrantyMappingConditions)
                                                .ThenInclude(w => w.ConditionWarranty)
                                                .Include(w => w.OrderDetail)
                                                .ThenInclude(w => w.Product));
            List<ConditionWarrantiesList> listCondition = war.WarrantyMappingConditions
       .Select(wmc => new ConditionWarrantiesList(wmc.ConditionWarranty))
       .ToList();
            return new WarrantyResponse(war.Id, war.DateOfPurchase, war.ExpirationDate, war.Period,
                                        war.Deflag, war.Status, war.Note, war.OrderDetail.Product.ProductName,
                                        listCondition);
        }
    }
}