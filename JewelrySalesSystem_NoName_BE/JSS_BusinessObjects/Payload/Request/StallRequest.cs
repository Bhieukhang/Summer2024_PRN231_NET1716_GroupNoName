using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Do Huu Thuan
namespace JSS_BusinessObjects.Payload.Request
{
    public class StallRequest
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public int? Number { get; set; }

        public Guid StaffId { get; set; }

        public bool? Deflag { get; set; }
    }
}
