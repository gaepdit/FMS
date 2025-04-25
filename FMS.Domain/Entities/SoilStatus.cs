using FMS.Domain.Entities.Base;
using FMS.Domain.Dto;

namespace FMS.Domain.Entities
{
    public class SoilStatus : BaseActiveModel
    {
        public SoilStatus() { }

        public SoilStatus(SoilStatusCreateDto soilStatus)
        {
            Name = soilStatus.Name;
        }

        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
