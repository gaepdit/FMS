using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ContactTitleCreateDto
    {
        [Display(Name = "Contact Title")]
        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
