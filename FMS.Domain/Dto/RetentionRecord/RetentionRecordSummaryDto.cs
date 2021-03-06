﻿using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class RetentionRecordSummaryDto
    {
        public RetentionRecordSummaryDto(RetentionRecord record)
        {
            StartYear = record.StartYear;
            EndYear = record.EndYear;
            BoxNumber = record.BoxNumber;
        }

        [Display(Name = "Start year")]
        public int StartYear { get; }

        [Display(Name = "End year")]
        public int EndYear { get; }

        [Display(Name = "Box Number")]
        public string BoxNumber { get; }

        public string Summary => string.Concat(StartYear, "–", EndYear, ": ", BoxNumber.Replace('-', '‑'));
    }
}