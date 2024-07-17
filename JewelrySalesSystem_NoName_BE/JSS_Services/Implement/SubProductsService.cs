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
using JSS_Repositories.Repo.Interface;

namespace JSS_Services.Implement
{
    public class SubProductsService : ISubProductsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SubProductsService> _logger;

        public SubProductsService(IUnitOfWork unitOfWork, ILogger<SubProductsService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<SubProducts>> GetAllSubProductslsAsync()
        {
            return await _unitOfWork.SubProductsRepository.GetListAsync();
        }
    }
}
