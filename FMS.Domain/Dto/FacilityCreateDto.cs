using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityCreateDto
    {
        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; set; }

        [Display(Name = "Facility Name")]
        public string Name { get; set; }

        [Display(Name = "County")]
        public int CountyId { get; set; }

        [Display(Name = "Facility Status")]
        public Guid FacilityStatusId { get; set; }
    }
}