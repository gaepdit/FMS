using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityStatusCreateDto
    {
        public bool Active { get; set; }

        [Display(Name = "Facility Status")]
        public string Name { get; set; }

        //public Guid EnvironmentalInterestId { get; set; }
    }
}
