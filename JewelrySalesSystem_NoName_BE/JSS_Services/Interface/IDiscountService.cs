using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_Services.Interface
{
    public interface IDiscountService
    {
        Task<DiscountResponse> ConfirmDiscountToManager(DiscountRequest confirmData);
    }
}
