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
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Facility Status")]
        public string Status { get; set; }

        public void TrimAll()
        {
            Status = Status?.Trim();
        }
    }
}
