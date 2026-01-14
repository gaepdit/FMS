using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityBasicDto
    {
        public FacilityBasicDto(Facility facility)
        {
            Id = facility.Id;
            Active = facility.Active;
            FacilityNumber = facility.FacilityNumber;
            Name = facility.Name;
        }

        public Guid Id { get; }
        public bool Active { get; }

        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; }

        [Display(Name = "Facility Name")]
        public string Name { get; }
    }
}