using FMS.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class OrganizationalUnitCreateDto
    {
        public bool Active { get; set; }

        [Display(Name = "Organizational Unit")]
        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
