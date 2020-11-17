using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class OrganizationalUnitCreateDto
    {
        [Required(ErrorMessage = "Organizational Unit Name is required.")]
        [Display(Name = "Organizational Unit")]
        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}