using System;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class HsrpFacilityProperties : BaseActiveModel
    {
        public HsrpFacilityProperties() { }

        public HsrpFacilityProperties(Guid facilityId, HsrpFacilityPropertiesCreateDto hfpcd) 
        {
            FacilityId = facilityId;
            DateListed = (DateOnly)(hfpcd?.DateListed);
            AdditionalOrgUnit = hfpcd?.AdditionalOrgUnit;
            Geologist = hfpcd?.Geologist;
            VRPDate = (DateOnly)(hfpcd?.VRPDate);
            BrownfieldDate = (DateOnly)(hfpcd?.BrownfieldDate);
        }
        public Guid FacilityId { get; set; }
        
        public DateOnly DateListed {  get; set; }

        public string AdditionalOrgUnit { get; set; }

        public string Geologist { get; set; }

        public DateOnly VRPDate { get; set; }

        public DateOnly BrownfieldDate { get; set; }
    }
}
