using JSS_BusinessObjects.Payload.Response;
using JSS_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSS_BusinessObjects.Models;

namespace JSS_Services.Interface
{
    public interface IProcessPriceService
    {
        public Task<IPaginate<ProcessPriceResponse>> GetListProcessPriceAsync(int page, int size);
        Task<ProcessPrice> UpdateProcessPriceAsync(int id, ProcessPrice updatedData);
    }
}
