using FMS.Domain.Entities;
using System;

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

        public Guid Id { get; set; }

        public bool Active { get; set; }

        public string Name { get; set; }

    }
}
