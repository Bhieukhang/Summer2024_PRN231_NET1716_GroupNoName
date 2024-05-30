using JSS_Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JSS_BusinessObjects.Payload.Response;
using JSS_Services.Implement;
using JSS_BusinessObjects.Models;
using JewelrySalesSystem_NoName_BE.Extenstion;
using Newtonsoft.Json;
using System.Security.Claims;
using JSS_BusinessObjects.DTO;
using Microsoft.AspNetCore.Authorization;

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
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetListAccountAsync(int page, int size)
        {
            var list = await _accountService.GetListAccountAsync(page, size);
            var accounts = JsonConvert.SerializeObject(list, Formatting.Indented);
            return Ok(accounts);
        }

        #region GetAccountByRoleId
        /// <summary>
        /// Get all accountsByRoleId.
        /// </summary>
        /// <returns>List of accountsByRoleId.</returns>
        // GET: api/Account
        #endregion
        [HttpGet(ApiEndPointConstant.Account.AccountByRoleIdEndpoint)] 
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetListAccountByRoleIdAsync(Guid roleId, int page, int size)
        {
            var list = await _accountService.GetListAccountByRoleIdAsync(roleId, page, size);
            var accounts = JsonConvert.SerializeObject(list, Formatting.Indented);
            return Ok(accounts);
        }

        #region GetTotalAccount
        /// <summary>
        /// Get Total accounts.
        /// </summary>
        /// <returns>List of accounts.</returns>
        // GET: api/Account
        #endregion
        [HttpGet(ApiEndPointConstant.Account.TotalAccountEndpoint)]
        public async Task<ActionResult<int>> GetTotalAccountCount()
        {
            var totalAccountCount = await _accountService.GetTotalAccountCountAsync();
            return Ok(totalAccountCount);
        }

        #region GetActiveAccount
        /// <summary>
        /// Get active accounts.
        /// </summary>
        /// <returns>List of accounts.</returns>
        // GET: api/Account
        #endregion
        [HttpGet(ApiEndPointConstant.Account.ActiveAccountEndpoint)]
        public async Task<ActionResult<int>> GetActiveAccountCount()
        {
            var activeAccountCount = await _accountService.GetActiveAccountCountAsync();
            return Ok(activeAccountCount);
        }

        #region GetAccountById
        /// <summary>
        /// Get a account by ID.
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

        #region UpdateAccount
        /// <summary>
        /// Update an account by ID.
        /// </summary>
        /// <param name="id">The ID of the account to update.</param>
        /// <param name="account">The account object with updated data.</param>
        /// <returns>The updated account object.</returns>
        // PUT: api/Account/{id}
        #endregion
        //  [HttpPut("{id}")]
        [HttpPut(ApiEndPointConstant.Account.AccountByIdEndpoint)]
        public async Task<ActionResult<Account>> UpdateAccountAsync(Guid id, [FromBody] Account account)
        {
            if (id != account.Id)
            {
                return BadRequest();
            }

            var updatedAccount = await _accountService.UpdateAccountAsync(id, account);

            if (updatedAccount == null)
            {
                return NotFound();
            }

            return Ok(updatedAccount);
        }

        #region CreateAccount
        /// <summary>
        /// Create a new account.
        /// </summary>
        /// <param name="account">The account object with the data to create.</param>
        /// <returns>The created account object.</returns>
        // POST: api/Account
        #endregion
        //[HttpPost]
        [HttpPost(ApiEndPointConstant.Account.AccountEndpoint)]
        public async Task<ActionResult<Account>> CreateAccountAsync(Account account)
        {
            var createdAccount = await _accountService.CreateAccountAsync(account);
            return CreatedAtAction(nameof(GetAccountByIdAsync), new { id = createdAccount.Id }, createdAccount);
        }

        #region AccountProfile
        /// <summary>
        /// Profile account.
        /// </summary>
        /// <param name="profile">The account object with the data.</param>
        /// <returns>Profile.</returns>
        #endregion
        [Authorize]
        [HttpGet(ApiEndPointConstant.Account.AccountProfileEndpoint)]
        public async Task<IActionResult> GetAccountProfile()
        {
            var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (accountId == null)
            {
                return Unauthorized();
            }

            var accountProfile = await _accountService.GetAccountByIdAsync(Guid.Parse(accountId));
            return Ok(accountProfile);
        }

        #region UpdateProfile
        /// <summary>
        /// Update profile.
        /// </summary>
        /// <param name="profile">Update Profile data.</param>
        /// <returns>Profile.</returns>
        #endregion
        [Authorize]
        [HttpPut(ApiEndPointConstant.Account.AccountProfileUpdateEndpoint)]
        public async Task<IActionResult> UpdateAccountProfile([FromBody] UpdateProfileDto updateProfileDto)
        {
            var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (accountId == null)
            {
                return Unauthorized();
            }

            await _accountService.UpdateProfileAsync(Guid.Parse(accountId), updateProfileDto);
            return NoContent();
        }

        #region SearchAccounts
        /// <summary>
        /// Search accounts by name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>List of accounts that match the search criteria.</returns>
        // GET: api/Account/Search
        #endregion
        [HttpGet(ApiEndPointConstant.Account.SearchAccountEndpoint)]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchAccountsAsync(string name)
        {
            var accounts = await _accountService.SearchAccountsByNameAsync(name);
            return Ok(accounts);
        }
    }
}
