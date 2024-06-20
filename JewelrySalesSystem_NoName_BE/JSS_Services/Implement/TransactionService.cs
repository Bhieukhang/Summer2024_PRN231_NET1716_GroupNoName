using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Response;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Services.Interface;
using Microsoft.Extensions.Logging;
using System.Data.Entity;

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

        public async Task<TransactionResponse> GetDetailTransactionByOrderId(Guid orderId)
        {
            var transaction = await _unitOfWork.GetRepository<Transaction>()
                .FirstOrDefaultAsync(
                    predicate: t => t.OrderId == orderId
                );
            var orderItem = await _unitOfWork.GetRepository<Order>().FirstOrDefaultAsync(o => o.Id == transaction.OrderId);

            OrderResponse order = new OrderResponse()
            {
                Id = transaction.OrderId,
                CustomerId = orderItem.CustomerId,
                Type= orderItem.Type,
                InsDate = orderItem.InsDate,
                TotalPrice = orderItem.TotalPrice,
                MaterialProcessPrice = orderItem.MaterialProcessPrice,
            };
            TransactionResponse transactionResponse = new TransactionResponse()
            {
                Id = transaction.Id,
                OrderId = orderId,
                Description = transaction.Description,
                Currency = transaction.Currency,
                TotalPrice = transaction.TotalPrice,
                InsDate = transaction.InsDate,
                Order = order
            };
            return transactionResponse;
        }

    }
}
