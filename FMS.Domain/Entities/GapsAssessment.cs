using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;
namespace FMS.Domain.Entities
{
    public class GapsAssessment : BaseActiveModel
    {
        public GapsAssessment() { }

        public GapsAssessment(GapsAssessmentCreateDto gapsAssessment)
        {
            Name = gapsAssessment.Name;
            Description = gapsAssessment.Description;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string GetName()
        {
            return $"{Name} - ({Description})";
        }  

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
