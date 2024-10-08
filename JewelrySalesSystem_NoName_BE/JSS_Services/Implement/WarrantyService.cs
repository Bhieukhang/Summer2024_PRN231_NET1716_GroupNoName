﻿using AutoMapper;
using Azure;
using FirebaseAdmin.Messaging;
using JSS_BusinessObjects;
using JSS_BusinessObjects.Helper;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Repositories.Repo.Interface;
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
    public class WarrantyService : IWarrantyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<WarrantyService> _logger;

        public WarrantyService(IUnitOfWork unitOfWork, ILogger<WarrantyService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<WarrantyResponse> GetWarrantyDetail(Guid id)
        {
            var warrantyDetail = await _unitOfWork.WarrantyRepository.SingleOrDefaultAsync(
                selector: x => new WarrantyResponse(x.Id, x.DateOfPurchase, x.ExpirationDate, x.Period, x.Deflag, x.Status,
                                                    x.Note, x.OrderDetail.Product.ProductName),
                    predicate:
                    Guid.Empty.Equals(id)
                        ? x => true
                        : x => (x.Id.Equals(id)));
            return warrantyDetail;
        }

        public async Task<IPaginate<WarrantyResponse>> GetWarranties(int page, int size)
        {
            IPaginate<WarrantyResponse> ListWarranties =
            await _unitOfWork.WarrantyRepository.GetList(
                selector: x => new WarrantyResponse(x.Id, x.DateOfPurchase, x.ExpirationDate, x.Period,
                                                    x.Deflag, x.Status, x.Note, x.OrderDetail.Product.ProductName),
                include: x => x.Include(w => w.OrderDetail)
                                .ThenInclude(w => w.Product),
                orderBy: x => x.OrderBy(w => w.ExpirationDate),
                page: page,
                size: size
                );
            return ListWarranties;
        }

        public async Task<IPaginate<WarrantyResponse>> GetWarrantiesNo(int page, int size)
        {
            IPaginate<WarrantyResponse> ListWarranties =
            await _unitOfWork.WarrantyRepository.GetList(
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
                await _unitOfWork.WarrantyRepository.InsertAsync(warranty);
                foreach (var map in item.ConditionMap)
                {
                    WarrantyMappingCondition condition = new WarrantyMappingCondition()
                    {
                        Id = Guid.NewGuid(),
                        ConditionWarrantyId = map.ConditionWarrantyId,
                        WarrantyId = warranty.Id,
                        InsDate = DateTime.Now,
                    };
                    await _unitOfWork.WarrantyMappingConditionRepository.InsertAsync(condition);
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
            var warranty = await _unitOfWork.WarrantyRepository.SingleOrDefaultAsync(
        predicate: x => x.Id == id
    );

            if (warranty == null)
            {
                throw new KeyNotFoundException("Không tìm thấy bảo hành.");
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
                    throw new ArgumentException("Cần ghi chú khi cập nhật bảo hành trong thời hạn hiệu lực.");
                }
                warranty.Status = request.Status;
                warranty.Note = request.Note;
            }

            var oldDateOfPurchase = warranty.DateOfPurchase;
            var oldExpirationDate = warranty.ExpirationDate;
            var oldPeriod = warranty.Period;
            var oldDeflag = warranty.Deflag;
            var oldOrderDetailId = warranty.OrderDetailId;
            var oldPhone = warranty.Phone;

            warranty.DateOfPurchase = request.DateOfPurchase ?? warranty.DateOfPurchase;
            warranty.ExpirationDate = request.ExpirationDate ?? warranty.ExpirationDate;
            warranty.Period = request.Period ?? warranty.Period;
            warranty.Deflag = request.Deflag;
            warranty.OrderDetailId = request.OrderDetailId ?? warranty.OrderDetailId;
            warranty.Phone = request.Phone ?? warranty.Phone;

            _unitOfWork.WarrantyRepository.UpdateAsync(warranty);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccessful)
            {
                throw new Exception("Lưu dữ liệu thất bại, không có hàng nào được thay đổi.");
            }

            return new WarrantyResponse(warranty.Id, oldDateOfPurchase, oldExpirationDate,
                                        oldPeriod, oldDeflag, warranty.Status, warranty.Note,
                                        oldOrderDetailId, oldPhone);
        }


        public async Task<WarrantyResponse> GetDetailById(Guid id)
        {
            var war = await _unitOfWork.WarrantyRepository.FirstOrDefaultAsync(w => w.Id == id,
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

        public async Task<WarrantyResponse> SearchByCode(string code)
        {
            try
            {
                var war = await _unitOfWork.WarrantyRepository.FirstOrDefaultAsync(w => w.CodeWarranty == code,
                       include: w => w.Include(w => w.WarrantyMappingConditions)
                                                .ThenInclude(w => w.ConditionWarranty)
                                                .Include(w => w.OrderDetail)
                                                .ThenInclude(w => w.Product));
                return new WarrantyResponse(war.Id, war.DateOfPurchase, war.ExpirationDate, war.Period, 
                                            war.Deflag, war.Status, war.Note, war.OrderDetail.Product.ProductName);
            }
            catch (Exception e)
            {
                throw new Exception("Error search code warranty");
            }
        }

        public async Task<WarrantyResponse> SearchByPhone(String phone)
        {
            try
            {
                var war = await _unitOfWork.WarrantyRepository.FirstOrDefaultAsync(w => w.Phone == phone);
                return new WarrantyResponse(war.Id, war.DateOfPurchase, war.ExpirationDate, war.Period, war.Status, war.Note);
            }
            catch (Exception e)
            {
                throw new Exception("Error search code warranty");
            }
        }
    }
}