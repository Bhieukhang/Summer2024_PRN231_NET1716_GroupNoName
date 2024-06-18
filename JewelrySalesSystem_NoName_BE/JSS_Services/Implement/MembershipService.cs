using JSS_BusinessObjects;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Response;
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
    public class MembershipService : BaseService<MembershipService>, IMembershipService
    {
        public MembershipService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork, ILogger<MembershipService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<IPaginate<MembershipResponse>> GetListMembership(int page, int size)
        {
            IPaginate<MembershipResponse> listMembership = await _unitOfWork.GetRepository<Membership>().GetList(
                selector: x => new MembershipResponse(x.Id, x.Name, x.Level, x.Point, x.RedeemPoint, x.UserId, x.UsedMoney, x.Deflag),
                orderBy: x => x.OrderByDescending(x => x.UsedMoney),
                page: page,
                size: size);
            return listMembership;
        }

        public async Task<IPaginate<MembershipResponse>> GetListMembershipExpired(int page, int size)
        {
            IPaginate<MembershipResponse> listMembershipExpired = await _unitOfWork.GetRepository<Membership>().GetList(
                selector: x => new MembershipResponse(x.Id, x.Name, x.Level, x.Point, x.RedeemPoint, x.UserId, x.UsedMoney, x.Deflag),
                predicate: x => x.Deflag.Equals(false),
                orderBy: x => x.OrderBy(x => x.UsedMoney),
                page: page,
                size: size);
            return listMembershipExpired;
        }

        public async Task<ProfileResponse> GetProfileMembershipById(Guid id)
        {
            var member = await _unitOfWork.GetRepository<Membership>().FirstOrDefaultAsync(x => x.UserId == id);
            var user = await _unitOfWork.GetRepository<Account>().FirstOrDefaultAsync(x => x.Id == id);
            if (member == null)
            {
                return null;
            }
            MembershipResponse membershipResponse = new MembershipResponse()
            {
                Id = member.Id,
                Name = member.Name,
                Level = member.Level,
                Point = member.Point,
                RedeemPoint = member.RedeemPoint,
                UserId = member.UserId,
                UsedMoney = member.UsedMoney,
                Deflag = member.Deflag
            };
            return new ProfileResponse(user.Phone, user.Dob, user.Address, user.ImgUrl, membershipResponse);
        }

        public async Task<MembershipResponse> GetMembershipByName(string name)
        {
            var queryMembership = await _unitOfWork.GetRepository<Membership>().FirstOrDefaultAsync(x => x.Name.Equals(name));
            if (queryMembership == null)
            {
                return null;
            }
            return new MembershipResponse(queryMembership.Id, queryMembership.Name, queryMembership.Level, queryMembership.Point,
                                          queryMembership.RedeemPoint, queryMembership.UserId, queryMembership.UsedMoney, queryMembership.Deflag);
        }


        public async Task<int> GetTotalMembershipCountAsync()
        {
            var total = _unitOfWork.GetRepository<Membership>();
            return await total.CountAsync();
        }
        public async Task<int> GetActiveMembershipCountAsync()
        {
            var activeMembership = _unitOfWork.GetRepository<Membership>();
            return await activeMembership.CountAsync(a => a.Deflag == true);
        }

        public async Task<int> GetUnavailableMembership()
        {
            var unMembership = await _unitOfWork.GetRepository<Membership>().CountAsync(m => m.Deflag == false);
            return unMembership;
        }

        public async Task<SearchAccountResponse> CreateMembership(string phone, string name)
        {
            Guid idAccount = Guid.NewGuid();
            var account = new Account()
            {
                Id = idAccount,
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
            _unitOfWork.GetRepository<Account>().InsertAsync(account);
            if (account != null)
            {
                Membership member = new Membership()
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Level = 1,
                    Point = 0,
                    RedeemPoint = 0,
                    UserId = idAccount,
                    UsedMoney = 0,
                    Deflag = true,
                };
                _unitOfWork.GetRepository<Membership>().InsertAsync(member);

                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
                if (isSuccessful == false) return null;
            }
            return new SearchAccountResponse(idAccount, account.FullName, account.Phone);
        }
    }
}