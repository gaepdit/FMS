using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityStatusSpec
    {
        public bool Active { get; set; }

        [Display(Name = "Facility Status")]
        public string Status { get; set; }

        //public Guid EnvironmentalInterestId { get; set; }
    }
}
