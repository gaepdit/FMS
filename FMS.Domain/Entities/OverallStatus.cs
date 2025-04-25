using FMS.Domain.Entities.Base;
using FMS.Domain.Dto;


namespace FMS.Domain.Entities
{
    public class OverallStatus : BaseActiveModel
    {
        public OverallStatus() { }

        public OverallStatus(OverallStatusCreateDto overallStatus)
        {
            Name = overallStatus.Name;
        }

        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
