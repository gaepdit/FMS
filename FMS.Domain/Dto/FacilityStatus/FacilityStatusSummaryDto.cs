using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityStatusSummaryDto
    {
        public FacilityStatusSummaryDto(FacilityStatus facilityStatus)
        {
            Id = facilityStatus.Id;
            Active = facilityStatus.Active;
            Status = facilityStatus.Status;
        }

        public Guid Id { get; }

        public bool Active { get; }

        [Display(Name = "Facility Status")]
        public string Status { get; }
    }
}