using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;

namespace JSS_Services.Interface
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
    }
}
