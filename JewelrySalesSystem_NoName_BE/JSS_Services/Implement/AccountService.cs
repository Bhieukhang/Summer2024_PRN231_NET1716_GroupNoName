using JSS_BusinessObjects.Models;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Services.Interface;
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

        public async Task<Account> GetAccountByIdAsync(Guid id)
        {
            try
            {
                var accountRepository = _unitOfWork.GetRepository<Account>();
                var account = await accountRepository.FirstOrDefaultAsync(a => a.Id == id);
                return account;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting the account by ID");
                throw;
            }
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
    }
}
