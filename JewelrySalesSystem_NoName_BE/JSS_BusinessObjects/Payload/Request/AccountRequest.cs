using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Request
{
    public class AccountRequest
    {
        public Guid Id { get; set; }

        public string? FullName { get; set; }

        public string Phone { get; set; } = null!;

        public DateTime? Dob { get; set; }

        public string Password { get; set; } = null!;

        public string? Address { get; set; }

        public string? ImgUrl { get; set; }

        public string? Status { get; set; }

        public bool? Deflag { get; set; }

        public Guid RoleId { get; set; }

        public DateTime? InsDate { get; set; }

        public DateTime? UpsDate { get; set; }
    }

    public class AccountMembership
    {
        public string? FullName { get; set; }

        public string Phone { get; set; } = null!;
    }
}
