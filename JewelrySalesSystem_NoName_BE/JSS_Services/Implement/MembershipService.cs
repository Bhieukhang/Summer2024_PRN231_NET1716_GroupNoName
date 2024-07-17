using JSS_BusinessObjects;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Repositories.Repo.Interface;
using JSS_Services.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace JSS_Services.Implement
{
    public class MembershipService : IMembershipService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MembershipService> _logger;

        public MembershipService(IUnitOfWork unitOfWork, ILogger<MembershipService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IPaginate<MembershipResponse>> GetListMembership(int page, int size)
        {
            IPaginate<MembershipResponse> listMembership = await _unitOfWork.MembershipRepository.GetList(
                selector: x => new MembershipResponse(x.Id, x.Name, x.Point, x.RedeemPoint, x.UserId, x.UsedMoney, x.Deflag),
                orderBy: x => x.OrderByDescending(x => x.UsedMoney),
                page: page,
                size: size);
            return listMembership;
        }

        public async Task<IPaginate<MembershipResponse>> GetListMembershipExpired(int page, int size)
        {
            IPaginate<MembershipResponse> listMembershipExpired = await _unitOfWork.MembershipRepository.GetList(
                selector: x => new MembershipResponse(x.Id, x.Name, x.Point, x.RedeemPoint, x.UserId, x.UsedMoney, x.Deflag),
                predicate: x => x.Deflag.Equals(false),
                orderBy: x => x.OrderBy(x => x.UsedMoney),
                page: page,
                size: size);
            return listMembershipExpired;
        }

        public async Task<ProfileResponse> GetProfileMembershipById(Guid id)
        {
            var member = await _unitOfWork.MembershipRepository.FirstOrDefaultAsync(x => x.UserId == id);
            var memberType = await _unitOfWork.MemberTypeRepository.FirstOrDefaultAsync(t => t.Id == member.MemberTypeId);
            var user = await _unitOfWork.AccountRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (member == null)
            {
                return null;
            }
            MembershipResponse membershipResponse = new MembershipResponse()
            {
                Id = member.Id,
                Name = member.Name,
                Point = member.Point,
                RedeemPoint = member.RedeemPoint,
                UserId = member.UserId,
                UsedMoney = member.UsedMoney,
                Deflag = member.Deflag,
                Level = memberType.Type
            };
            return new ProfileResponse(user.Phone, user.Dob, user.Address, user.ImgUrl, membershipResponse);
        }

        public async Task<MembershipResponse> GetMembershipByName(string name)
        {
            var queryMembership = await _unitOfWork.MembershipRepository.FirstOrDefaultAsync(x => x.Name.Equals(name));
            if (queryMembership == null)
            {
                return null;
            }
            return new MembershipResponse(queryMembership.Id, queryMembership.Name, queryMembership.Point,
                                          queryMembership.RedeemPoint, queryMembership.UserId, queryMembership.UsedMoney, queryMembership.Deflag);
        }

        public async Task<MembershipResponse> UpdateUserMoney(Guid userId, double userMoney)
        {
            var membership = await _unitOfWork.MembershipRepository.FirstOrDefaultAsync(x => x.UserId == userId);
            membership.UsedMoney += userMoney;

            await UpdateMemberType(membership);

            _unitOfWork.MembershipRepository.UpdateAsync(membership);

            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (isSuccessful == false) return null;
            return new MembershipResponse(membership.Id, membership.Name, membership.Point,
                                          membership.RedeemPoint, membership.UserId, membership.UsedMoney, membership.Deflag);
        }
        private async Task UpdateMemberType(Membership membership)
        {
            var memberTypes = await _unitOfWork.MemberTypeRepository.GetListAsync();

            if (membership.UsedMoney >= 400000000)
            {
                membership.MemberTypeId = memberTypes.FirstOrDefault(mt => mt.Type == "Diamond")?.Id;
            }
            else if (membership.UsedMoney >= 150000000)
            {
                membership.MemberTypeId = memberTypes.FirstOrDefault(mt => mt.Type == "Gold")?.Id;
            }
            else if (membership.UsedMoney >= 30000000)
            {
                membership.MemberTypeId = memberTypes.FirstOrDefault(mt => mt.Type == "Silver")?.Id;
            }
            else
            {
                membership.MemberTypeId = memberTypes.FirstOrDefault(mt => mt.Type == "Bronze")?.Id;
            }
        }

        public async Task<int> GetTotalMembershipCountAsync()
        {
            var total = _unitOfWork.MembershipRepository;
            return await total.CountAsync();
        }
        public async Task<int> GetActiveMembershipCountAsync()
        {
            var activeMembership = _unitOfWork.MembershipRepository;
            return await activeMembership.CountAsync(a => a.Deflag == true);
        }

        public async Task<int> GetUnavailableMembership()
        {
            var unMembership = await _unitOfWork.MembershipRepository.CountAsync(m => m.Deflag == false);
            return unMembership;
        }

        public async Task<SearchAccountResponse> CreateMembership(string phone, string name)
        {
            var account = new Account()
            {
                Id = Guid.NewGuid(),
                FullName = name,
                Phone = phone,
                Dob = null,
                Password = "000",
                Address = null,
                ImgUrl = null,
                Status = "UnActive",
                Deflag = false,
                RoleId = Guid.Parse("7C9E6679-7425-40DE-944B-E07FC1F90AE9"),
                InsDate = DateTime.Now,
                UpsDate = DateTime.Now,
            };
            _unitOfWork.AccountRepository.InsertAsync(account);
            if (account != null)
            {
                Membership member = new Membership()
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Point = 0,
                    RedeemPoint = 0,
                    UserId = account.Id,
                    UsedMoney = 0,
                    Deflag = true,
                    MemberTypeId = Guid.Parse("18095960-ACB3-4FD4-BCD3-646D9DF3E6E1")
                };
                _unitOfWork.MembershipRepository.InsertAsync(member);

                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
                if (isSuccessful == false) return null;
            }
            return new SearchAccountResponse(account.Id, account.FullName, account.Phone);
        }

        public async Task<MembershipInfo> GetInfoMembership(string phone)
        {
            var account = await _unitOfWork.AccountRepository
                            .FirstOrDefaultAsync(m => m.Phone == phone);
            var member = await _unitOfWork.MembershipRepository.FirstOrDefaultAsync(m => m.UserId == account.Id);
            var memberType = await _unitOfWork.MemberTypeRepository.FirstOrDefaultAsync(t => t.Id == member.MemberTypeId);
            MembershipInfo mem = new MembershipInfo()
            {
                Id = member.Id,
                Name = member.Name,
                Phone = account.Phone,
                Type = memberType.Type,
                UserMoney = (double)member.UsedMoney,
                PercentDiscount = (double)memberType.PercentDiscount
            };
            return mem;
        }
    }
}