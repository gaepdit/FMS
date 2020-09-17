﻿using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class RetentionRecordCreateDto
    {
        [Required]
        [Display(Name = "Facility")]
        public Guid FacilityId { get; set; }

        [Required]
        [Display(Name = "Start year")]
        public int StartYear { get; set; }

        [Required]
        [Display(Name = "End year")]
        public int EndYear { get; set; }

        [Required]
        [Display(Name = "Box Number")]
        public string BoxNumber { get; set; }

        [Display(Name = "Consignment Number")]
        public string ConsignmentNumber { get; set; }

        [Display(Name = "Shelf Number")]
        public string ShelfNumber { get; set; }

        [Display(Name = "Retention Schedule Number")]
        public string RetentionSchedule { get; set; }
    }
}
