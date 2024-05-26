using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Response
{
    public class ProfileResponse
    {
        public ProfileResponse(string phone, DateTime? dob, string? address, string? imgUrl, MembershipResponse member) 
        {
            Phone = phone;
            Dob = dob;
            Address = address;
            ImgUrl = imgUrl;
            Membership = member;
        }
        public string Phone { get; set; }

        public DateTime? Dob { get; set; }

        public string? Address { get; set; }

        public string? ImgUrl { get; set; }
        public MembershipResponse Membership { get; set; }
    }
}
