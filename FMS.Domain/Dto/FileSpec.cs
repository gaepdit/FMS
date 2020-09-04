using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FileSpec
    {
        [StringLength(9)]
        [Display(Name = "File Label")]
        public string FileLabel { get; set; }

        public bool Active { get; set; }
    }
}
