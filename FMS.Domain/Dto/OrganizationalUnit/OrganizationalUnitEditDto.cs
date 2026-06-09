using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class OrganizationalUnitEditDto
    {
        public OrganizationalUnitEditDto()
        {
            // Required for EditOrganizationalUnit page
        }

        public OrganizationalUnitEditDto(OrganizationalUnit organizationalUnit)
        {
            Active = organizationalUnit.Active;
            Name = organizationalUnit.Name;
            UserProgram = organizationalUnit.UserProgram;
        }

        public bool Active { get; set; }

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
