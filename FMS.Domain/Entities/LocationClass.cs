using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class LocationClass : BaseActiveModel, INamedModel
    {
        public LocationClass() { }

        public LocationClass(LocationClassCreateDto locClass)
        {
            Name = locClass.Name;
            Description = locClass.Description;
        }

        [Display(Name = "Class Name")]
        public string Name { get; set; }

        [Display(Name = "Class Description")]
        public string Description { get; set; }

        public string DisplayName => $"{Name} ({Description})";

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }
    }
}
