using JSS_BusinessObjects.Models;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Services.Interface;
using Microsoft.Extensions.Logging;

namespace JSS_Services.Implement
{
    public class TransactionService : BaseService<TransactionService>, ITransactionService
    {
        public TransactionService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork,
            ILogger<TransactionService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _unitOfWork.GetRepository<Transaction>().GetListAsync();
        }
        
    }
}
