using BusinessObjects.Mo;
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


// Do Huu Thuan
namespace JSS_Services.Implement
{
    public class ProcessPriceService : BaseService<ProcessPrice>, IProcessPriceService
    {
        public ProcessPriceService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork, ILogger<ProcessPrice> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<IPaginate<ProcessPriceResponse>> GetListProcessPriceAsync(int page, int size)
        {
            IPaginate<ProcessPriceResponse> list = await _unitOfWork.GetRepository<ProcessPrice>().GetList(
                selector: x => new ProcessPriceResponse(x.ProcesspriceId, x.ProcessPrice1, x.Size, x.CategoryId, x.Description, x.UpsDate, x.Status),
                orderBy: x => x.OrderByDescending(x => x.ProcesspriceId),
                page: page,
                size: size); ;
            return list;
        }
    }
}
