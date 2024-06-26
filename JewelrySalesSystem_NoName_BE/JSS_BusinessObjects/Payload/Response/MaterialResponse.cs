using JSS_BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Response
{
    public class MaterialResponse
    {
        public MaterialResponse(Guid id, string? materialName, DateTime? insDate)
        {
            Id = id;
            MaterialName = materialName;
            InsDate = insDate;
        }
        public Guid Id { get; set; }

        public string? MaterialName { get; set; }

        public DateTime? InsDate { get; set; }
    }
}
