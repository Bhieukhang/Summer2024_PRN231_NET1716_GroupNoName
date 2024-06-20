using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;

namespace JSS_Services.Interface
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
        Task<TransactionResponse> GetDetailTransactionByOrderId(Guid orderId);
    }
}
