using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class FileSummaryDto
    {
        public FileSummaryDto(File file)
        {
            FileLabel = file.FileLabel;
        }

        [Display(Name = "File Label")]
        public string FileLabel { get; }
    }
}