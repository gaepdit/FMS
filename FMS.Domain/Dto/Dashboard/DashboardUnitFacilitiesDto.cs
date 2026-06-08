using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FMS.Domain.Dto
{
    public class DashboardUnitFacilitiesDto
    {
        public DashboardUnitFacilitiesDto(Facility facility) 
        {
            Id = facility.Id;
            Name = facility.Name;
            Active = facility.Active;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
