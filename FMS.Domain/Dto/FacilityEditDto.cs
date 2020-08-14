using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityEditDto
    {
        public FacilityEditDto() { }
        public FacilityEditDto(FacilityDetailDto facility)
        {
            FacilityNumber = facility.FacilityNumber;
            Name = facility.Name;
            Active = facility.Active;
            CountyId = facility.County.Id;
        }

        [Display(Name = "Facility Number"), Required]
        public string FacilityNumber { get; set; }

        [Display(Name = "Facility Name"), Required]
        public string Name { get; set; }

        [Display(Name = "Active Site")]
        public bool Active { get; set; } = true;

        [Display(Name = "County"), Required]
        public int CountyId { get; set; }
    }
}