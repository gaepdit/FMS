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
            Active = file.Active;
            FileLabel = file.FileLabel;
        }

        public Guid Id { get; }
        public bool Active { get; }

        [Display(Name = "File Label")]
        public string FileLabel { get; }
    }
}