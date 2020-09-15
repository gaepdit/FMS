using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FileSpec
    {
        [StringLength(9)]
        [Display(Name = "File Label")]
        public string FileLabel { get; set; }

        [Display(Name = "County")]
        public int? CountyId { get; set; }

        [Display(Name = "Show inactive")]
        public bool ShowInactive { get; set; }
    }
}
