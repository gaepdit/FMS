using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class LocationClassSummaryDto
    {
        public LocationClassSummaryDto(LocationClass locClass)
        {
            Id = locClass.Id;
            Active = locClass.Active;
            Name = locClass.Name;
            Description = locClass.Description;
        }

        public Guid Id { get; }

        public bool Active { get; }

        [Display(Name = "Class")]
        public string Name { get; }

        public string Description { get; }
    }
}
