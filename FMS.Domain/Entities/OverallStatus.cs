using FMS.Domain.Entities.Base;
using FMS.Domain.Dto;


namespace FMS.Domain.Entities
{
    public class OverallStatus : BaseActiveModel, INamedModel
    {
        public OverallStatus() { }

        public OverallStatus(OverallStatusCreateDto overallStatus)
        {
            Name = overallStatus.Name;
            Description = overallStatus.Description;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }
    }
}
