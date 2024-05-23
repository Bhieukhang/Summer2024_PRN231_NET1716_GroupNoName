using JSS_BusinessObjects.Models;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JSS_Services.Implement
{
    public class AuthService : BaseService<Account>, IAuthService
    {
        public static IConfiguration Configuration { get; }
        private readonly HashSet<string> _blacklistedTokens = new HashSet<string>();

        public AuthService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork, ILogger<Account> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<Account> GetAccountByPhone(string phone, string password)
        {
            var account = await _unitOfWork.GetRepository<Account>().SingleOrDefaultAsync(predicate: x =>
                x.Phone.Equals(phone) && x.Password.Equals(password));
            return account;
        }

        public async Task<string> LoginAsync(string phone, string password)
        {
            var accountRepository = _unitOfWork.GetRepository<Account>();
            var account = await _unitOfWork.GetRepository<Account>().SingleOrDefaultAsync(predicate: x =>
                x.Phone.Equals(phone) && x.Password.Equals(password));

            if (account == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials or account not found.");
            }

            return GenerateJwtToken(account);
        }

        private string GenerateJwtToken(Account account)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                    new Claim(ClaimTypes.Name, account.FullName ?? ""),
                    new Claim(ClaimTypes.MobilePhone, account.Phone ?? ""),
                    new Claim(ClaimTypes.Role, account.RoleId.ToString())
                };

            var keyString = "Tokenspodpsjohinidbfjhbvkfdjhagvakfd&*¨T&(SFGD&(¨SFD(&VY&(6dfsutf7f6dod8g-f&TG08t¨&*ts&¨*dt&sfg(öd&astdecatechlabs";
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(keyString));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var expires = DateTime.Now.AddDays(30);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expires,
                signingCredentials: cred
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}