using JSS_Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JSS_BusinessObjects.Payload.Response;
using JSS_Services.Implement;
using JSS_BusinessObjects.Models;
using JewelrySalesSystem_NoName_BE.Extenstion;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        #region GetAccount
        /// <summary>
        /// Get all accounts.
        /// </summary>
        /// <returns>List of accounts.</returns>
        // GET: api/Account
        #endregion
        // [HttpGet]
        [HttpGet(ApiEndPointConstant.Account.AccountEndpoint)]
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccountsAsync()
         {
            var accounts = await _accountService.GetAllAccountsAsync();
            return Ok(accounts);
         }

        #region GetAccountById
        /// <summary>
        /// Get a account by its ID.
        /// </summary>
        /// <param name="id">The ID of the stall to retrieve.</param>
        /// <returns>The stall with the specified ID.</returns>
        // GET: api/Account/{id}
        #endregion
        // [HttpGet("{id}")]
        [HttpGet(ApiEndPointConstant.Account.AccountByIdEndpoint)]
        public async Task<ActionResult<Account>> GetAccountByIdAsync(Guid id) 
        {
            var account = await _accountService.GetAccountByIdAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }
    }
}
