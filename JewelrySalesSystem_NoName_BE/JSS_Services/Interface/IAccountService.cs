using JSS_BusinessObjects;
using JSS_BusinessObjects.DTO;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_Services.Interface
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<Account> GetAccountByIdAsync(Guid id);
        Task<Account> UpdateAccountAsync(Guid id, Account account);
        Task<Account> CreateAccountAsync(Account account);
        Task<int> GetTotalAccountCountAsync(); 
        Task<int> GetActiveAccountCountAsync();
        public Task<IPaginate<AccountResponse>> GetListAccountAsync(int page, int size);

        public Task<IPaginate<AccountResponse>> GetListAccountByRoleIdAsync(Guid roleId, int page, int size);
        Task UpdateProfileAsync(Guid id, UpdateProfileDto updateProfileDto);
    }
}
