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
    public interface IDiamondService
    {
        Task<IPaginate<DiamondResponse>> GetAllDiamondsAsync(int page, int size);
        Task<DiamondResponse> SearchDiamondByCodeAsync(string code);
        Task<IEnumerable<DiamondResponse>> AutocompleteDiamondsAsync(string query);
        Task<Diamond> GetDiamondByIdAsync(Guid id);
        Task<Diamond> CreateDiamondAsync(Diamond diamond, Stream imageStream, string imageName);
        Task<Diamond> UpdateDiamondAsync(Guid id, Diamond diamond, Stream imageStream, string imageName);
        Task<bool> DeleteDiamondAsync(Guid id);
    }
}
