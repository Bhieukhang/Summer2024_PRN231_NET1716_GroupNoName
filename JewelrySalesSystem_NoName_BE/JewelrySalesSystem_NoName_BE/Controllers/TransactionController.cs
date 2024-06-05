using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Helper;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _TransactionService;

        public TransactionController(ITransactionService TransactionService)
        {
            _TransactionService = TransactionService;
        }

        #region GetAllTransactions
        /// <summary>
        /// Get all Transactions.
        /// </summary>
        /// <returns>List of Transactions.</returns>
        /// GET : api/Transaction
        #endregion
        [Authorize(Roles = "Manager, Staff")]
        [HttpGet(ApiEndPointConstant.Transaction.TransactionEndpoint)]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAllTransactionsAsync()
        {
            var Transactions = await _TransactionService.GetAllTransactionsAsync();
            return Ok(Transactions);
        }

        
    }
}
