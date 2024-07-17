using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Response;
using JSS_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSS_Services.Interface;
using JSS_Repositories;
using JSS_DataAccessObjects;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using JSS_Repositories.Repo.Interface;

namespace JSS_Services.Implement
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<OrderDetailService> _logger;

        public OrderDetailService(IUnitOfWork unitOfWork, ILogger<OrderDetailService> logger) 
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        // Do Huu Thuan
        public async Task<OrderDetail> GetOrderDetailByIdAsync(Guid id)
        {
            return await _unitOfWork.OrderDetailRepository.FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync()
        {
            return await _unitOfWork.OrderDetailRepository.GetListAsync();
        }
        public async Task<OrderDetail> GetOrderDetailByOrderIdAsync(Guid id)
        {
            return await _unitOfWork.OrderDetailRepository.FirstOrDefaultAsync(
                predicate: a => a.OrderId == id,
                include: q => q
                .Include(od => od.Order)
                .Include(od => od.Product));
        }
    }
}
