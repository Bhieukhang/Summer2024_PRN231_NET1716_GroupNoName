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
    }
}
