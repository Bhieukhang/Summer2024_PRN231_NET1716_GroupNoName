using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Response;
using JSS_BusinessObjects;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Services.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_Services.Implement
{
    public class SubProductsService : BaseService<SubProductsService>, ISubProductsService
    {
        public SubProductsService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork, ILogger<SubProductsService> logger) : base(unitOfWork, logger)
        {
        }
        public async Task<IEnumerable<SubProducts>> GetAllSubProductslsAsync()
        {
            return await _unitOfWork.GetRepository<SubProducts>().GetListAsync();
        }
    }
}
