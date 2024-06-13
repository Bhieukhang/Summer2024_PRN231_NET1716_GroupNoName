using JSS_DataAccessObjects;
using JSS_BusinessObjects.Models;
using JSS_Repositories;
using JSS_Services.Interface;
using Microsoft.EntityFrameworkCore;
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
        private readonly IConfiguration _configuration;

        public AuthService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork, ILogger<Account> logger, IConfiguration configuration) : base(unitOfWork, logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Account> GetAccountByPhone(string phone, string password)
        {
            var account = await _unitOfWork.GetRepository<Account>().FirstOrDefaultAsync(predicate: x =>
                x.Phone.Equals(phone) && x.Password.Equals(password), include: q => q.Include(x => x.Role));

            if (account == null)
            {
                throw new UnauthorizedAccessException("Invalid phone or password.");
            }
            return account;
        }

        public async Task<string> LoginAsync(string phone, string password)
        {
            var accountRepository = _unitOfWork.GetRepository<Account>();
            var account = await _unitOfWork.GetRepository<Account>().FirstOrDefaultAsync(predicate: x =>
                x.Phone.Equals(phone) && x.Password.Equals(password), include: q => q.Include(x => x.Role));

            if (account == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials or account not found.");
            }

            return GenerateJwtToken(account);
        }

        private string GenerateJwtToken(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account), "Account cannot be null");
            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                    new Claim(ClaimTypes.Name, account.FullName ?? ""),
                    new Claim(ClaimTypes.MobilePhone, account.Phone ?? ""),
                    new Claim(ClaimTypes.Role, account.Role.RoleName),
                    new Claim("ImgUrl", account.ImgUrl ?? "")
                };

            var keyString = _configuration["Jwt:Key"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(30);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expires,
                signingCredentials: cred
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}