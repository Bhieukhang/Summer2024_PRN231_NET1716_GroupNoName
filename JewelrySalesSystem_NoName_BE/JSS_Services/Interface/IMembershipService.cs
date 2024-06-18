using JSS_BusinessObjects;
using JSS_BusinessObjects.Payload.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_Services.Interface
{
    public interface IMembershipService
    {
        public Task<IPaginate<MembershipResponse>> GetListMembership(int page, int size);
        public Task<IPaginate<MembershipResponse>> GetListMembershipExpired(int page, int size);
        public Task<MembershipResponse> GetMembershipByName(string name);
        public Task<ProfileResponse> GetProfileMembershipById(Guid id);

        public Task<int> GetTotalMembershipCountAsync();
        public Task<int> GetActiveMembershipCountAsync();
        public Task<int> GetUnavailableMembership();
        public Task<SearchAccountResponse> CreateMembership(string phone, string name);
    }
}
