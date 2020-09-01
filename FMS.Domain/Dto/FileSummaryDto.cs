using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FileSummaryDto
    {
        public FileSummaryDto(File file)
        {
            Id = file.Id;
            FileLabel = file.FileLabel;
            Active = file.Active;
        }

        public Guid Id;

        [StringLength(9)]
        [Display(Name = "File Label")]
        public string? FileLabel { get; set; }

        public bool? Active { get; set; }
    }
}
