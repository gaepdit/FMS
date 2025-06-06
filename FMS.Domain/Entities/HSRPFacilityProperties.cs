using System;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class HsrpFacilityProperties : BaseActiveModel
    {
        public HsrpFacilityProperties() { }

        public HsrpFacilityProperties(Guid facilityId, HsrpFacilityPropertiesCreateDto hsrpFacilityProperties) 
        {
            FacilityId = facilityId;
            DateListed = hsrpFacilityProperties.DateListed;
            AdditionalOrgUnit = hsrpFacilityProperties.AdditionalOrgUnit;
            Geologist = hsrpFacilityProperties.Geologist;
            VRPDate = hsrpFacilityProperties.VRPDate;
            BrownfieldDate = hsrpFacilityProperties.BrownfieldDate;
        }
        public Guid FacilityId { get; set; }
        
        public DateOnly DateListed {  get; set; }

        public string AdditionalOrgUnit { get; set; }

        public string Geologist { get; set; }

        public DateOnly VRPDate { get; set; }

        public DateOnly BrownfieldDate { get; set; }
    }
}
