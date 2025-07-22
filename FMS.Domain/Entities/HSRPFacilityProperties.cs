using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class HsrpFacilityProperties : BaseActiveModel
    {
        public HsrpFacilityProperties() { }

        public HsrpFacilityProperties(Guid id, HsrpFacilityPropertiesCreateDto hsrpFacilityProperties) 
        {
            Id = id;
            FacilityId = hsrpFacilityProperties.FacilityId;
            DateListed = hsrpFacilityProperties.DateListed;
            AdditionalOrgUnit = hsrpFacilityProperties.AdditionalOrgUnit;
            Geologist = hsrpFacilityProperties.Geologist;
            VRPDate = hsrpFacilityProperties.VRPDate;
            BrownfieldDate = hsrpFacilityProperties.BrownfieldDate;
            DateDeListed = hsrpFacilityProperties.DateDeListed;
        }
        public Guid FacilityId { get; set; }

        [Display(Name = "Date Listed")]
        public DateOnly DateListed {  get; set; }

        [Display(Name = "Additional Org Unit")]
        public string AdditionalOrgUnit { get; set; }

        [Display(Name = "Geologist")]
        public string Geologist { get; set; }

        [Display(Name = "VRP Date")]
        public DateOnly? VRPDate { get; set; }

        [Display(Name = "Brownfield Date")]
        public DateOnly? BrownfieldDate { get; set; }

        [Display(Name = "Date De-listed")]
        public DateOnly? DateDeListed { get; set; }
    }
}
