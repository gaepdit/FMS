﻿using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS.Domain.Dto
{
    public class FacilityStatusEditDto
    {
        public FacilityStatusEditDto() { }

        public FacilityStatusEditDto(FacilityStatus facilityStatus)
        {
            Id = facilityStatus.Id;
            Active = facilityStatus.Active;
            Status = facilityStatus.Status;
            //EnvironmentalInterestId = facilityStatus.EnvironmentalInterest.Id;
        }

        public Guid Id;

        public bool Active { get; set; }

        [Display(Name = "Facility Status")]
        public string Status { get; set; }

        //public Guid EnvironmentalInterestId { get; set; }
    }
}
