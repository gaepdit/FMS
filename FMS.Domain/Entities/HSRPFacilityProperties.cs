using System;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class HsrpFacilityProperties : BaseActiveModel
    {
        public HsrpFacilityProperties() { }

        public HsrpFacilityProperties(Guid facilityId, HsrpFacilityPropertiesCreateDto Hfpcd) 
        {
            FacilityId = facilityId;
            DateListed = (DateOnly)(Hfpcd?.DateListed);
            AdditionalOrgUnit = Hfpcd?.AdditionalOrgUnit;
            Geologist = Hfpcd?.Geologist;
            VRPDate = (DateOnly)(Hfpcd?.VRPDate);
            BrownfieldDate = (DateOnly)(Hfpcd?.BrownfieldDate);
        }
        public Guid FacilityId { get; set; }
        
        public DateOnly DateListed {  get; set; }

        public string AdditionalOrgUnit { get; set; }

        public string Geologist { get; set; }

        public DateOnly VRPDate { get; set; }

        public DateOnly BrownfieldDate { get; set; }
    }
}
