using Firebase.Storage;
using Azure.Core;
using FirebaseAdmin.Messaging;
using JSS_BusinessObjects;
using JSS_BusinessObjects.DTO;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Response;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Services.Interface;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JSS_Services.Implement
{
    public class AccountService : BaseService<AccountService>, IAccountService
    {
        private readonly string _bucket = "jssimage-253a4.appspot.com";
        private readonly List<Guid> excludedRoleIds = new List<Guid>
        {
            Guid.Parse("7C9E6679-7425-40DE-944B-E07FC1F90AE9"),
            Guid.Parse("0F8FAD5B-D9CB-469F-A165-70867728950E")
        };

        public AccountService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork, ILogger<AccountService> logger) : base(unitOfWork, logger)
        {
        }
        public async Task<IPaginate<AccountResponse>> GetListAccountAsync(int page, int size)
        {
            IPaginate<AccountResponse> listAccount = await _unitOfWork.GetRepository<Account>().GetList(
                selector: x => new AccountResponse(x.Id, x.FullName, x.Phone, x.Dob, x.Password, x.Address, x.ImgUrl, x.Status, x.Deflag, x.RoleId, x.InsDate, x.UpsDate),
                predicate: x => !excludedRoleIds.Contains(x.RoleId),
                orderBy: x => x.OrderByDescending(x => x.Id),
                page: page,
                size: size); ;
            return listAccount;
        }
        public async Task<IPaginate<AccountResponse>> GetListAccountByRoleIdAsync(Guid roleId, int page, int size)
        {
            IPaginate<AccountResponse> listAccount = await _unitOfWork.GetRepository<Account>().GetList(
                selector: x => new AccountResponse(x.Id, x.FullName, x.Phone, x.Dob, x.Password, x.Address, x.ImgUrl, x.Status, x.Deflag, x.RoleId, x.InsDate, x.UpsDate),
                predicate: x => x.RoleId == roleId && !excludedRoleIds.Contains(x.RoleId),
                orderBy: x => x.OrderByDescending(x => x.Id),
                page: page,
                size: size);
            return listAccount;
        }

        public async Task<IPaginate<AccountResponse>> GetFilteredAccountsAsync(string? searchTerm, Guid? roleId, bool? deflag, int page, int size)
        {
            var accountRepository = _unitOfWork.GetRepository<Account>();

            var predicate = PredicateBuilder.New<Account>(true);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                predicate = predicate.And(x => x.FullName.Contains(searchTerm));
            }

            if (roleId.HasValue)
            {
                predicate = predicate.And(x => x.RoleId == roleId.Value && !excludedRoleIds.Contains(x.RoleId));
            }

            if (deflag.HasValue)
            {
                predicate = predicate.And(x => x.Deflag == deflag.Value);
            }

            var listAccount = await accountRepository.GetList(
                selector: x => new AccountResponse(x.Id, x.FullName, x.Phone, x.Dob, x.Password, x.Address, x.ImgUrl, x.Status, x.Deflag, x.RoleId, x.InsDate, x.UpsDate),
                predicate: predicate,
                orderBy: x => x.OrderByDescending(x => x.Id),
                page: page,
                size: size);

            return listAccount;
        }

        public async Task<int> GetTotalAccountCountAsync()
        {
            var accountRepository = _unitOfWork.GetRepository<Account>();
            return await accountRepository.CountAsync(x => !excludedRoleIds.Contains(x.RoleId));
        }
        public async Task<int> GetActiveAccountCountAsync()
        {
            var accountRepository = _unitOfWork.GetRepository<Account>();
            return await accountRepository.CountAsync(a => a.Status == "Active" && !excludedRoleIds.Contains(a.RoleId));
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

        public async Task<Account> CreateAccountAsync(Account account, Stream imageStream, string imageName)
        {
            //Guid DefaultRoleId = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7");
            try
            {
                var imageUrl = await UploadImageToFirebase(imageStream, imageName);
                if (string.IsNullOrEmpty(imageUrl))
                {
                    throw new Exception("Image upload failed, URL is empty.");
                }

                var accountRepository = _unitOfWork.GetRepository<Account>();
                account.Id = Guid.NewGuid();
                account.ImgUrl = imageUrl;
                account.InsDate = DateTime.UtcNow;
                account.Status = "Active";
                account.UpsDate = DateTime.UtcNow;
                //account.RoleId = DefaultRoleId;

                account.Deflag = true;
                await accountRepository.InsertAsync(account);
                //await _unitOfWork.CommitAsync();
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
                if (!isSuccessful)
                {
                    throw new Exception("Commit failed, no rows affected.");
                }

                return account;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating account");
                throw;
            }
        }

        public async Task<Account> UpdateAccountAsync(Guid id, Account account, Stream imageStream, string imageName)
        {
            try
            {
                var accountRepository = _unitOfWork.GetRepository<Account>();
                var _account = await accountRepository.FirstOrDefaultAsync(a => a.Id == id, include: q => q.Include(x => x.Role));

                if (_account == null)
                {
                    return null;
                }

                _account.FullName = account.FullName != default ? account.FullName : _account.FullName;
                _account.Phone = account.Phone != default ? account.Phone : _account.Phone;
                _account.Dob = account.Dob != default ? account.Dob : _account.Dob;
                _account.Password = account.Password != default ? account.Password : _account.Password;
                _account.Address = account.Address != default ? account.Address : _account.Address;
                _account.Deflag = account.Deflag != default ? account.Deflag : _account.Deflag;
                _account.RoleId = account.RoleId != Guid.Empty ? account.RoleId : _account.RoleId;
                _account.Status = account.Status != default ? account.Status : _account.Status;

                if (imageStream != null)
                {
                    var imageUrl = await UploadImageToFirebase(imageStream, imageName);
                    _account.ImgUrl = imageUrl;
                }
                else if (imageStream == null)
                {
                    account.ImgUrl = _account.ImgUrl;
                }
                _account.UpsDate = DateTime.UtcNow;

                accountRepository.UpdateAsync(_account);
                //await _unitOfWork.CommitAsync();
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
                if (!isSuccessful) return null;

                return _account;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating account");
                throw;
            }
        }

        public async Task<Account> UpdateDeflagAccountAsync(Guid id)
        {
            try
            {
                var accountRepository = _unitOfWork.GetRepository<Account>();
                var account = await accountRepository.FirstOrDefaultAsync(a => a.Id == id);

                if (account == null)
                {
                    return null;
                }

                account.Deflag = false;
                account.Status = "Inactive";
                account.UpsDate = DateTime.UtcNow;

                accountRepository.UpdateAsync(account);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
                if (!isSuccessful)
                {
                    return null;
                }

                return account;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating deflag of account");
                throw;
            }
        }

        public async Task UpdateProfileAsync(Guid id, UpdateProfileDto updateProfileDto, Stream imageStream, string imageName)
        {
            try
            {
                var accountProfile = _unitOfWork.GetRepository<Account>();
                var account = await accountProfile.FirstOrDefaultAsync(a => a.Id == id, include: q => q.Include(x => x.Role));

                if (account == null)
                {
                    throw new KeyNotFoundException("Account not found");
                }

                account.FullName = !string.IsNullOrEmpty(updateProfileDto.FullName) ? updateProfileDto.FullName : account.FullName;
                account.Dob = updateProfileDto.Dob != default ? updateProfileDto.Dob : account.Dob;
                account.Address = !string.IsNullOrEmpty(updateProfileDto.Address) ? updateProfileDto.Address : account.Address;
                account.Password = !string.IsNullOrEmpty(updateProfileDto.Password) ? updateProfileDto.Password : account.Password;

                if (imageStream != null)
                {
                    var imageUrl = await UploadImageToFirebase(imageStream, imageName);
                    account.ImgUrl = imageUrl;
                }
                //else if (imageStream == null)
                //{
                //    account.ImgUrl = account.ImgUrl;
                //}
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

        public async Task<Account> SearchAccountsByNameAsync(string name)
        {
            try
            {
                //var account = await _unitOfWork.GetRepository<Account>().FirstOrDefaultAsync(p => p.FullName.Contains(name), include: s => s.Include(p => p.Role));
                //return account;
                if (string.IsNullOrEmpty(name))
                {
                    return await _unitOfWork.GetRepository<Account>().FirstOrDefaultAsync(include: s => s.Include(p => p.Role));
                }
                else
                {
                    return await _unitOfWork.GetRepository<Account>().FirstOrDefaultAsync(p => p.FullName.Contains(name), include: s => s.Include(p => p.Role));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the account by name.");
                throw;
            }
        }

        private async Task<string> UploadImageToFirebase(Stream imageStream, string imageName)
        {
            var storage = new FirebaseStorage(_bucket);
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageName);

            var uploadTask = storage
                .Child("uploads")
                .Child(uniqueFileName)
                .PutAsync(imageStream);

            return await uploadTask;
        }

        //public async Task<IPaginate<AccountResponse>> GetListAccountWithDeflagFalseAsync(int page, int size)
        //{
        //    Guid excludedRoleId = Guid.Parse("7C9E6679-7425-40DE-944B-E07FC1F90AE9");

        //    IPaginate<AccountResponse> listAccount = await _unitOfWork.GetRepository<Account>().GetList(
        //        selector: x => new AccountResponse(x.Id, x.FullName, x.Phone, x.Dob, x.Password, x.Address, x.ImgUrl, x.Status, x.Deflag, x.RoleId, x.InsDate, x.UpsDate),
        //        predicate: x => x.Deflag == false && x.RoleId != excludedRoleId,
        //        orderBy: x => x.OrderByDescending(x => x.Id),
        //        page: page,
        //        size: size);
        //    return listAccount;
        //}

        public async Task<SearchAccountResponse> SearchMembership(string phone)
        {
            var mem = await _unitOfWork.GetRepository<Account>().FirstOrDefaultAsync(a => a.Phone == phone,
                                                                include: a => a.Include(a => a.Role));

            if (mem == null)
            {
                throw new AppConstant.MessageError((int)AppConstant.ErrCode.Membership_Not_Found, AppConstant.ErrMessage.MembershipNotFound);
            }

            if (mem != null && mem.Role.RoleName != AppConstant.Role.Customer)
            {
                throw new AppConstant.MessageError((int)AppConstant.ErrCode.Success, AppConstant.ErrMessage.MembershipNotRegister);
            }

            return new SearchAccountResponse(mem.Id, mem.FullName, mem.Phone);
        }

    }
}
