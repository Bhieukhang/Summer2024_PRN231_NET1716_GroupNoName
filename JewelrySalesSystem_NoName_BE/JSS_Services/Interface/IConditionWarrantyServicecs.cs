using JSS_BusinessObjects;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_Services.Interface
{
    public interface IConditionWarrantyServicecs
    {
        public Task<IPaginate<ConditionWarrantyResponse>> GetListConditionWarranties(int page, int size);
        public Task<ConditionWarrantyResponse> GetConditionWarranty(Guid id);
        public Task<ConditionWarrantyResponse> CreateConditionWarranty(ConditionWarrantyRequest newData);
    }
}
