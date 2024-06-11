using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Response
{
    public class AccountResponse
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

        public AccountResponse(Guid id, string? fullname, string phone, DateTime? dob, string password, string? address, string? imgUrl,
            string? status, bool? deflag, Guid roleId, DateTime? insDate, DateTime? upsDate)
        {
            Id = id; FullName = fullname; Phone = phone; Dob = dob; Password = password; Address = address; ImgUrl = imgUrl; Status = status; Deflag = deflag; RoleId = roleId; InsDate = insDate; UpsDate = upsDate;
        }
    }

    public class SearchAccountResponse
    {
        public SearchAccountResponse(Guid id, string? fullName, string phone)
        {
            Id = id;
            FullName = fullName;
            Phone = phone;
        }
        public Guid Id { get; set; }

        public string? FullName { get; set; }

        public string Phone { get; set; } = null!;
    }

    public class InforCustomer
    {
        public InforCustomer(Guid id, string? fullName, string phone, string? address, string imgUrl)
        {
            Id = id; FullName = fullName;
            Phone = phone; Address = address;
            ImgUrl = imgUrl;
        }
        public Guid Id { get; set; }

        public string? FullName { get; set; }

        public string Phone { get; set; } = null!;
        public string? Address { get; set; }

        public string? ImgUrl { get; set; }
    }
}
