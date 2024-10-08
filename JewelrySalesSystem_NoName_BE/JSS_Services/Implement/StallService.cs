﻿using JSS_BusinessObjects;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Response;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Repositories.Repo.Interface;
using JSS_Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


// Do Huu Thuan
namespace JSS_Services.Implement
{
    public class StallService : IStallService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<StallService> _logger;

        public StallService(IUnitOfWork unitOfWork, ILogger<StallService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IPaginate<StallResponse>> GetListStallAsync(int page, int size)
        {
            IPaginate<StallResponse> listStall = await _unitOfWork.StallRepository.GetList(
                selector: x => new StallResponse(x.Id, x.Name, x.Number, x.StaffId ,x.Deflag),
                orderBy: x => x.OrderByDescending(x => x.Id),
                page: page,
                size: size); ;
            return listStall;
        }
        public async Task<IEnumerable<Stall>> GetAllStallsAsync()
        {
            try
            {
                var stallRepository = _unitOfWork.StallRepository;
                var stalls = await stallRepository.GetListAsync();
                return stalls;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all stalls");
                throw;
            }
        }

        public async Task<Stall> GetStallByIdAsync(Guid id)
        {
            try
            {
                var stallRepository = _unitOfWork.StallRepository;
                 var stall = await stallRepository.FirstOrDefaultAsync(s => s.Id == id);
                return stall;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting stall by ID");
                throw;
            }
        }







    }
}
