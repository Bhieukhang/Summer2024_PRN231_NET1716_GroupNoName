using JSS_BusinessObjects.Payload.Request;
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
    public interface IStallService
    {
        Task<IEnumerable<Stall>> GetAllStallsAsync();
        Task<Stall> GetStallByIdAsync(Guid id);
        public Task<IPaginate<StallResponse>> GetListStallAsync(int page, int size);
    }
}
