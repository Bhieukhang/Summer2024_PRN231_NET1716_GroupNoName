using JSS_BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


// Do Huu Thuan
namespace JSS_BusinessObjects.Payload.Response
{
    public class StallResponse
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public int? Number { get; set; }

        public Guid StaffId { get; set; }

        public bool? Deflag { get; set; }
        public StallResponse(Guid id, string? name, int? number, Guid staffId, bool? deflag)
        {
            Id = id; Name=name; Number = number; StaffId = staffId; Deflag = deflag;
        }
    }
}
