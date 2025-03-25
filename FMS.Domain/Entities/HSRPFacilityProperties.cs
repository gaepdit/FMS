using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
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
            DateListed = (DateTime)(Hfpcd?.DateListed);
            AdditionalOrgUnit = Hfpcd?.AdditionalOrgUnit;
            Geologist = Hfpcd?.Geologist;
            VRPDate = (DateTime)(Hfpcd?.VRPDate);
            BrownfieldDate = (DateTime)(Hfpcd?.BrownfieldDate);
        }
        public Guid FacilityId { get; set; } = Guid.Empty;
        
        public DateTime DateListed {  get; set; }

        public string AdditionalOrgUnit { get; set; }

        public string Geologist { get; set; }

        public DateTime VRPDate { get; set; }

        public DateTime BrownfieldDate { get; set; }
    }
}
