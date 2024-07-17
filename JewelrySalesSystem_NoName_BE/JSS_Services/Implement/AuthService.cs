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
using JSS_Repositories.Repo.Interface;

namespace JSS_Services.Implement
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AccountService> _logger;

        public AuthService(IUnitOfWork unitOfWork, ILogger<AccountService> logger, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Account> GetAccountByPhone(string phone, string password)
        {
            var account = await _unitOfWork.AccountRepository.FirstOrDefaultAsync(predicate: x =>
                x.Phone.Equals(phone), include: q => q.Include(x => x.Role));

            if (account == null)
            {
                throw new UnauthorizedAccessException("Số điện thoại chưa được đăng ký trong hệ thống.");
            }

            if (account.RoleId == Guid.Parse("7C9E6679-7425-40DE-944B-E07FC1F90AE9"))
            {
                throw new UnauthorizedAccessException("Không có quyền truy cập.");
            }

            if (account.Deflag == false)
            {
                throw new UnauthorizedAccessException("Tài khoản không hoạt động.");
            }

            if (!account.Password.Equals(password))
            {
                throw new UnauthorizedAccessException("Số điện thoại hoặc mật khẩu bị sai. Hãy thử lại!");
            }

            return account;
        }

        public async Task<string> LoginAsync(string phone, string password)
        {
            var accountRepository = _unitOfWork.AccountRepository;
            var account = await _unitOfWork.AccountRepository.FirstOrDefaultAsync(predicate: x =>
                x.Phone.Equals(phone) && x.Password.Equals(password), include: q => q.Include(x => x.Role));

            if (account == null)
            {
                throw new UnauthorizedAccessException("Không tìm thấy thông tin xác thực hoặc tài khoản không hợp lệ.");
            }

            return GenerateJwtToken(account);
        }

        private string GenerateJwtToken(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account), "Tài khoản không được để trống.");
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
