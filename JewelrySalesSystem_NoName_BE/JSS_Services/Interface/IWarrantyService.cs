using JSS_BusinessObjects;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_Services.Interface
{
    public interface IWarrantyService
    {
        public Task<WarrantyResponse> GetWarrantyDetail(Guid id);
        public Task<IPaginate<WarrantyResponse>> GetWarranties(int page, int size);
        public Task<IPaginate<WarrantyResponse>> GetWarrantiesNo(int page, int size);

        Task<List<WarrantyCreateResponse>> CreateWarranty(List<WarrantyRequest> list, string phone);
        Task<WarrantyResponse> UpdateWarranty(Guid id, WarrantyUpdateRequest request);

        Task<WarrantyResponse> GetDetailById(Guid id);
    }
}