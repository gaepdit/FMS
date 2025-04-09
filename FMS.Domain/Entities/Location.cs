using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class Location : BaseActiveModel
    {
        public Location() { }

        public Location(LocationEditDto location)
        {
            FacilityId = location.FacilityId;
            Score = location.Score;
        }

        public Guid FacilityId { get; set; }
        public string Score { get; set; }
    }
}
