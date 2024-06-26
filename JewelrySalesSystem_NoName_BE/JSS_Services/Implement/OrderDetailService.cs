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

namespace JSS_Services.Implement
{
    public class OrderDetailService : BaseService<OrderDetailService>, IOrderDetailService
    {
        public OrderDetailService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork, ILogger<OrderDetailService> logger) : base(unitOfWork, logger)
        {
        }
        // Do Huu Thuan
        public async Task<OrderDetail> GetOrderDetailByIdAsync(Guid id)
        {
            return await _unitOfWork.GetRepository<OrderDetail>().FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync()
        {
            return await _unitOfWork.GetRepository<OrderDetail>().GetListAsync();
        }

    }
}
