using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class OrganizationalUnitSummaryDto
    {
        public OrganizationalUnitSummaryDto(OrganizationalUnit organizationalUnit)
        {
            Id = organizationalUnit.Id;
            Active = organizationalUnit.Active;
            Name = organizationalUnit.Name;
        }

        public Guid Id { get; }
        public bool Active { get; }

        [Display(Name = "Organizational Unit")]
        public string Name { get; }
    }
}