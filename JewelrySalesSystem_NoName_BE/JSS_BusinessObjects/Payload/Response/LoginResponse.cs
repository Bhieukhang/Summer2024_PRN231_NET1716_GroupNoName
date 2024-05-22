using JSS_BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Response
{
    public class LoginResponse
    {
        public LoginResponse(string? phone, string roleName, string token)
        {
            Phone = phone;
            RoleName = roleName;
            Token = token;
        }

        public string? Phone { get; set; }
        public string RoleName { get; set; }
        public string Token { get; set; }
    }
}
