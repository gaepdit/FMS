using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityTypeEditDto
    {
        public FacilityTypeEditDto() { }

        public FacilityTypeEditDto(FacilityType facilityType)
        {
            Active = facilityType.Active;
            Code = facilityType.Code;
            Name = facilityType.Name;
        }

        public bool Active { get; set; }

        // Existing numeric code
        public int Code { get; set; }

        [Display(Name = "Facility Type")]
        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
