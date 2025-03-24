using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Dto
{
    public class HsrpFacilityPropertiesCreateDto
    {
        public Guid FacilityId { get; set; } = Guid.Empty;

        public DateTime DateListed { get; set; }

        public string AdditionalOrgUnit { get; set; }

        public string Geologist { get; set; }

        public DateTime VRPDate { get; set; }

        public DateTime BrownfieldDate { get; set; }
    }
}
