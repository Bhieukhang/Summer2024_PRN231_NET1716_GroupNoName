using JSS_BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_Services.Interface
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string phone, string password);
        Task<Account> GetAccountByPhone(string phone, string password);
    }
}
