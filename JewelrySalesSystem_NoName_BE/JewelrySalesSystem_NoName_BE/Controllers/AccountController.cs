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
using JSS_BusinessObjects.Payload.Request;
using System.Collections.Generic;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  //  [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;

        public AccountController(IAccountService accountService, IRoleService roleService)
        {
            _accountService = accountService;
            _roleService = roleService;
        }
        #region GetAccount
        /// <summary>
        /// Get all accounts.
        /// </summary>
        /// <returns>List of accounts.</returns>
        // GET: api/Account
        #endregion
        [Authorize(Roles = "Manager, Admin")]
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
        [Authorize(Roles = "Manager, Admin")]
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
        [Authorize(Roles = "Manager, Admin")]
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
        [Authorize(Roles = "Manager, Admin")]
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
     //   [Authorize]
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
        [Authorize(Roles = "Manager, Admin")]
        [HttpPut(ApiEndPointConstant.Account.AccountByIdEndpoint)]
        public async Task<ActionResult<Account>> UpdateAccountAsync(Guid id, [FromBody] AccountRequest accountRequest)
        {
            Stream stream = null;
            if (!string.IsNullOrEmpty(accountRequest.ImgUrl))
            {
                stream = new MemoryStream(Convert.FromBase64String(accountRequest.ImgUrl));
            }

            var existingAccount = await _accountService.GetAccountByIdAsync(id);
            if (existingAccount == null)
            {
                return NotFound("Không tìm thấy tài khoản.");
            }

            if (accountRequest.RoleId != Guid.Empty && accountRequest.RoleId != accountRequest.RoleId)
            {
                var role = await _roleService.GetRoleByIdAsync(accountRequest.RoleId);
                if (role == null)
                {
                    accountRequest.RoleId = existingAccount.RoleId;
                }
            }

            var account = new Account
            {
                FullName = accountRequest.FullName,
                Phone = accountRequest.Phone,
                Dob = accountRequest.Dob,
                Password = accountRequest.Password,
                Address = accountRequest.Address,
                ImgUrl = accountRequest.ImgUrl,
                Deflag = accountRequest.Deflag,
                RoleId = accountRequest.RoleId,
                InsDate = accountRequest.InsDate,
                Status = accountRequest.Status,
            };

            var updatedAccount = await _accountService.UpdateAccountAsync(id, account, stream, "uploadedFileName");
            if (updatedAccount == null)
            {
                return StatusCode(500, "Đã có lỗi xảy ra khi cập nhật tài khoản.");
            }

            return Ok(new AccountResponse(
                updatedAccount.Id,
                updatedAccount.FullName,
                updatedAccount.Phone,
                updatedAccount.Dob,
                updatedAccount.Password,
                updatedAccount.Address,
                updatedAccount.ImgUrl,
                updatedAccount.Status,
                updatedAccount.Deflag,
                updatedAccount.RoleId,
                updatedAccount.InsDate,
                updatedAccount.UpsDate
            ));
        }

        #region CreateAccount
        /// <summary>
        /// Create a new account.
        /// </summary>
        /// <param name="account">The account object with the data to create.</param>
        /// <returns>The created account object.</returns>
        // POST: api/Account
        #endregion
        [Authorize(Roles = "Manager, Admin")]
        [HttpPost(ApiEndPointConstant.Account.AccountEndpoint)]
        public async Task<ActionResult> CreateAccountAsync([FromBody] AccountRequest accountRequest)
        {
            if (string.IsNullOrEmpty(accountRequest.ImgUrl))
                return BadRequest("Không có hình ảnh nào được tải lên.");

            var stream = new MemoryStream(Convert.FromBase64String(accountRequest.ImgUrl));
            var role = await _roleService.GetRoleByIdAsync(accountRequest.RoleId);
            if (role == null)
            {
                return NotFound("Không tìm thấy chức vụ.");
            }

            // Kiểm tra tuổi
            var today = DateTime.UtcNow;
            var age = today.Year - accountRequest.Dob.Value.Year;
            if (accountRequest.Dob > today.AddYears(-age)) age--;
            if (age < 18)
            {
                return BadRequest("Người dùng phải đủ 18 tuổi trở lên.");
            }

            var account = new Account
            {
                FullName = accountRequest.FullName,
                Phone = accountRequest.Phone,
                Dob = accountRequest.Dob,
                Password = accountRequest.Password,
                Address = accountRequest.Address,
                ImgUrl = accountRequest.ImgUrl,
                Deflag = accountRequest.Deflag,
                RoleId = accountRequest.RoleId,
                InsDate = accountRequest.InsDate
            };

            try
            {
                var createdAccount = await _accountService.CreateAccountAsync(account, stream, "uploadedFileName");
                if (createdAccount == null)
                {
                    return StatusCode(500, "Có lỗi xảy ra trong khi tạo tài khoản.");
                }

                return Ok(new AccountResponse(
                    createdAccount.Id,
                    createdAccount.FullName,
                    createdAccount.Phone,
                    createdAccount.Dob,
                    createdAccount.Password,
                    createdAccount.Address,
                    createdAccount.ImgUrl,
                    createdAccount.Status,
                    createdAccount.Deflag,
                    createdAccount.RoleId,
                    createdAccount.InsDate,
                    createdAccount.UpsDate
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the account.");
            }
        }

        #region CheckPhone
        /// <summary>
        /// Check phone.
        /// </summary>
        /// <param name="pohone">The phone existed in the data.</param>
        /// <returns>CheckPhone.</returns>
        #endregion
        [Authorize]
        [HttpGet(ApiEndPointConstant.Account.CheckPhoneEndpoint)]
        public async Task<ActionResult> CheckPhoneAsync(string phone)
        {
            var phoneExists = await _accountService.IsPhoneExistsAsync(phone);
            if (phoneExists)
            {
                return BadRequest("Số điện thoại đã tồn tại. Vui lòng sử dụng số điện thoại khác.");
            }
            return Ok();
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

            Stream stream = null;
            if (!string.IsNullOrEmpty(updateProfileDto.ImgUrl))
            {
                stream = new MemoryStream(Convert.FromBase64String(updateProfileDto.ImgUrl));
            }

            try
            {
                await _accountService.UpdateProfileAsync(Guid.Parse(accountId), updateProfileDto, stream, "uploadedFileName");
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Account not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the account profile.");
            }
        }

        #region SearchAccounts
        /// <summary>
        /// Search accounts by name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>List of accounts that match the search criteria.</returns>
        // GET: api/Account/Search
        #endregion
        [Authorize(Roles = "Manager, Admin")]
        [HttpGet(ApiEndPointConstant.Account.SearchAccountEndpoint)]
        //[ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<Account>> SearchAccountsByNameAsync(string name)
        {
            var account = await _accountService.SearchAccountsByNameAsync(name);
            if (account == null)
            {
                return NotFound();
            }

            var searchEmployee = new Account
            {
                Id = account.Id,
                FullName = account.FullName,
                Phone = account.Phone,
                Dob = account.Dob,
                Address = account.Address,
                ImgUrl = account.ImgUrl,
                Deflag = account.Deflag,
                RoleId = account.RoleId,
                InsDate = account.InsDate,
                Role = account.Role
            };
            return Ok(searchEmployee);
        }

        #region SearchMemberByPhone
        /// <summary>
        /// Search member by phne.
        /// </summary>
        /// <param name="phone">The phone to search for membership.</param>
        /// <returns>Account of member.</returns>
        // GET: api/Account/Search
        #endregion
        [HttpGet(ApiEndPointConstant.Account.SearchMemberEndpoint)]
        [ProducesResponseType(typeof(SearchAccountResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchMemberByPhone(string phone)
        {
            var member = await _accountService.SearchMembership(phone);
            var result = JsonConvert.SerializeObject(member, Formatting.Indented);
            return Ok(result);
        }

        //#region GetAccountsWithDeflagFalse
        ///// <summary>
        ///// Get all accounts with deflag set to false.
        ///// </summary>
        ///// <returns>List of accounts with deflag set to false.</returns>
        //// GET: api/Account/DeflagFalse
        //#endregion
        //[Authorize(Roles = "Manager, Admin")]
        //[HttpGet(ApiEndPointConstant.Account.AccountsWithDeflagFalseEndpoint)]
        //[ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        //public async Task<IActionResult> GetListAccountWithDeflagFalseAsync(int page, int size)
        //{
        //    var list = await _accountService.GetListAccountWithDeflagFalseAsync(page, size);
        //    var accounts = JsonConvert.SerializeObject(list, Formatting.Indented);
        //    return Ok(accounts);
        //}

        #region GetFilteredAccounts
        /// <summary>
        /// Get filtered accounts by search term, role and/or deflag.
        /// </summary>
        /// <param name="searchTerm">Search term to filter by.</param>
        /// <param name="roleId">Role ID to filter by.</param>
        /// <param name="deflag">Deflag status to filter by.</param>
        /// <param name="page">Page number.</param>
        /// <param name="size">Page size.</param>
        /// <returns>List of filtered accounts.</returns>
        // GET: api/Account/Filtered
        #endregion
        [Authorize(Roles = "Manager, Admin")]
        [HttpGet(ApiEndPointConstant.Account.FilteredAccountsEndpoint)]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFilteredAccountsAsync(string? searchTerm, Guid? roleId, bool? deflag, int page, int size)
        {
            var list = await _accountService.GetFilteredAccountsAsync(searchTerm, roleId, deflag, page, size);
            var accounts = JsonConvert.SerializeObject(list, Formatting.Indented);
            return Ok(accounts);
        }

        #region UpdateDeflagAccount
        /// <summary>
        /// Update deflag of an account by ID.
        /// </summary>
        /// <param name="id">The ID of the account to update deflag.</param>
        /// <returns>The updated account object with deflag set to false and status set to inactive.</returns>
        // PUT: api/Account/UpdateDeflag/{id}
        #endregion
        [Authorize(Roles = "Manager, Admin")]
        [HttpPut(ApiEndPointConstant.Account.UpdateDeflagEndpoint)]
        public async Task<ActionResult<AccountResponse>> UpdateDeflagAccountAsync(Guid id)
        {
            var updatedAccount = await _accountService.UpdateDeflagAccountAsync(id);
            if (updatedAccount == null)
            {
                return NotFound("Account not found.");
            }

            return Ok(new AccountResponse(
                updatedAccount.Id,
                updatedAccount.FullName,
                updatedAccount.Phone,
                updatedAccount.Dob,
                updatedAccount.Password,
                updatedAccount.Address,
                updatedAccount.ImgUrl,
                updatedAccount.Status,
                updatedAccount.Deflag,
                updatedAccount.RoleId,
                updatedAccount.InsDate,
                updatedAccount.UpsDate
            ));
        }
    }
}