using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Helper;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

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

        #region GetOrderDetail
        /// <summary>
        /// Get detail order and transaction by OrderId.
        /// </summary>
        /// <returns>Item order and transactions.</returns>
        /// GET : api/Transaction/orderId
        #endregion
        //[Authorize(Roles = "Manager, Staff")]
        [HttpGet(ApiEndPointConstant.Transaction.TransactionOrderEndpoint)]
        public async Task<ActionResult<TransactionResponse>> GetOrderDetail(Guid orderId)
        {
            var Transactions = await _TransactionService.GetDetailTransactionByOrderId(orderId);
            var result = JsonConvert.SerializeObject(Transactions, Formatting.Indented);
            return Ok(result);
        }
    }
}
