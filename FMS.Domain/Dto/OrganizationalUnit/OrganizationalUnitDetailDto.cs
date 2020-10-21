using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class OrganizationalUnitDetailDto
    {
        public OrganizationalUnitDetailDto(OrganizationalUnit organizationalUnit)
        {
            Id = organizationalUnit.Id;
            Active = organizationalUnit.Active;
            Name = organizationalUnit.Name;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [StringLength(20)]
        [Display(Name = "Organizational Unit")]
        public string Name { get; set; }
    }
}
