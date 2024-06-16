using JSS_BusinessObjects;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_Services.Interface
{
    public interface ISubProductsService
    {
        Task<IEnumerable<SubProducts>> GetAllSubProductslsAsync();
        
    }
}
