using JSS_BusinessObjects;
using JSS_BusinessObjects.DTO;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Response;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_Services.Implement
{
    public class AccountService : BaseService<AccountService>, IAccountService
    {
        public AccountService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork, ILogger<AccountService> logger) : base(unitOfWork, logger)
        {
        }
        public async Task<IPaginate<AccountResponse>> GetListAccountAsync(int page, int size)
        {
            IPaginate<AccountResponse> listAccount = await _unitOfWork.GetRepository<Account>().GetList(
                selector: x => new AccountResponse(x.Id, x.FullName, x.Phone, x.Dob, x.Password, x.Address, x.ImgUrl, x.Status, x.Deflag, x.RoleId, x.InsDate, x.UpsDate),
                orderBy: x => x.OrderByDescending(x => x.Id),
                page: page,
                size: size); ;
            return listAccount;
        }
        public async Task<IPaginate<AccountResponse>> GetListAccountByRoleIdAsync(Guid roleId, int page, int size)
        {
            IPaginate<AccountResponse> listAccount = await _unitOfWork.GetRepository<Account>().GetList(
                selector: x => new AccountResponse(x.Id, x.FullName, x.Phone, x.Dob, x.Password, x.Address, x.ImgUrl, x.Status, x.Deflag, x.RoleId, x.InsDate, x.UpsDate),
                predicate: x => x.RoleId == roleId,
                orderBy: x => x.OrderByDescending(x => x.Id),
                page: page,
                size: size);
            return listAccount;
        }

        public async Task<int> GetTotalAccountCountAsync()
        {
            var accountRepository = _unitOfWork.GetRepository<Account>();
            return await accountRepository.CountAsync();
        }
        public async Task<int> GetActiveAccountCountAsync()
        {
            var accountRepository = _unitOfWork.GetRepository<Account>();
            return await accountRepository.CountAsync(a => a.Status == "Active");
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            try
            {
                var accountRepository = _unitOfWork.GetRepository<Account>();
                var accounts = await accountRepository.GetListAsync();
                return accounts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all accounts");
                throw;
            }
        }

        public async Task<Account> GetAccountByIdAsync(Guid id)
        {
            try
            {
                var accountRepository = _unitOfWork.GetRepository<Account>();
                var account = await accountRepository.FirstOrDefaultAsync(a => a.Id == id, include: q => q.Include(x => x.Role));
                if (account == null)
                {
                    throw new KeyNotFoundException("Account not found");
                }
                return account;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting the account by ID");
                throw;
            }
        }
       
        public async Task<Account> CreateAccountAsync(Account account)
        {
            Guid DefaultRoleId = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7");
            try
            {
                var accountRepository = _unitOfWork.GetRepository<Account>();
                account.Id = Guid.NewGuid();
                account.InsDate = DateTime.UtcNow;
                account.RoleId = DefaultRoleId;
                account.UpsDate = DateTime.UtcNow;
                await accountRepository.InsertAsync(account);
                await _unitOfWork.CommitAsync();

                return account;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating account");
                throw;
            }
        }

        public async Task<Account> UpdateAccountAsync(Guid id, Account account)
        {
            try
            {
                var accountRepository = _unitOfWork.GetRepository<Account>();
                var _account = await accountRepository.FirstOrDefaultAsync(a => a.Id == id, include: q => q.Include(x => x.Role));

                if (_account == null)
                {
                    return null;
                }
                _account.FullName = account.FullName;
                _account.Phone = account.Phone;
                _account.Dob = account.Dob;
                _account.Password = account.Password;
                _account.Address = account.Address;
                _account.ImgUrl = account.ImgUrl;
                _account.Status = account.Status;
                _account.RoleId = account.RoleId;
                _account.UpsDate = DateTime.UtcNow;
                accountRepository.UpdateAsync(_account);
                await _unitOfWork.CommitAsync();

                return _account;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating account");
                throw;
            }
        }

        public async Task UpdateProfileAsync(Guid id, UpdateProfileDto updateProfileDto)
        {
            try
            {
                var accountProfile = _unitOfWork.GetRepository<Account>();
                var account = await accountProfile.FirstOrDefaultAsync(a => a.Id == id, include: q => q.Include(x => x.Role));

                if (account == null)
                {
                    throw new KeyNotFoundException("Account not found");
                }

                account.FullName = updateProfileDto.FullName;
                account.Dob = updateProfileDto.Dob;
                account.Address = updateProfileDto.Address;
                account.ImgUrl = updateProfileDto.ImgUrl;
                account.UpsDate = DateTime.UtcNow;

                accountProfile.UpdateAsync(account);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating account profile");
                throw;
            }
        }
    }
}
