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
using System.Drawing;
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

        public async Task<WarrantyResponse> GetWarrantyDetail(Guid id)
        {
            var warrantyDetail = await _unitOfWork.GetRepository<Warranty>().SingleOrDefaultAsync(
                selector: x => new WarrantyResponse(x.Id, x.DateOfPurchase, x.ExpirationDate, x.Period,x.Deflag, x.Status,
                                                    x.ConditionWarranty),
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
                                                    x.Deflag, x.Status,x.ConditionWarranty),
                page: page,
                size: size
                );
            return ListWarranties;
        }

        public async Task<IPaginate<WarrantyResponse>> GetWarrantiesNo(int page, int size)
        {
            IPaginate<WarrantyResponse> ListWarranties =
            await _unitOfWork.GetRepository<Warranty>().GetList(
                include: x => x.Include(x => x.ConditionWarranty),
                selector: x => new WarrantyResponse(x.Id, x.DateOfPurchase, x.ExpirationDate, x.Period, 
                                                   x.Deflag, x.Status, x.ConditionWarranty),
                page: page,
                size: size
            );
            return ListWarranties;
        }

        public async Task<List<WarrantyCreateResponse>> CreateWarranty(List<WarrantyRequest> newData, string phone)
        {
            List<WarrantyCreateResponse> listWarranty = new List<WarrantyCreateResponse>();
            foreach (WarrantyRequest item in newData)
            {
                var warItem = await _unitOfWork.GetRepository<Warranty>().FirstOrDefaultAsync(x => x.OrderDetailId == item.OrderDetailId);
                if (warItem != null)
                {
                    continue;
                }
                Warranty war = new Warranty()
                {
                    Id = Guid.NewGuid(),
                    DateOfPurchase = DateTime.Now,
                    ExpirationDate = DateTime.Now,
                    Period = item.Period,
                    Deflag = true,
                    OrderDetailId = (Guid)item.OrderDetailId,
                    ConditionWarrantyId = Guid.Parse("B1958280-788A-4BD8-95C3-EEF953878098"),
                    Status = "Active",
                    Note = item.Note,
                    Phone = phone
                };
                listWarranty.Add(new WarrantyCreateResponse { listWarrantyId = war.Id });
                await _unitOfWork.GetRepository<Warranty>().InsertAsync(war);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
                if (isSuccessful == false) return null;
            }
           
            return listWarranty;
        }
    }
}
