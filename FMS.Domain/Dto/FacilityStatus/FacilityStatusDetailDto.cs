using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityStatusDetailDto
    {
        public FacilityStatusDetailDto(FacilityStatus facilityStatus)
        {
            Id = facilityStatus.Id;
            Active = facilityStatus.Active;
            Status = facilityStatus.Status;
            //EnvironmentalInterestId = facilityStatus.EnvironmentalInterest.Id;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Facility Status")]
        public string Status { get; set; }

        //public Guid EnvironmentalInterestId { get; set; }
    }
}
