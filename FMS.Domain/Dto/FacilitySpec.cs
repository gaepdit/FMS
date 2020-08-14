using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilitySpec
    {
        [Display(Name = "Facility Name")]
        public string Name { get; set; }

        [Display(Name = "Active Site")]
        public bool? Active { get; set; }

        [Display(Name = "County")]
        public int? CountyId { get; set; }
    }
}