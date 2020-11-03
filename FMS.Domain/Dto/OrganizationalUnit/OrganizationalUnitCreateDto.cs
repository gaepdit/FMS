﻿using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class OrganizationalUnitCreateDto
    {
        [Display(Name = "Organizational Unit")]
        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}