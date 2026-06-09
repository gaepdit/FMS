using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class OrganizationalUnitCreateDto
    {
        [Required(ErrorMessage = "Organizational Unit Name is required.")]
        [Display(Name = "Organizational Unit")]
        public string Name { get; set; }

        [Display(Name = "Program")]
        public UserProgram UserProgram { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
