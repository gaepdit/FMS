using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

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
        }

        public bool Active { get; set; }

        [Display(Name = "Organizational Unit")]
        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}